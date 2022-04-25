namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using global::EKS.ProcessMaps.Business.Interfaces;
    using Microsoft.Extensions.Configuration;
    using EksEnum = EKS.ProcessMaps.Helper.Enum;

    /// <summary>
    /// Public steps related crud operations
    /// </summary>
    public class ProcessMapCommonAppService : IProcessMapCommonAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IRepository<ContentInformation> _contentInformationRepo;
        private readonly IRepository<ContentType> _contentTypeRepo;
        private readonly IConfiguration _config;
        private readonly IRepository<AssetStatus> _assetStatusRepo;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="config"></param>
        /// <param name="processMapsRepo"></param>
        /// <param name="contentInformationRepo"></param>
        /// <param name="contentTypeRepo"></param>
        public ProcessMapCommonAppService(
            IMapper mapper,
            IConfiguration config,
            IRepository<ProcessMap> processMapsRepo,
            IRepository<ContentInformation> contentInformationRepo,
            IRepository<ContentType> contentTypeRepo,
            IRepository<AssetStatus> assetStatusRepo)
        {
            this._mapper = mapper;
            this._config = config;
            this._processMapsRepo = processMapsRepo;
            this._contentInformationRepo = contentInformationRepo;
            this._contentTypeRepo = contentTypeRepo;
            this._assetStatusRepo = assetStatusRepo;
        }

        /// <summary>
        /// GenerateContentId
        /// </summary>
        /// <param name="processMapId"></param>
        /// <param name="assetTypeId"></param>
        /// <param name="createdUser"></param>
        /// <param name="isPrivateAsset"></param>
        /// <returns></returns>
        public async Task<string> GenerateContentId(int processMapId, int? assetTypeId, string createdUser, bool isPrivateAsset)
        {
            var contentType = this._contentTypeRepo.FindAll(x => x.ContentTypeId == (assetTypeId ?? 4)).FirstOrDefault()?.Code;
            string contentId = await this.GetContentIdInfo(contentType, isPrivateAsset).ConfigureAwait(false);
            ContentInformation contentDetails = new ContentInformation();
            contentDetails.ContentId = contentId;
            contentDetails.ContentItemId = Convert.ToInt32(processMapId);
            contentDetails.ContentType = contentType;
            contentDetails.CreatedDateTime = DateTime.Today;
            contentDetails.CreatedUser = createdUser;
            var contentInformationResult = await this._contentInformationRepo.AddAsyn(contentDetails).ConfigureAwait(false);
            var contentInformationId = contentInformationResult?.ContentId;
            ProcessMapModel updatedProcessMapModel = await this.UpdateContentIdAsync(processMapId, contentId).ConfigureAwait(false);
            contentId = updatedProcessMapModel.ContentId;

            return await Task.FromResult(contentId).ConfigureAwait(false);
        }

        /// <summary>
        /// GetContentIdInfo
        /// </summary>
        /// <param name="identifier"></param>
        /// <param name="isPrivateAsset"></param>
        /// <returns></returns>
        public async Task<string> GetContentIdInfo(string identifier, bool isPrivateAsset)
        {
            int latestId = await this.GetCGContentID(isPrivateAsset).ConfigureAwait(false);
            int contentId = latestId + 1;
            switch (identifier)
            {
                case "M":
                case "SF":
                    identifier = "F";
                    break;
                case "SP":
                    identifier = "P";
                    break;
                default:
                    identifier = "F";
                    break;
            }

            var contentIdSettings = this._config.GetSection("ContentIdSettings");
            var length = isPrivateAsset
                ? contentIdSettings.GetValue<string>("PvtTotalLength")
                : contentIdSettings.GetValue<string>("TotalLength");

            string customContentId = $"{identifier}-{contentId.ToString($"D{length}")}";
            return customContentId;
        }

        /// <summary>
        /// GetCGContentID
        /// </summary>
        /// <param name="isPrivateAsset"></param>
        /// <returns></returns>
        private async Task<int> GetCGContentID(bool isPrivateAsset = false)
        {
            var contentIdSettings = this._config.GetSection("ContentIdSettings");
            var startNumber = !isPrivateAsset ? contentIdSettings.GetValue<int>("StartNumber")
                : contentIdSettings.GetValue<int>("PvtStartNumber");
            int dataCount = 0;
            try
            {
                dataCount = await this._contentInformationRepo.CountAsync().ConfigureAwait(false);
                dataCount = startNumber + dataCount;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }
            return dataCount;
        }

        /// <summary>
        /// UpdateContentIdAsync
        /// </summary>
        /// <param name="processMapId"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<ProcessMapModel> UpdateContentIdAsync(int processMapId, string contentId)
        {
            ProcessMap resultData = this._processMapsRepo.FindBy(x => x.Id == processMapId).FirstOrDefault();
            resultData.ContentId = contentId;

            ProcessMap updateData = await this._processMapsRepo.UpdateExAsyn(resultData, processMapId).ConfigureAwait(false);

            return this._mapper.Map<ProcessMapModel>(updateData);
        }

        /// <summary>
        /// Get asset status name
        /// </summary>
        /// <param name="assetStatusId"></param>
        /// <returns></returns>
        public async Task<string> GetAssetStatusAsync(int assetStatusId)
        {
            return (await this._assetStatusRepo.FindByAsyn(x => x.AssetStatusId == assetStatusId).ConfigureAwait(false))?.FirstOrDefault()?.Name;
        }
    }
}
