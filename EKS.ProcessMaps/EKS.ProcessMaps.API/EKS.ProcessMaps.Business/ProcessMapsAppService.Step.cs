namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoMapper;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using EKS.Common.Logging;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using EksEnum = EKS.ProcessMaps.Helper.Enum;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// ProcessMapsAppService
    /// </summary>
    public partial class ProcessMapsAppService
    {

        /// <summary>
        /// GetStepByIdOrContentIdAsync.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(int id, string contentId, int? version)
        {
            ICollection<ProcessMap> allSteps = null;
            if (id > 0)
            {
                allSteps = await this._processMapsRepo.FindAllAsync(x => x.Id == id).ConfigureAwait(false);
            }
            else if (!string.IsNullOrEmpty(contentId) && !version.HasValue)
            {
                allSteps = await this._processMapsRepo.FindAllAsync(x => x.ContentId == contentId).ConfigureAwait(false);
            }
            else if (!string.IsNullOrEmpty(contentId) && version.HasValue)
            {
                allSteps = await this._processMapsRepo.FindAllAsync(x => x.ContentId == contentId && x.Version == version).ConfigureAwait(false);
            }

            var processMapModels = this._mapper.Map<IEnumerable<ProcessMapModel>>(allSteps);
            var steps = this._mapper.Map<IEnumerable<StepModel>>(processMapModels)?.ToList();
            var processMap = processMapModels?.FirstOrDefault();
            var step = steps?.FirstOrDefault();

            if (steps == null || steps.Count == 0)
            {
                return steps;
            }

            var swimLaneStep = this.GetSwimLanesByProcessMapId(processMap.Id);//contentId - modify
            step.StepId = processMap.Id;
            step.StepTitle = processMap.Title;
            step.StepActivityBlockId = this._activitiesRepo.FindBy(act => act.AssetContentId == processMap.ContentId)?.FirstOrDefault()?.Id;
            step.StepContentId = processMap.ContentId;
            step.BaseType = "P";

            if (swimLaneStep?.Count > 0)
            {
                swimLaneStep.ForEach(sl =>
                {
                    if (step.StepSwimLanes == null)
                    {
                        step.StepSwimLanes = new List<StepSwimlane>();
                    }
                    var activityBlock = this._activitiesRepo.FindBy(x => x.SwimLaneId == sl.Id);
                    var activityBlockModel = this._mapper.Map<List<ActivityBlocksModel>>(activityBlock);
                    activityBlockModel.ForEach(ab =>
                    {
                        ab.AssetStatus = this._pubCommonService.GetKnowledgeAssetStatus(ab.AssetContentId, ab.Version ?? 0);

                        if (ab.ActivityContainers == null)
                        {
                            ab.ActivityContainers = new List<ActivityContainerModel>();
                        }

                        if (!string.IsNullOrEmpty(ab.AssetContentId))
                        {
                            var content = this.GetActivityPageByContentId(ab.AssetContentId)?.Content;
                            if (content != null)
                            {
                                ab.ActivityContainers.AddRange(content);
                            }
                        }
                    });
                    step.StepSwimLanes.Add(new StepSwimlane
                    {
                        SwimLaneId = sl.Id,
                        StepId = processMap.Id,
                        BaseType = "SL",
                        SwimLaneTitle = sl.DisciplineText,
                        DisciplineText = sl.DisciplineText,
                        DisciplineId = sl.DisciplineId,
                        BorderColor = sl.BorderColor,
                        BorderWidth = sl.BorderWidth,
                        BorderStyle = sl.BorderStyle,
                        Color = sl.Color,
                        ActivityBlocks = activityBlockModel.OrderBy(x => x.SequenceNumber).ToList(),
                    });
                });
            }

            return steps;
        }

        private async Task<ProcessMap> AddStep(ActivityBlocksModel activityBlock)
        {
            var step = await this._processMapsRepo.GetAsync(activityBlock.ProcessMapId.Value);// this.GetProcessMapByIdAsync(activityBlock.ProcessMapId.Value);
            step.PrivateInd = true;
            step.AssetTypeId = (int)EksEnum.AssetTypes.SP;
            step.Title = activityBlock.Name;
            step.Id = 0;

            await this._processMapsRepo.AddAsyn(step);

            var contentPhases = await _contentPhasesRepo.FindAllAsync(x => x.ContentId == activityBlock.ProcessMapId);
            foreach (var contentPhase in contentPhases)
            {
                var contentPhaseEntity = new ContentPhases()
                {
                    ContentId = step.Id,
                    TypeId = step.AssetTypeId,
                    PhaseId = contentPhase.PhaseId
                };
                this._contentPhasesRepo.Add(contentPhaseEntity);
            }

            var contentTags = await _contentTagsRepo.FindAllAsync(x => x.ContentId == activityBlock.ProcessMapId);
            foreach (var tag in contentTags)
            {
                var tagEntity = new ContentTags()
                {
                    ContentId = step.Id,
                    TypeId = step.AssetTypeId ?? 14,
                    TagId = tag.TagId
                };
                this._contentTagsRepo.Add(tagEntity);
            }

            return step;
        }

        private async Task<Phases> AddPhase(int? processMapId)
        {
            Phases phases = new Phases();
            phases.ProcessMapId = processMapId;
            phases.Caption = "Phase-1";
            phases.Name = "Phase-1";

            return await this._phasesRepo.AddAsyn(phases).ConfigureAwait(false);
        }

        private async Task<Phases> AddPhaseStep(ProcessMap step)
        {
            Phases phasesModelStep = new Phases();
            phasesModelStep.ProcessMapId = step.Id;
            phasesModelStep.Caption = "Phase-1";
            phasesModelStep.Name = "Phase-1";

            return await this._phasesRepo.AddAsyn(phasesModelStep).ConfigureAwait(false);
        }

        private async Task<ActivityBlocks> AddActivityBlock(ActivityBlocksModel activityBlock, ProcessMap step, int? phaseId)
        {
            var stepFlowId = activityBlock.ProcessMapId;
            var swimLaneId = activityBlock.SwimLaneId;
            if (swimLaneId == null || swimLaneId == 0)
            {
                swimLaneId = _activityGroupsRepo.Find(x => x.ProcessMapId == stepFlowId)?.Id;
            }

            var bigest = _activitiesRepo.FindBy(x => x.ProcessMapId == stepFlowId && x.SequenceNumber != null)
                .Select(x => x.SequenceNumber).ToList().DefaultIfEmpty(0).Max();

            activityBlock.SequenceNumber = bigest + 1;
            activityBlock.ProcessMapId = stepFlowId;
            activityBlock.SwimLaneId = swimLaneId;
            activityBlock.ActivityTypeId = 8;
            activityBlock.PhaseId = phaseId;
            activityBlock.AssetContentId = step.ContentId;
            if (activityBlock.PhaseId == 0)
            {
                activityBlock.PhaseId = null;
            }

            return await this._activitiesRepo.AddAsyn(this._mapper.Map<ActivityBlocks>(activityBlock)).ConfigureAwait(false);
        }

        private async Task<NatureOfChange> AddNatureOfChange(ProcessMap step, ProcessMapInputOutputModel stepModel)
        {
            NatureOfChange nocData = new NatureOfChange();
            nocData.ContentId = string.IsNullOrEmpty(step.ContentId) ? stepModel.ContentId : step.ContentId;
            nocData.ContentItemId = Convert.ToInt32(step.Id);
            nocData.AssetTypeId = stepModel.AssetTypeId;
            nocData.Version = stepModel.Version;
            nocData.NocdateTime = stepModel.CreatedDateTime;
            nocData.Definition = "Initial Version";
            nocData.CreatedDateTime = stepModel.CreatedDateTime ?? DateTime.Today;
            nocData.CreatedUser = stepModel.CreatedUser;

            return await this.natureOfChangeRepo.AddAsyn(nocData).ConfigureAwait(false);
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

        /// <summary>
        /// CreateStepAsync.
        /// </summary>
        /// <param name="activityBlock"></param>
        /// <returns></returns>
        public async Task<ProcessMapModel> CreateStepAsync(ActivityBlocksModel activityBlock)
        {
            if (activityBlock.ProcessMapId <= 0)
            {
                return new ProcessMapModel();
            }

            var stepFlowId = activityBlock.ProcessMapId;

            // Create Step
            var step = await AddStep(activityBlock).ConfigureAwait(false);
            var stepModel = this._mapper.Map<ProcessMapInputOutputModel>(step);
            var contentId = await this.GenerateContentId(step, stepModel).ConfigureAwait(false);
            step.ContentId = contentId;

            //var phase = await AddPhase(activityBlock).ConfigureAwait(false);
            var block = await AddActivityBlock(activityBlock, step, activityBlock.PhaseId).ConfigureAwait(false);
            await AddNatureOfChange(step, stepModel).ConfigureAwait(false);
            await AddPrivateAssets(step.Id, stepFlowId);
            await AddPhaseStep(step);

            var returnModel = this._mapper.Map<ProcessMapModel>(step);
            if (returnModel.ActivityBlocks == null)
            {
                returnModel.ActivityBlocks = new List<ActivityBlocksModel>();
            }
            returnModel.ActivityBlocks.Add(this._mapper.Map<ActivityBlocksModel>(block));
            return returnModel;
        }

        /// <summary>
        /// DeleteStep.
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> DeleteStep(long id)
        {
            if (id <= 0)
            {
                return false;
            }

            var activityBlocks = this._activitiesRepo.FindBy(x => x.ProcessMapId == id).ToList();
            foreach (var activityBlock in activityBlocks)
            {
                await this._activitiesRepo.DeleteAsyn(activityBlock);
            }

            var privateAsset = this._privateAssetsRepo.FindBy(x => x.ContentAssetId == id)?.FirstOrDefault();
            if (privateAsset != null)
            {
                await this._privateAssetsRepo.DeleteAsyn(privateAsset);
            }

            var phases = this._phasesRepo.FindBy(x => x.ProcessMapId == id).ToList();
            foreach (var phase in phases)
            {
                await this._phasesRepo.DeleteAsyn(phase);
            }

            var nOCs = this.natureOfChangeRepo.FindBy(x => x.ContentItemId == id).ToList();
            foreach (var noc in nOCs)
            {
                await this.natureOfChangeRepo.DeleteAsyn(noc);
            }

            var swimLanes = this._activityGroupsRepo.FindBy(x => x.ProcessMapId == id).ToList();
            foreach (var sl in swimLanes)
            {
                await this._activityGroupsRepo.DeleteAsyn(sl);
            };

            var processMap = this._processMapsRepo.FindBy(x => x.Id == id)?.FirstOrDefault();
            if (processMap != null)
            {
                await this._processMapsRepo.DeleteAsyn(processMap);
                return true;
            }

            return true;
        }

        /// <summary>
        /// RemoveStepFromStepFlowAsync
        /// </summary>
        /// <param name="stepFlowId"></param>
        /// <param name="stepContentId"></param>
        /// <returns></returns>
        public async Task<bool> RemoveStepFromStepFlowAsync(int stepFlowId, string stepContentId)
        {
            var activityBlocks = await this._activitiesRepo.FindByAsyn(x => x.ProcessMapId == stepFlowId && x.AssetContentId == stepContentId).ConfigureAwait(false);
            foreach (var activityBlock in activityBlocks)
            {
                await this.RemoveConnections(activityBlock.Id).ConfigureAwait(false);
                await this._activitiesRepo.DeleteAsyn(activityBlock).ConfigureAwait(false);
            }

            return activityBlocks.Any();
        }

        private async Task RemoveConnections(int activityBlockId)
        {
            var connections = await this._activityConnectionsRepo.FindByAsyn(x => x.ActivityBlockId == activityBlockId || x.PreviousActivityBlockId == activityBlockId).ConfigureAwait(false);
            foreach (var conn in connections)
            {
                await this._activityConnectionsRepo.DeleteAsyn(conn).ConfigureAwait(false);
            }
        }
    }
}