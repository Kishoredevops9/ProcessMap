using AutoMapper;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.DA.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;
using E = EKS.ProcessMaps.Entities;
using PM = EKS.ProcessMaps.Models.PublishedContent;
using M = EKS.ProcessMaps.Models;
using EKSEnum = EKS.ProcessMaps.Helper.Enum;
using EKS.ProcessMaps.Helper.Enum;
using EKS.ProcessMaps.Models;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class KnowledgeAssetCloneAppService : IKnowledgeAssetCloneAppService
    {
        private readonly IMapper _mapper;
        private readonly IKnowledgeAssetRepository _kaRepo;
        private readonly IKnowledgeAssetTransferService _transferService;
        private readonly IPublishContentRepository<PE.ActivityBlocks> _activityBlockPublishedRepo;
        private readonly IPublishContentRepository<PE.Connectors> _connectorRepo;
        private readonly IPublishContentRepository<PE.UsersCache> _userCacheRepo;

        private readonly IProcessMapCommonAppService _commonService;
        private readonly IProcessMapsAppService _processMapsAppService;
        private readonly IRepository<E.ProcessMap> _processMapsRepo;
        private readonly IRepository<E.SwimLanes> _swimLaneRepo;
        private readonly IRepository<E.Phases> _phasesRepo;
        private readonly IRepository<E.ContentPhases> _contentPhasesRepo;
        private readonly IRepository<E.ContentExportCompliances> _contentExportCompliancesRepo;
        private readonly IRepository<E.ContentTags> _contentTagsRepo;
        private readonly IRepository<E.NatureOfChange> _natureOfChangeRepo;
        private readonly IRepository<E.PrivateAssets> _privateAssetsRepo;
        private readonly IRepository<E.KpacksMap> _kpackMapRepo;
        private readonly IRepository<E.ActivityBlocks> _activitiesRepo;
        private readonly IRepository<E.ActivityConnections> _activityConnectionsRepo;

        private List<KeyValuePair<int, int>> _activityBlockTrackingChangeIds = new List<KeyValuePair<int, int>>();
        private List<KeyValuePair<int, int>> _phaseTrackingChangeIds = new List<KeyValuePair<int, int>>();

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="assetTypeRepo"></param>
        /// <param name="knowledgeAssetRepo"></param>
        /// <param name="privateAssetRepo"></param>
        /// <param name="disciplineRepo"></param>
        /// <param name="activityBlockRepo"></param>
        /// <param name="containerItemsRepo"></param>
        /// <param name="excelService"></param>
        public KnowledgeAssetCloneAppService(
            IMapper mapper,
            IKnowledgeAssetRepository kaRepo,
            IKnowledgeAssetTransferService transferService,
            IPublishContentRepository<PE.ActivityBlocks> activityBlockRepo,
            IPublishContentRepository<PE.Connectors> connectorRepo,
            IProcessMapsAppService processMapsAppService,
            IProcessMapCommonAppService commonService,
            IRepository<E.ProcessMap> processMapsRepo,
            IRepository<E.SwimLanes> activityGroupsRepo,
            IRepository<E.Phases> phasesRepo,
            IRepository<E.ContentPhases> contentPhaseRepo,
            IRepository<E.ContentExportCompliances> contentExportCompliancesRepo,
            IRepository<E.ContentTags> contentTagsRepo,
            IRepository<E.NatureOfChange> natureOfChangeRepo,
            IRepository<E.PrivateAssets> privateAssetsRepo,
            IRepository<E.KpacksMap> kpackMaprepo,
            IRepository<E.ActivityBlocks> activitiesRepo,
            IRepository<E.ActivityConnections> activityConnectionsRepo,
            IPublishContentRepository<PE.UsersCache> userCacheRepo
            )
        {
            this._mapper = mapper;
            this._kaRepo = kaRepo;
            this._transferService = transferService;
            this._activityBlockPublishedRepo = activityBlockRepo;
            this._connectorRepo = connectorRepo;

            this._commonService = commonService;
            this._processMapsAppService = processMapsAppService;
            this._processMapsRepo = processMapsRepo;
            this._swimLaneRepo = activityGroupsRepo;
            this._phasesRepo = phasesRepo;
            this._contentPhasesRepo = contentPhaseRepo;
            this._contentExportCompliancesRepo = contentExportCompliancesRepo;
            this._contentTagsRepo = contentTagsRepo;
            this._natureOfChangeRepo = natureOfChangeRepo;
            this._privateAssetsRepo = privateAssetsRepo;
            this._kpackMapRepo = kpackMaprepo;
            this._processMapsAppService = processMapsAppService;
            this._activitiesRepo = activitiesRepo;
            this._activityConnectionsRepo = activityConnectionsRepo;
            this._userCacheRepo = userCacheRepo;
        }

        public async Task<M.RevisionCheckingResult> IsAbleToReviseAsync(ProcessMapsReviseModel reviseModel)
        {
            var result = new RevisionCheckingResult();
            var knowledgeAsset = await this._kaRepo.GetKnowledgeAssetsByIdNoTrackingAsync(reviseModel.Id).ConfigureAwait(false);
            if (knowledgeAsset == null)
            {
                result.IsAbleToRevise = false;
                result.Message = $"KnowledgeAssetId {reviseModel.Id} is not found.";
            }
            else
            {
                var nextVersion = knowledgeAsset.Version + 1;
                var processMapNextVersion = this._processMapsRepo.FindBy(x => x.ContentId == knowledgeAsset.ContentId && x.Version == nextVersion).FirstOrDefault();
                if (processMapNextVersion == null)
                {
                    result.IsAbleToRevise = true;
                }
                else
                {
                    result.IsAbleToRevise = false;
                    var revisedUser = this._userCacheRepo.FindBy(u => u.Email == processMapNextVersion.CreatedUser).FirstOrDefault()?.DisplayName;
                    result.Message = $"Content has already been revised by {revisedUser}";
                }
            }
            return result;
        }

        /// <summary>
        /// ReviseStepFlowAsync
        /// </summary>
        /// <param name="fromKnowledgeAssetId"></param>
        /// <returns></returns>
        public async Task<M.ProcessMapModel> ReviseStepFlowAsync(ProcessMapsReviseModel reviseModel)
        {
            var fromKnowledgeAsset = await this._kaRepo.GetKnowledgeAssets_ForCloneAsyn(reviseModel.Id).ConfigureAwait(false);
            return await this.CloneStepFlowAsync(fromKnowledgeAsset, reviseModel.CreatedUser, isRevise: true).ConfigureAwait(false);
        }

        public async Task<M.ProcessMapModel> ReviseStepAsync(ProcessMapsReviseModel reviseModel)
        {
            var fromKnowledgeAsset = await this._kaRepo.GetKnowledgeAssets_ForCloneAsyn(reviseModel.Id).ConfigureAwait(false);
            return await this.ClonePublicStepAsync(fromKnowledgeAsset, reviseModel.CreatedUser, isRevise: true).ConfigureAwait(false);
        }

        /// <summary>
        /// SaveAsStepFlowAsync
        /// </summary>
        /// <param name="fromKnowledgeAssetId"></param>
        /// <returns></returns>
        public async Task<M.ProcessMapModel> SaveAsStepFlowAsync(ProcessMapsSaveAsModel saveAsModel)
        {
            var fromKnowledgeAsset = await this._kaRepo.GetKnowledgeAssets_ForCloneAsyn(saveAsModel.Id).ConfigureAwait(false);
            return await this.CloneStepFlowAsync(fromKnowledgeAsset, saveAsModel.CreatedUser, isRevise: false).ConfigureAwait(false);
        }

        public async Task<M.ProcessMapModel> SaveAsStepAsync(ProcessMapsSaveAsModel saveAsModel)
        {
            var fromKnowledgeAsset = await this._kaRepo.GetKnowledgeAssets_ForCloneAsyn(saveAsModel.Id).ConfigureAwait(false);
            return await this.ClonePublicStepAsync(fromKnowledgeAsset, saveAsModel.CreatedUser, isRevise: false).ConfigureAwait(false);
        }

        /// <summary>
        /// CloneStepFlowAsync
        /// </summary>
        /// <param name="fromKnowledgeAsset"></param>
        /// <param name="isRevise"></param>
        /// <returns></returns>
        private async Task<M.ProcessMapModel> CloneStepFlowAsync(PE.KnowledgeAssets fromKnowledgeAsset, string createdUser, bool isRevise)
        {
            var fromStepFlow = this._transferService.TransferKnowledgeAsset_To_ProcessMap(fromKnowledgeAsset);
            var fromSwimLane = this._mapper.Map<List<E.SwimLanes>>(fromKnowledgeAsset.SwimLanes).FirstOrDefault();

            var newStepFlow = await this.CloneProcessMapProperties(fromStepFlow, createdUser, isRevise).ConfigureAwait(false);
            var newSwimLane = await this.CloneSwimLane(fromSwimLane, newStepFlow.Id).ConfigureAwait(false);
            await this.ClonePhases(fromKnowledgeAsset, newStepFlow.Id).ConfigureAwait(false);
            await this.CloneContentPhase(fromKnowledgeAsset, newStepFlow.Id).ConfigureAwait(false);
            await this.CloneContentTag(fromKnowledgeAsset, newStepFlow.Id).ConfigureAwait(false);
            await this.CloneNatureOfChange(fromKnowledgeAsset, newStepFlow, isRevise).ConfigureAwait(false);
            await this.CloneKpackMap(fromKnowledgeAsset, newStepFlow).ConfigureAwait(false);
            await this.CloneStepActivityBlocks(fromKnowledgeAsset, newSwimLane.Id, newStepFlow.Id).ConfigureAwait(false);
            await this.CloneActivityConnection().ConfigureAwait(false);

            return this._mapper.Map<M.ProcessMapModel>(newStepFlow);
        }

        /// <summary>
        /// ClonePublicStepAsync
        /// </summary>
        /// <param name="fromKnowledgeAsset"></param>
        /// <param name="isRevise"></param>
        /// <returns></returns>
        private async Task<M.ProcessMapModel> ClonePublicStepAsync(PE.KnowledgeAssets fromKnowledgeAsset, string createdUser, bool isRevise)
        {
            var fromStep = this._transferService.TransferKnowledgeAsset_To_ProcessMap(fromKnowledgeAsset);
            var newStep = await this.CloneProcessMapProperties(fromStep, createdUser, isRevise).ConfigureAwait(false);
            
            await this.ClonePhases(fromKnowledgeAsset, newStep.Id).ConfigureAwait(false);
            await this.CloneContentPhase(fromKnowledgeAsset, newStep.Id).ConfigureAwait(false);
            await this.CloneContentTag(fromKnowledgeAsset, newStep.Id).ConfigureAwait(false);
            await this.CloneNatureOfChange(fromKnowledgeAsset, newStep, isRevise).ConfigureAwait(false);
            await this.CloneKpackMap(fromKnowledgeAsset, newStep).ConfigureAwait(false);
            await this.CloneSwimLaneInStep(fromKnowledgeAsset, newStep).ConfigureAwait(false);
            await this.CloneActivityConnection().ConfigureAwait(false);

            return this._mapper.Map<M.ProcessMapModel>(newStep);
        }

        /// <summary>
        /// CloneStepFlowProperties
        /// </summary>
        /// <param name="fromProcessMap"></param>
        /// <param name="isRevise"></param>
        /// <returns></returns>
        private async Task<E.ProcessMap> CloneProcessMapProperties(E.ProcessMap fromProcessMap, string createdUser, bool isRevise)
        {
            fromProcessMap.Id = 0;
            if (!string.IsNullOrEmpty(createdUser))
            {
                fromProcessMap.CreatedUser = createdUser;
                fromProcessMap.Author = createdUser;
                fromProcessMap.LastUpdateUser = createdUser;
            }
            fromProcessMap.CreatedDateTime = DateTime.Now;
            fromProcessMap.LastUpdateDateTime = DateTime.Now;
            fromProcessMap.AssetStatusId = (int)EKSEnum.AssetStatuses.Draft;
            fromProcessMap.Version = 1;
            if (isRevise)
            {
                var sfHaveSameContentId = await this._processMapsRepo.FindByAsyn(x => x.ContentId == fromProcessMap.ContentId).ConfigureAwait(false);
                var latest = sfHaveSameContentId.OrderByDescending(o => o.Version).FirstOrDefault();
                fromProcessMap.Version = latest.Version + 1;
            }

            var newStepFlow = await this._processMapsRepo.AddAsyn(fromProcessMap).ConfigureAwait(false);

            var isSaveAs = !isRevise;
            if (isSaveAs)
            {
                newStepFlow.ContentId = await this._commonService.GenerateContentId(newStepFlow.Id, newStepFlow.AssetTypeId, newStepFlow.CreatedUser, newStepFlow.PrivateInd).ConfigureAwait(false);
            }

            return newStepFlow;
        }

        /// <summary>
        /// CloneSwimLane
        /// </summary>
        /// <param name="fromSwimLane"></param>
        /// <param name="toStepFlowId"></param>
        /// <returns></returns>
        private async Task<E.SwimLanes> CloneSwimLane(E.SwimLanes fromSwimLane, int toStepFlowId)
        {
            fromSwimLane.Id = 0;
            fromSwimLane.ProcessMapId = toStepFlowId;
            fromSwimLane.CreatedDateTime = DateTime.Now;
            fromSwimLane.LastUpdateDateTime = DateTime.Now;
            var newSwimLane = await _swimLaneRepo.AddAsyn(fromSwimLane);
            return newSwimLane;
        }

        /// <summary>
        /// ClonePhases
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="toStepFlowId"></param>
        /// <returns></returns>
        private async Task ClonePhases(PE.KnowledgeAssets knowledgeAsset, int toStepFlowId)
        {
            var phases = this._mapper.Map<List<E.Phases>>(knowledgeAsset.PhasesMap);
            foreach (var ph in phases)
            {
                var oldPhaseId = ph.Id;

                ph.Id = 0;
                ph.ProcessMapId = toStepFlowId;
                ph.CreatedDateTime = DateTime.Now;
                ph.LastUpdateDateTime = DateTime.Now;
                await _phasesRepo.AddAsyn(ph);

                var newPhaseId = ph.Id;
                _phaseTrackingChangeIds.Add(new KeyValuePair<int, int>(oldPhaseId, newPhaseId));
            }
        }

        /// <summary>
        /// CloneContentPhase
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="toStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneContentPhase(PE.KnowledgeAssets knowledgeAsset, int toStepFlowId)
        {
            var contentPhases = this._mapper.Map<List<E.ContentPhases>>(knowledgeAsset.AssetPhases);
            foreach (var content in contentPhases)
            {
                content.ContentPhaseId = 0;
                content.ContentId = toStepFlowId;
                content.TypeId = knowledgeAsset.AssetTypeCode == EKSEnum.PublishedAssetTypeCode.F.ToString()
                    ? (int)EKSEnum.AssetTypes.SF
                    : (int)EKSEnum.AssetTypes.SP;
                content.CreatedDateTime = DateTime.Now;
                content.LastUpdateDateTime = DateTime.Now;
                await _contentPhasesRepo.AddAsyn(content);
            }
        }

        /// <summary>
        /// CloneContentTag
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="toStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneContentTag(PE.KnowledgeAssets knowledgeAsset, int toStepFlowId)
        {
            var contentTags = this._mapper.Map<List<E.ContentTags>>(knowledgeAsset.AssetTags);
            foreach (var content in contentTags)
            {
                content.Id = 0;
                content.ContentId = toStepFlowId;
                content.TypeId = knowledgeAsset.AssetTypeCode == EKSEnum.PublishedAssetTypeCode.F.ToString()
                        ? (int)EKSEnum.AssetTypes.SF
                        : (int)EKSEnum.AssetTypes.SP;
                content.CreatedDateTime = DateTime.Now;
                content.LastUpdateDateTime = DateTime.Now;
                await _contentTagsRepo.AddAsyn(content);
            }
        }

        /// <summary>
        /// CloneNatureOfChange
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="newStepFlow"></param>
        /// <param name="isRevise"></param>
        /// <returns></returns>
        private async Task CloneNatureOfChange(PE.KnowledgeAssets knowledgeAsset, E.ProcessMap newStepFlow, bool isRevise = false)
        {
            var nocs = this._transferService.TransferAssetPartToNatureOfChange(knowledgeAsset.AssetParts);
            var userCache = this._userCacheRepo.FindBy(x => x.Email == newStepFlow.CreatedUser).FirstOrDefault();
            var noc = nocs?.FirstOrDefault();
            if (noc != null)
            {
                noc.NatureOfChangeId = 0;
                noc.ContentId = newStepFlow.ContentId;
                noc.ContentItemId = newStepFlow.Id;
                noc.CreatedDateTime = DateTime.Now;
                noc.LastUpdateDateTime = DateTime.Now;
                noc.Version = newStepFlow.Version;
                if (isRevise)
                {
                    noc.Definition = string.Format("Revision was created by {0}", userCache.DisplayName);
                }
                else
                {
                    noc.Definition = string.Format("Copy from {0}", knowledgeAsset.ContentId);
                }

                await this._natureOfChangeRepo.AddAsyn(noc).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// CloneKpackMap
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="toStepFlow"></param>
        /// <returns></returns>
        private async Task CloneKpackMap(PE.KnowledgeAssets knowledgeAsset, E.ProcessMap toStepFlow)
        {
            var kPackMaps = this._mapper.Map<List<E.KpacksMap>>(knowledgeAsset.ContainerItems);
            foreach (var kpm in kPackMaps)
            {
                kpm.KpacksMapId = 0;
                kpm.ParentContentAssetId = toStepFlow.ContentId;
                kpm.CreatedDateTime = DateTime.Now;
                kpm.LastUpdateDateTime = DateTime.Now;
                kpm.Version = toStepFlow.Version.Value;
                kpm.ParentContentTypeId = toStepFlow.AssetTypeId.Value;
                await this._kpackMapRepo.AddAsyn(kpm);
            }
        }

        /// <summary>
        /// CloneContentExportCompliances
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="toStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneContentExportCompliances(PE.KnowledgeAssets knowledgeAsset, int toStepFlowId)
        {
            var compliances = this._mapper.Map<List<E.ContentExportCompliances>>(knowledgeAsset.AssetExportCompliances);
            foreach (var cpl in compliances)
            {
                cpl.Id = 0;
                cpl.ContentId = toStepFlowId;
                cpl.TypeId = (int)EKSEnum.AssetTypes.SF;
                cpl.CreatedDateTime = DateTime.Now;
                cpl.LastUpdateDateTime = DateTime.Now;
                await this._contentExportCompliancesRepo.AddAsyn(cpl);
            }
        }

        /// <summary>
        /// CloneStepsInStepFlow
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <param name="newMainSwimLaneId"></param>
        /// <param name="fromStepFlowId"></param>
        /// <param name="newStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneStepsInStepFlow(PE.KnowledgeAssets knowledgeAsset, int newMainSwimLaneId, int fromStepFlowId, int newStepFlowId)
        {
            var fromMainSwimLaneId = knowledgeAsset.SwimLanes.FirstOrDefault().Id;
            var privateAssets = this._mapper.Map<List<E.PrivateAssets>>(knowledgeAsset.PrivateAssetsParentAsset);
            var fromStepIds = privateAssets.Select(x => x.ContentAssetId);
            foreach (var fromStepId in fromStepIds)
            {
                var newStep = await CloneStep(fromMainSwimLaneId, newMainSwimLaneId, fromStepId, newStepFlowId);

                var fromPrivateAsset = privateAssets.FirstOrDefault(x => x.ContentAssetId == fromStepId);
                await ClonePrivateAsset(fromPrivateAsset, newStep.Id, newStepFlowId);
            }
        }

        /// <summary>
        /// ClonePrivateAsset
        /// </summary>
        /// <param name="privateAsset"></param>
        /// <param name="newStepId"></param>
        /// <param name="newStepFlowId"></param>
        /// <returns></returns>
        private async Task ClonePrivateAsset(E.PrivateAssets privateAsset, int newStepId, int newStepFlowId)
        {
            privateAsset.ContentAssetId = newStepId;
            privateAsset.ParentContentAssetId = newStepFlowId;
            privateAsset.CreatedDateTime = DateTime.Now;
            privateAsset.LastUpdateDateTime = DateTime.Now;
            await _privateAssetsRepo.AddAsyn(privateAsset);
        }

        /// <summary>
        /// CloneStep
        /// </summary>
        /// <param name="fromMainSwimLaneId"></param>
        /// <param name="newMainSwimLaneId"></param>
        /// <param name="fromStepId"></param>
        /// <param name="newStepFlowId"></param>
        /// <returns></returns>
        private async Task<E.ProcessMap> CloneStep(int fromMainSwimLaneId, int newMainSwimLaneId, int fromStepId, int newStepFlowId)
        {
            var kaStep = await this._kaRepo.GetKnowledgeAssets_ForClone_StepAsyn(fromStepId);
            var step = this._transferService.TransferKnowledgeAsset_To_ProcessMap(kaStep);
            var fromVersion = step.Version;
            var fromContentId = step.ContentId;

            step.Id = 0;
            step.CreatedDateTime = DateTime.Now;
            step.LastUpdateDateTime = DateTime.Now;
            step.AssetStatusId = (int)EKSEnum.AssetStatuses.Draft;

            var newStep = await _processMapsRepo.AddAsyn(step);
            newStep.ContentId = await _commonService.GenerateContentId(newStep.Id, newStep.AssetTypeId, newStep.CreatedUser, newStep.PrivateInd);

            // Each step is stored in 2 table: ProcessMap and ActivityBlock
            await CloneStepItSelfInActivityBlock(fromMainSwimLaneId, newMainSwimLaneId, fromContentId, newStep.ContentId, newStepFlowId);

            await CloneContentTag(kaStep, newStep.Id);
            await CloneContentPhase(kaStep, newStep.Id);
            await ClonePhases(kaStep, newStep.Id);
            await CloneKpackMap(kaStep, newStep);
            await CloneSwimLaneInStep(kaStep, newStep);

            return newStep;
        }

        /// <summary>
        /// CloneStepItSelfInActivityBlock
        /// </summary>
        /// <param name="fromMaiSwimlaneId"></param>
        /// <param name="newMainSwimLaneId"></param>
        /// <param name="fromStepContentId"></param>
        /// <param name="newStepContentId"></param>
        /// <param name="newStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneStepItSelfInActivityBlock(int fromMaiSwimlaneId, int newMainSwimLaneId, string fromStepContentId, string newStepContentId, int newStepFlowId)
        {
            var kaStepActivityBlock = _activityBlockPublishedRepo.FindAllNoTracking(x => x.SwimLaneId == fromMaiSwimlaneId
               && x.AssetContentId == fromStepContentId && x.ActivityBlockTypeId == (int)EKSEnum.ActivityBlockTypes.Step).FirstOrDefault();
            var stepActivityBlock = this._mapper.Map<E.ActivityBlocks>(kaStepActivityBlock);

            if (stepActivityBlock != null)
            {
                await CloneActivityBlock(stepActivityBlock, newStepFlowId, newMainSwimLaneId);
            }
        }

        /// <summary>
        /// CloneStepActivityBlocks
        /// </summary>
        /// <param name="fromMaiSwimlaneId"></param>
        /// <param name="newMainSwimLaneId"></param>
        /// <param name="newStepFlowId"></param>
        /// <returns></returns>
        private async Task CloneStepActivityBlocks(PE.KnowledgeAssets stepFlow, int newMainSwimLaneId, int newStepFlowId)
        {
            var fromMaiSwimlaneId = stepFlow.SwimLanes.FirstOrDefault().Id;
            var kaStepActivityBlocks = _activityBlockPublishedRepo.FindAllNoTracking(x => x.SwimLaneId == fromMaiSwimlaneId 
                && x.ActivityBlockTypeId == (int)EKSEnum.ActivityBlockTypes.Step);
            var stepActivityBlocks = this._mapper.Map<List<E.ActivityBlocks>>(kaStepActivityBlocks);

            foreach (var stepActivityBlock in stepActivityBlocks)
            {
                await CloneActivityBlock(stepActivityBlock, newStepFlowId, newMainSwimLaneId);
            }
        }

        /// <summary>
        /// CloneActivityBlock
        /// </summary>
        /// <param name="fromActivityBlock"></param>
        /// <param name="newProcessMapId"></param>
        /// <param name="newStepContentId"></param>
        /// <param name="newSwimLaneId"></param>
        /// <returns></returns>
        private async Task CloneActivityBlock(E.ActivityBlocks fromActivityBlock, int newProcessMapId, int newSwimLaneId)
        {
            var oldActivityBlockId = fromActivityBlock.Id;

            fromActivityBlock.Id = 0;
            fromActivityBlock.ProcessMapId = newProcessMapId;
            fromActivityBlock.SwimLaneId = newSwimLaneId;
            fromActivityBlock.PhaseId = this._phaseTrackingChangeIds.FirstOrDefault(x => x.Key == fromActivityBlock.PhaseId).Value;
            if (fromActivityBlock.PhaseId == 0)
            {
                fromActivityBlock.PhaseId = null;
            }
            
            await this._activitiesRepo.AddAsyn(fromActivityBlock).ConfigureAwait(false);
            var newActivityBlockId = fromActivityBlock.Id;

            _activityBlockTrackingChangeIds.Add(new KeyValuePair<int, int>(oldActivityBlockId, newActivityBlockId));
        }

        /// <summary>
        /// CloneSwimLaneInStep
        /// </summary>
        /// <param name="kaStep"></param>
        /// <param name="newStep"></param>
        /// <returns></returns>
        private async Task CloneSwimLaneInStep(PE.KnowledgeAssets kaStep, E.ProcessMap newStep)
        {
            var fromSwimLanes = this._mapper.Map<List<E.SwimLanes>>(kaStep.SwimLanes);
            foreach (var fromSwimLane in fromSwimLanes)
            {
                var fromSwimLaneId = fromSwimLane.Id;
                var newSwimLane = await CloneSwimLane(fromSwimLane, newStep.Id);

                var publishedAB = _activityBlockPublishedRepo.FindAllNoTracking(x => x.SwimLaneId == fromSwimLaneId);
                var fromActivityBlocks = this._mapper.Map<List<E.ActivityBlocks>>(publishedAB);
                foreach (var act in fromActivityBlocks)
                {
                    await CloneActivityBlock(act, newStep.Id, newSwimLane.Id);
                }
            }
        }

        /// <summary>
        /// CloneActivityConnection
        /// </summary>
        /// <returns></returns>
        private async Task CloneActivityConnection()
        {
            if (_activityBlockTrackingChangeIds.Count == 0)
            {
                return;
            }

            var fromActivityBlockIds = _activityBlockTrackingChangeIds.Select(x => x.Key).ToList();
            var connectionsPublisheds = _connectorRepo.FindBy(x => fromActivityBlockIds.Contains(x.ChildActivityBlockId)).ToList();
            var fromActivityConnections = _mapper.Map<List<E.ActivityConnections>>(connectionsPublisheds);
            foreach (var actConnection in fromActivityConnections)
            {
                actConnection.Id = 0;
                actConnection.CreatedDateTime = DateTime.Now;
                actConnection.LastUpdateDateTime = DateTime.Now;
                actConnection.ActivityBlockId = _activityBlockTrackingChangeIds.FirstOrDefault(x => x.Key == actConnection.ActivityBlockId).Value;

                if (actConnection.PreviousActivityBlockId != null)
                {
                    actConnection.PreviousActivityBlockId = _activityBlockTrackingChangeIds.FirstOrDefault(x => x.Key == actConnection.PreviousActivityBlockId).Value;
                }

                await _activityConnectionsRepo.AddAsyn(actConnection);
            }
        }
    }
}
