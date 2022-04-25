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
    public class PublicStepsAppService : IPublicStepsAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IRepository<ContentPhases> _contentPhasesRepo;
        private readonly IRepository<ContentTags> _contentTagsRepo;
        private readonly IRepository<ContentInformation> _contentInformationRepo;
        private readonly IRepository<ContentType> _contentTypeRepo;
        private readonly IRepository<Phases> _phasesRepo;
        private readonly IRepository<NatureOfChange> natureOfChangeRepo;
        private readonly IConfiguration _config;
        private readonly IProcessMapCommonAppService _commonService;

        /// <summary>
        /// Constructor of public steps
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="config"></param>
        /// <param name="processMapsRepo"></param>
        /// <param name="contentPhasesRepo"></param>
        /// <param name="contentTagsRepo"></param>
        /// <param name="contentInformationRepo"></param>
        /// <param name="contentTypeRepo"></param>
        /// <param name="phasesRepo"></param>
        /// <param name="natureOfChangeRepo"></param>
        public PublicStepsAppService(
            IMapper mapper,
            IConfiguration config,
            IRepository<ProcessMap> processMapsRepo,
            IRepository<ContentPhases> contentPhasesRepo,
            IRepository<ContentTags> contentTagsRepo,
            IRepository<ContentInformation> contentInformationRepo,
            IRepository<ContentType> contentTypeRepo,
            IRepository<Phases> phasesRepo,
            IRepository<NatureOfChange> natureOfChangeRepo,
            IProcessMapCommonAppService commonService)
        {
            this._mapper = mapper;
            this._config = config;
            this._processMapsRepo = processMapsRepo;
            this._contentPhasesRepo = contentPhasesRepo;
            this._contentTagsRepo = contentTagsRepo;
            this._contentInformationRepo = contentInformationRepo;
            this._contentTypeRepo = contentTypeRepo;
            this._phasesRepo = phasesRepo;
            this.natureOfChangeRepo = natureOfChangeRepo;
            this._commonService = commonService;
        }

        /// <summary>
        /// Create public steps.
        /// </summary>
        /// <param name="processMapModel"></param>
        /// <returns></returns>
        public async Task<PublicStepsInputOutputModel> CreatePublicStepsAsync(ProcessMapModel processMapModel)
        {
            PublicStepsInputOutputModel resultDataModel = new PublicStepsInputOutputModel();
            string contentId = string.Empty;
            ProcessMap resultData = new ProcessMap();
            try
            {
                processMapModel.AssetStatusId = (int)EksEnum.AssetStatuses.Draft;
                processMapModel.CreatedDateTime = DateTime.Now;
                processMapModel.LastUpdateDateTime = DateTime.Now;
                processMapModel.LastUpdateUser = processMapModel.CreatedUser;
                processMapModel.PrivateInd = false; // Should be false for public steps

                resultData = await this._processMapsRepo.AddAsyn(this._mapper.Map<ProcessMap>(processMapModel)).ConfigureAwait(false);
                resultDataModel = this._mapper.Map<PublicStepsInputOutputModel>(resultData);

                if (processMapModel.ContentPhase != null)
                {
                    var newPhases = new List<ContentPhases>();
                    processMapModel.ContentPhase.ForEach(contentPhase =>
                    {
                        var objDataPhase = new ContentPhases
                        {
                            ContentId = Convert.ToInt32(resultData.Id),
                            TypeId = Convert.ToInt32(resultData.AssetTypeId),
                            PhaseId = Convert.ToInt32(contentPhase),
                        };
                        newPhases.Add(objDataPhase);
                    });
                    this._contentPhasesRepo.AddRange(newPhases);
                }

                if (processMapModel.ContentTag != null)
                {
                    var newTags = new List<ContentTags>();
                    processMapModel.ContentTag.ForEach(contentTag =>
                    {
                        var objData = new ContentTags
                        {
                            ContentId = Convert.ToInt32(resultData.Id),
                            TypeId = resultData.AssetTypeId ?? 0,
                            TagId = Convert.ToInt32(contentTag),
                        };
                        newTags.Add(objData);
                    });
                    this._contentTagsRepo.AddRange(newTags);
                }

                contentId = await this.GenerateContentId(resultData, resultDataModel).ConfigureAwait(false);
                resultDataModel.ContentId = contentId;
                resultDataModel.ContentTag = processMapModel.ContentTag;

                await this.AddPhase(processMapId: resultData.Id).ConfigureAwait(false);

                await this.CreateNatureOfChange(processMapModel, resultData, contentId).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return resultDataModel;
        }

        /// <summary>
        /// Add phases
        /// </summary>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        private async Task<Phases> AddPhase(int? processMapId)
        {
            Phases phases = new Phases
            {
                ProcessMapId = processMapId,
                Caption = "Phase-1",
                Name = "Phase-1",
            };

            return await this._phasesRepo.AddAsyn(phases).ConfigureAwait(false);
        }

        /// <summary>
        /// Create nature of change.
        /// </summary>
        /// <param name="processMapModel"></param>
        /// <param name="resultData"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        private async Task<NatureOfChange> CreateNatureOfChange(ProcessMapModel processMapModel, ProcessMap resultData, string contentId)
        {
            NatureOfChange nocData = new NatureOfChange
            {
                ContentId = string.IsNullOrEmpty(contentId) ? processMapModel.ContentId : contentId,
                ContentItemId = Convert.ToInt32(resultData.Id),
                AssetTypeId = processMapModel.AssetTypeId,
                Version = resultData.Version,
                NocdateTime = resultData.CreatedDateTime,
                Definition = "Initial Version",
                CreatedDateTime = resultData.CreatedDateTime ?? DateTime.Today,
                CreatedUser = resultData.CreatedUser,
            };

            return await this.natureOfChangeRepo.AddAsyn(nocData).ConfigureAwait(false);
        }

        /// <summary>
        /// ContentId Genreation Generation for Process Maps
        /// </summary>
        /// <param name="resultData"></param>
        /// <param name="resultDataModel"></param>
        /// <returns>string contentId</returns>
        private async Task<string> GenerateContentId(ProcessMap resultData, PublicStepsInputOutputModel resultDataModel)
        {
            var assetTypeId = resultDataModel.AssetTypeId;
            var createdUser = resultDataModel.CreatedUser;
            var processMapId = resultData.Id;
            return await this._commonService.GenerateContentId(processMapId, assetTypeId, createdUser, isPrivateAsset: false).ConfigureAwait(false);
        }

        /// <summary>
        /// Update purpose of public steps
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<PublicStepsPurposeModel> UpdatePublicStepsPurposeAsync(PublicStepsPurposeModel model)
        {
            ProcessMap updatedData = new ProcessMap();

            ProcessMap actualData = this._processMapsRepo.FindBy(x => x.Id == model.Id).FirstOrDefault();
            if (actualData != null)
            {
                actualData.ContentId = model.ContentId;
                actualData.Purpose = model.Purpose;
                updatedData = await this._processMapsRepo.UpdateExAsyn(actualData, actualData.Id).ConfigureAwait(false);
            }

            return this._mapper.Map<PublicStepsPurposeModel>(updatedData);
        }
    }
}
