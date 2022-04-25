using AutoMapper;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Entities.PublishedContent;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKSEnum = EKS.ProcessMaps.Helper.Enum;
using PE = EKS.ProcessMaps.Entities.PublishedContent;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class ContainerItemAppService : IContainerItemAppService
    {
        private readonly IMapper _mapper;
        private readonly IPublishContentRepository<ContainerItems> _containerItemRepo;
        private readonly IPublishContentRepository<KnowledgeAssets> _kaRepo;

        public ContainerItemAppService(
            IMapper mapper,
            IPublishContentRepository<ContainerItems> containerItemRepo,
            IPublishContentRepository<KnowledgeAssets> kaRepo)
        {
            this._mapper = mapper;
            this._containerItemRepo = containerItemRepo;
            this._kaRepo = kaRepo;
        }

        /// <summary>
        /// GetKpackMapByKnowledgeAssetId
        /// </summary>
        /// <param name="knowledgeAssetId"></param>
        /// <returns></returns>
        public async Task<List<KPackMapExtendModel>> GetKpackMapByKnowledgeAssetId(string parentContentAssetId, int version)
        {
            var result = new List<KPackMapExtendModel>();

            var knowledgeAssets = await this._kaRepo.FindAllAsync(x => x.ContentId == parentContentAssetId);
            var ka = knowledgeAssets.LastOrDefault(x => version == 0 || x.Version == version);
            if (ka == null)
            {
                return result;
            }

            var kpacks = await this._containerItemRepo.FindAllAsync(x => x.ContainerKnowledgeAssetId == ka.Id);
            if (kpacks == null)
            {
                return result;
            }
            
            foreach (var kpack in kpacks)
            {
                var knowledgePack = this._kaRepo.GetAllIncluding(x => x.AssetTypeCodeNavigation, x => x.Discipline)
                    .FirstOrDefault(x => x.ContentId == kpack.AssetContentId);

                var model = this._mapper.Map<KPackMapExtendModel>(kpack);
                model.ParentContentAssetId = ka.ContentId;
                model.ParentContentTypeId = (int)EKSEnum.AssetTypes.SF; // Dont know why it hard code = 13 on UI
                model.Title = knowledgePack?.Title;
                model.DisciplineCode = knowledgePack?.DisciplineCode;
                model.ContentTypeName = knowledgePack?.AssetTypeCodeNavigation?.Name;
                model.SubSubDisciplineName = this.GetSubSubDisciplineName(knowledgePack.Discipline);

                result.Add(model);
            }

            return result;
        }
        
        /// <summary>
        /// GetSubSubDisciplineName
        /// </summary>
        /// <param name="disciplines"></param>
        /// <returns></returns>
        private string GetSubSubDisciplineName(PE.Disciplines disciplines)
        {
            if (disciplines == null)
            {
                return string.Empty;
            }

            return
                disciplines.Discipline4 ??
                disciplines.Discipline3 ??
                disciplines.Discipline2 ??
                disciplines.Discipline1;
        }
    }
}
