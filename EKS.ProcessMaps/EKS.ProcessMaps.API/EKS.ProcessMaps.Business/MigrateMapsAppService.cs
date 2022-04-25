namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    //using System.Transactions;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using Microsoft.EntityFrameworkCore.Storage;
    using EksEnum = EKS.ProcessMaps.Helper.Enum;

    /// <summary>
    /// CRUD operations of activity blocks.
    /// </summary>
    public class MigrateMapsAppService : IMigrateMapsAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ActivityBlocks> _activitiesRepo;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IRepository<SwimLanes> _swimLanesRepo;
        private readonly IRepository<ContentPhases> _contentPhasesRepo;
        private readonly IRepository<ContentTags> _contentTagsRepo;
        private readonly IRepository<Phases> _phasesRepo;
        private readonly IRepository<NatureOfChange> _natureOfChangeRepo;
        private readonly IRepository<PrivateAssets> _privateAssetsRepo;
        private readonly IProcessMapCommonAppService _commonService;
        private readonly IRepository<MigratedContent> _migratedContentRepo;
        private readonly IRepository<IDbContextTransaction> _repositoryTrans;

        public MigrateMapsAppService(
            IMapper mapper,
            IRepository<ProcessMap> processMapsRepo,
            IRepository<ActivityBlocks> activitiesRepo,
            IRepository<SwimLanes> swimLanesRepo,
            IRepository<ContentPhases> contentPhasesRepo,
            IRepository<ContentTags> contentTagsRepo,
            IRepository<Phases> phasesRepo,
            IRepository<NatureOfChange> natureOfChangeRepo,
            IRepository<PrivateAssets> privateAssetsRepo,
            IProcessMapCommonAppService commonService,
            IRepository<MigratedContent> migratedContentRepo,
            IRepository<IDbContextTransaction> repositoryTrans)
        {
            this._processMapsRepo = processMapsRepo;
            this._mapper = mapper;
            this._activitiesRepo = activitiesRepo;
            this._swimLanesRepo = swimLanesRepo;
            this._contentPhasesRepo = contentPhasesRepo;
            this._contentTagsRepo = contentTagsRepo;
            this._phasesRepo = phasesRepo;
            this._natureOfChangeRepo = natureOfChangeRepo;
            this._privateAssetsRepo = privateAssetsRepo;
            this._commonService = commonService;
            this._migratedContentRepo = migratedContentRepo;
            this._repositoryTrans = repositoryTrans;
        }

        /// <summary>
        /// MigrateMapsAsync.
        /// </summary>
        /// <param name="contentAssetTypeModel"></param>
        /// <returns></returns>
        public async Task<ProcessMapInputOutputModel> MigrateMapsAsync(ContentAssetTypeModel contentAssetTypeModel)
        {
            ProcessMapInputOutputModel resultDataModel = new ProcessMapInputOutputModel();
            string contentId = string.Empty;
            ProcessMap resultData = new ProcessMap();
            IDbContextTransaction transaction = null;
            //var trnOptions = new TransactionOptions
            //{
            //    Timeout = TimeSpan.MaxValue,
            //};
            //using (TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, trnOptions))
            //{
            try
            {
                contentAssetTypeModel.AssetStatusId = (int)EksEnum.AssetStatuses.Draft;
                var oldCreatedDateTime = contentAssetTypeModel.CreatedDateTime;
                contentAssetTypeModel.CreatedDateTime = DateTime.Now;
                contentAssetTypeModel.LastUpdateDateTime = DateTime.Now;
                contentAssetTypeModel.LastUpdateUser = contentAssetTypeModel.CreatedUser;
                contentAssetTypeModel.PrivateInd = false;

                // Added
                if (contentAssetTypeModel.AssetTypeId == null
                    || contentAssetTypeModel.AssetTypeId == 0
                    || contentAssetTypeModel.AssetTypeId == (int)EksEnum.AssetTypes.M)
                {
                    contentAssetTypeModel.AssetTypeId = (int)EksEnum.AssetTypes.SF;
                }

                var toModel = this._mapper.Map<ProcessMapInputOutputModel>(contentAssetTypeModel);
                toModel.DisciplineId = null;

                var processMapModel = this._mapper.Map<ProcessMap>(toModel);
                processMapModel.Gen2ContentId = contentAssetTypeModel.ContentId;
                processMapModel.Version = 1;

                var existingSF = this._processMapsRepo.FindAll(pm => pm.Gen2ContentId == contentAssetTypeModel.ContentId)?.FirstOrDefault();

                if (existingSF != null)
                {
                    throw new Exception($"Map {existingSF.Gen2ContentId} already migrated.");
                }
                // Begin Transaction
                transaction = await this._repositoryTrans.BeginTransaction().ConfigureAwait(false);
                resultData = await this._processMapsRepo.AddAsyn(processMapModel).ConfigureAwait(false);
                resultDataModel = this._mapper.Map<ProcessMapInputOutputModel>(resultData);

                contentAssetTypeModel.Id = resultData.Id;
                resultDataModel.StepFlowId = resultData.Id;

                int? sfSwimLaneId = null;
                if (resultDataModel.StepFlowId > 0
                    && contentAssetTypeModel.SwimLane != null)
                {
                    contentAssetTypeModel.SwimLane.ProcessMapId = resultData.Id;
                    var slResult = this._swimLanesRepo.Add(this._mapper.Map<SwimLanes>(contentAssetTypeModel.SwimLane));
                    sfSwimLaneId = slResult.Id;
                }

                if (contentAssetTypeModel.ContentPhase != null)
                {
                    contentAssetTypeModel.ContentPhase.ForEach(contentPhase =>
                    {
                        var objDataPhase = new ContentPhases();
                        {
                            objDataPhase.ContentId = Convert.ToInt32(resultData.Id);
                            objDataPhase.TypeId = Convert.ToInt32(resultData.AssetTypeId);
                            objDataPhase.PhaseId = Convert.ToInt32(contentPhase);
                            this._contentPhasesRepo.Add(objDataPhase);
                        }
                    });
                }

                contentId = await this.GenerateContentId(resultData, resultDataModel).ConfigureAwait(false);
                resultDataModel.ContentId = contentId;
                resultDataModel.ContentTag = contentAssetTypeModel.ContentTag;

                var mapPhaseSFResult = await this.AddPhase(processMapId: resultData.Id).ConfigureAwait(false);

                // Saving for NatureOfChange
                var nocProcessMapModel = this._mapper.Map<ProcessMapModel>(resultDataModel);
                nocProcessMapModel.Id = resultData.Id;
                await this.AddNatureOfChange(nocProcessMapModel).ConfigureAwait(false);
                nocProcessMapModel.ContentId = contentAssetTypeModel.ContentId;
                nocProcessMapModel.CreatedDateTime = oldCreatedDateTime;
                nocProcessMapModel.EffectiveFrom = oldCreatedDateTime;
                nocProcessMapModel.EffectiveTo = DateTime.Today;
                nocProcessMapModel.Version = Convert.ToInt32(contentAssetTypeModel.VersionNumber);
                await this.AddNatureOfChange(nocProcessMapModel, $"Map {contentAssetTypeModel.ContentId} with new contentId {contentId} migrated from Gen2 ESW.").ConfigureAwait(false);

                foreach (var step in contentAssetTypeModel.Steps)
                {
                    var stepFlowId = resultDataModel.StepFlowId;

                    // Create Step
                    step.Id = stepFlowId;

                    var existingStep = this._processMapsRepo.FindAll(pm => pm.Gen2ContentId == step.ContentId)?.FirstOrDefault();
                    // TODO: Add exising step check
                    if (existingStep != null)
                    {
                        var existingStepModel = this._mapper.Map<ProcessMapModel>(existingStep);
                        var stepActivityBlocks = step.ActivityBlocks.Where(x => x.DisciplineText == null).ToList();
                        foreach (var stepActivityBlock in stepActivityBlocks)
                        {
                            stepActivityBlock.ProcessMapId = stepFlowId;
                            var block = await this.AddActivityBlock(stepActivityBlock, existingStepModel, mapPhaseSFResult?.Id, sfSwimLaneId).ConfigureAwait(false);
                        }
                    }
                    else
                    {
                        var stepResult = await this.AddStep(step, toModel).ConfigureAwait(false);
                        var stepModel = this._mapper.Map<ProcessMapInputOutputModel>(step);
                        contentId = await this.GenerateContentId(stepResult, stepModel).ConfigureAwait(false);
                        var oldStepContentId = step.ContentId;
                        step.ContentId = contentId;
                        stepResult.ContentId = contentId;

                        var stepActivityBlocks = step.ActivityBlocks.Where(x => x.DisciplineText == null).ToList();
                        foreach (var stepActivityBlock in stepActivityBlocks)
                        {
                            stepActivityBlock.ProcessMapId = stepFlowId;
                            var block = await this.AddActivityBlock(stepActivityBlock, step, mapPhaseSFResult?.Id, sfSwimLaneId).ConfigureAwait(false);
                        }   

                        step.Id = stepResult.Id;
                        var mapPhaseStepResult = await this.AddPhase(step.Id).ConfigureAwait(false);
                        foreach (var swimLane in step.SwimLanes)
                        {
                            var actsByDiscipline = step.ActivityBlocks.Where(x => x.DisciplineText == swimLane.DisciplineText).ToList();
                            swimLane.ProcessMapId = stepResult.Id;
                            var swimLaneRes = this._swimLanesRepo.Add(this._mapper.Map<SwimLanes>(swimLane));
                            foreach (var activityBlock in actsByDiscipline)
                            {
                                var slId = swimLaneRes?.Id;
                                activityBlock.ProcessMapId = stepResult.Id;
                                var block = await this.AddActivityBlock(activityBlock, step, mapPhaseStepResult?.Id, slId).ConfigureAwait(false);
                            }
                        }

                        step.Version = step.Version ?? 1;
                        await this.AddNatureOfChange(step).ConfigureAwait(false);
                        if (oldStepContentId.Split('-').Length > 2)
                        {
                            // Saving for NatureOfChange Legacy
                            nocProcessMapModel = step;
                            nocProcessMapModel.Id = stepResult.Id;
                            nocProcessMapModel.ContentId = oldStepContentId;
                            nocProcessMapModel.CreatedDateTime = contentAssetTypeModel.CreatedDateTime;
                            nocProcessMapModel.EffectiveFrom = contentAssetTypeModel.CreatedDateTime;
                            nocProcessMapModel.EffectiveTo = DateTime.Today;
                            await this.AddNatureOfChange(nocProcessMapModel, $"Map {oldStepContentId} migrated from Gen2 ESW.").ConfigureAwait(false);
                        }

                        var returnModel = this._mapper.Map<ProcessMapModel>(stepResult);
                        if (step.PrivateInd)
                        {
                            await this.AddPrivateAssets(stepResult.Id, stepFlowId).ConfigureAwait(false);
                        }
                        else
                        {
                            await this.CreateMigratedContentAsync("Created", returnModel.Id, contentId, "SP", returnModel.Title, 1).ConfigureAwait(false);
                        }
                    }
                }

                await this.CreateMigratedContentAsync("Created", resultDataModel.StepFlowId, resultDataModel.ContentId, "SF", resultDataModel.Title, 1).ConfigureAwait(false);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                throw;
            }

            //}

            return resultDataModel;
        }

        /// <summary>
        /// CreateMigratedContentAsync.
        /// </summary>
        /// <param name="currentStatus">string</param>
        /// <param name="contentid">numeric</param>
        /// <param name="contentNo">XXX-X-1234</param>
        /// <param name="contentType"></param>
        /// <param name="title">string</param>
        /// <param name="version"></param>
        /// <returns>MigratedContentModel</returns>
        private async Task<MigratedContent> CreateMigratedContentAsync(string currentStatus, long contentid, string contentNo, string contentType, string title, int? version)
        {
            var migratedContentModel = new MigratedContent
            {
                CurrentStatus = currentStatus,
                Contentid = contentid,
                Contentno = contentNo,
                ContentType = contentType,
                Title = title,
                Version = Convert.ToInt32(version),
            };
            var result = await this._migratedContentRepo.AddAsyn(migratedContentModel).ConfigureAwait(false);
            return result;
        }

        /// <summary>
        /// ContentId Genreation Generation for Process Maps
        /// </summary>
        /// <param name="resultData"></param>
        /// <param name="resultDataModel"></param>
        /// <returns>string contentId</returns>
        private async Task<string> GenerateContentId(ProcessMap resultData, ProcessMapInputOutputModel resultDataModel)
        {
            var disciplineCodeId = resultDataModel.DisciplineId;// disciplineId needs to go
            var assetTypeId = resultDataModel.AssetTypeId;
            var createdUser = resultDataModel.CreatedUser;
            var processMapId = resultData.Id;
            return await this._commonService.GenerateContentId(processMapId, assetTypeId, createdUser, resultData.PrivateInd).ConfigureAwait(false);
        }

        private async Task<Phases> AddPhase(int? processMapId)
        {
            Phases phases = new Phases();
            phases.ProcessMapId = processMapId;
            phases.Caption = "Phase-1";
            phases.Name = "Phase-1";

            return await this._phasesRepo.AddAsyn(phases).ConfigureAwait(false);
        }

        /// <summary>
        /// AddStep.
        /// </summary>
        /// <param name="stepModel"></param>
        /// <param name="pmModel"></param>
        /// <returns></returns>
        private async Task<ProcessMap> AddStep(ProcessMapModel stepModel, ProcessMapInputOutputModel pmModel)
        {
            var step = this._mapper.Map<ProcessMap>(pmModel);
            step.PrivateInd = stepModel.PrivateInd;
            step.AssetTypeId = (int)EksEnum.AssetTypes.SP;
            step.Title = stepModel.Title;
            step.Gen2ContentId = stepModel.ContentId?.Split('-')?.Length > 2 ? stepModel.ContentId : null;
            step.Version = 1;
            step.Id = 0;

            var stepResult = await this._processMapsRepo.AddAsyn(step).ConfigureAwait(false);
            var contentPhases = await this._contentPhasesRepo.FindAllAsync(x => x.ContentId == stepModel.Id).ConfigureAwait(false);
            foreach (var contentPhase in contentPhases)
            {
                var contentPhaseEntity = new ContentPhases()
                {
                    ContentId = stepResult.Id,
                    TypeId = step.AssetTypeId,
                    PhaseId = contentPhase.PhaseId,
                };
                this._contentPhasesRepo.Add(contentPhaseEntity);
            }

            return stepResult;
        }

        private async Task<ActivityBlocks> AddActivityBlock(ActivityBlocksModel activityModel, ProcessMapModel step, int? phaseId, int? sfSwimLaneId)
        {
            var stepFlowId = activityModel.ProcessMapId;
            var swimLaneId = sfSwimLaneId;
            if (swimLaneId == null || swimLaneId == 0)
            {
                swimLaneId = this._swimLanesRepo.Find(x => x.ProcessMapId == stepFlowId)?.Id;
            }

            //var bigest = this._activitiesRepo.FindBy(x => x.ProcessMapId == stepFlowId && x.SequenceNumber != null)
            //    .Select(x => x.SequenceNumber).ToList().DefaultIfEmpty(0).Max();

            //activityModel.SequenceNumber = bigest + 1;
            activityModel.ProcessMapId = stepFlowId;
            activityModel.SwimLaneId = swimLaneId;
            //activityModel.ActivityTypeId = 8;
            activityModel.PhaseId = phaseId;
            activityModel.AssetContentId = activityModel.ActivityTypeId == 8 ? step.ContentId : activityModel.AssetContentId;
            if (activityModel.PhaseId == 0)
            {
                activityModel.PhaseId = null;
            }

            return await this._activitiesRepo.AddAsyn(this._mapper.Map<ActivityBlocks>(activityModel)).ConfigureAwait(false);
        }

        /// <summary>
        /// AddNatureOfChange.
        /// </summary>
        /// <param name="mapModel"></param>
        /// <param name="nocDefinition"></param>
        /// <returns></returns>
        private async Task<NatureOfChange> AddNatureOfChange(ProcessMapModel mapModel, string nocDefinition = null)
        {
            NatureOfChange nocData = new NatureOfChange();
            var creationDate = nocDefinition == null ? DateTime.Today : mapModel.CreatedDateTime;
            var effectiveToDate = nocDefinition == null ? DateTime.MaxValue : DateTime.Today;
            nocData.ContentId = mapModel.ContentId;
            nocData.ContentItemId = Convert.ToInt32(mapModel.Id);
            nocData.AssetTypeId = mapModel.AssetTypeId;
            nocData.Version = mapModel.Version;
            nocData.NocdateTime = creationDate;
            nocData.EffectiveFrom = creationDate ?? DateTime.Today;
            nocData.EffectiveTo = effectiveToDate;
            nocData.Definition = nocDefinition ?? "Initial Version";
            nocData.CreatedDateTime = creationDate ?? DateTime.Today;
            nocData.CreatedUser = mapModel.CreatedUser;

            return await this._natureOfChangeRepo.AddAsyn(nocData).ConfigureAwait(false);
        }

        private async Task<PrivateAssets> AddPrivateAssets(int stepId, int? stepFlowId)
        {
            var privateAsset = new PrivateAssets
            {
                ContentAssetId = stepId,
                ParentContentAssetId = stepFlowId,
            };

            return await this._privateAssetsRepo.AddAsyn(privateAsset).ConfigureAwait(false);
        }
    }
}
