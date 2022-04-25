namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;

    public class KnowledgeAssetsAppService : IKnowledgeAssetsAppService
    {
        private readonly IRepository<KnowledgeAsset> _knowledgeAssetsRepo;
        private readonly IMapper mapper;

        public KnowledgeAssetsAppService(IRepository<KnowledgeAsset> knowledgeAssetsRepo, IMapper mapper)
        {
            this.mapper = mapper;
            this._knowledgeAssetsRepo = knowledgeAssetsRepo;
        }

        public async Task<KnowledgeAssetModel> CreateKnowledgeAssetAsync(KnowledgeAssetModel model)
        {
            var newEntity = this.mapper.Map<KnowledgeAsset>(model);
            var entity = await this._knowledgeAssetsRepo.AddAsyn(newEntity).ConfigureAwait(false);
            return this.mapper.Map<KnowledgeAssetModel>(entity);
        }
    }
}