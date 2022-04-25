using AutoMapper;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.DA.Interfaces;
using System.Collections.Generic;
using System.Linq;
using PE = EKS.ProcessMaps.Entities.PublishedContent;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class PublishedCommonAppService : IPublishedCommonAppService
    {
        private readonly IPublishContentRepository<PE.KnowledgeAssets> kaRepo;
        private readonly IPublishContentRepository<PE.AssetStatuses> assetStatusRepo;

        /// <summary>
        /// PublishedCommonAppService
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="kaRepo"></param>
        /// <param name="assetStatusRepo"></param>
        public PublishedCommonAppService(
            IPublishContentRepository<PE.KnowledgeAssets> kaRepo,
            IPublishContentRepository<PE.AssetStatuses> assetStatusRepo)
        {
            this.kaRepo = kaRepo;
            this.assetStatusRepo = assetStatusRepo;
        }

        /// <summary>
        /// GetKnowledgeAssetStatus
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public string GetKnowledgeAssetStatus(string contentId, int version)
        {
            if (version == 0)
            {
                version = 1;
            }

            var assetStatus = (
                from ka in this.kaRepo.Queryable()
                join status in this.assetStatusRepo.Queryable() on ka.AssetStatusId equals status.Id
                where ka.ContentId == contentId && ka.Version == version
                select status.Name).FirstOrDefault();

            return assetStatus;
        }
    }
}
