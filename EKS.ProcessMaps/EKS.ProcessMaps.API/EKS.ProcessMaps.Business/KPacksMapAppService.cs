namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;

    public class KPacksMapAppService : IKPacksAppService
    {
        private readonly IRepository<KpacksMap> _kpackrepo;
        private readonly IKnowledgePackRepository _knowledgePackRepo;
        private readonly IMapper mapper;

        public KPacksMapAppService(IRepository<KpacksMap> kpackrepo, IKnowledgePackRepository knowledgePackRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this._kpackrepo = kpackrepo;
            this._knowledgePackRepo = knowledgePackRepo;
        }

        public async Task<KPackMapModel> CreateKPacksMapAsync(KPackMapModel kPackModel)
        {
            KpacksMap resultData = await this._kpackrepo.AddAsyn(this.mapper.Map<KpacksMap>(kPackModel)).ConfigureAwait(false);
            return this.mapper.Map<KPackMapModel>(resultData);
        }

        public async Task<IEnumerable<KPackMapModel>> AddKPacksMapAsync(IEnumerable<KPackMapModel> kPackMapModels)
        {
            var resultDate = new List<KPackMapModel>();

            foreach (var model in kPackMapModels)
            {
                var entity = await this._kpackrepo.AddAsyn(this.mapper.Map<KpacksMap>(model)).ConfigureAwait(false);
                resultDate.Add(this.mapper.Map<KPackMapModel>(entity));
            }

            return resultDate;
        }
        public async Task<List<KPackMapExtendModel>> GetKPacksMapByParentContentAssetIdAsync(string parentContentAssetId, int? version)
        {
            Expression<Func<KpacksMap, bool>> query = x => x.ParentContentAssetId == parentContentAssetId;
            if (version.HasValue)
            {
                query = x => x.ParentContentAssetId == parentContentAssetId && x.Version == version;
            }

            var kPackMapEntities = await this._kpackrepo.FindAllAsync(query).ConfigureAwait(false);
            
            var result = this.mapper.Map<List<KPackMapExtendModel>>(kPackMapEntities);

            foreach (var model in result)
            {
                var knowledgePack = this._knowledgePackRepo.FindKnowledgePackByContentId(model.ContentAssetId);

                if (knowledgePack != null)
                {
                    model.Title = knowledgePack.Title;
                    model.DisciplineCode = knowledgePack.DisciplineCode;
                    if (knowledgePack.AssetType != null)
                    {
                        model.ContentTypeName = knowledgePack.AssetType.Name;
                    }
                    if (knowledgePack.Discipline != null)
                    {
                        model.SubSubDisciplineName =
                            knowledgePack.Discipline.Discipline4 ??
                            knowledgePack.Discipline.Discipline3 ??
                            knowledgePack.Discipline.Discipline2 ??
                            knowledgePack.Discipline.Discipline1;
                    }
                }
            }
          
            return result;
        }

        public async Task<bool> DeleteKPacksMapAsync(long id)
        {
            KpacksMap resultData = this._kpackrepo.FindBy(x => x.KpacksMapId == id).FirstOrDefault();

            if (resultData != null)
            {
                int result = await this._kpackrepo.DeleteAsyn(this.mapper.Map<KpacksMap>(resultData)).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}