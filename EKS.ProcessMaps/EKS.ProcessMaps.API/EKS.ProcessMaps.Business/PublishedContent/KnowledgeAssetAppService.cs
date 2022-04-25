using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;
using EksEnum = EKS.ProcessMaps.Helper.Enum;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class KnowledgeAssetAppService : IKnowledgeAssetAppService
    {
        private readonly IKnowledgeAssetRepository _kaRepo;
        private readonly IKnowledgeAssetTransferService _transferService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="kaRepo"></param>
        /// <param name="transferService"></param>
        public KnowledgeAssetAppService(
            IKnowledgeAssetRepository kaRepo,
            IKnowledgeAssetTransferService transferService)
        {
            this._kaRepo = kaRepo;
            this._transferService = transferService;
        }

        /// <summary>
        /// GetProcessMapByContentId
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<ProcessMapModel> GetProcessMapByIdOrContentId(int id, string contentId, int version)
        {
            var knowledgeAsset = await this._kaRepo.GetKnowledgeAssets_NoStepAsyn(id, contentId, version);

            if (knowledgeAsset != null)
            {
                var processMapModel = this._transferService.TransferKnowledgeAsset_To_ProcessMapModel(knowledgeAsset);
                return processMapModel;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetProcessMapFlowViewByIdOrContentId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<ProcessMapModel> GetProcessMapFlowViewByIdOrContentId(int id, string contentId, int version)
        {
            var knowledgeAsset = await this._kaRepo.GetKnowledgeAssets_NoStepAsyn(id, contentId, version);

            if (knowledgeAsset != null)
            {
                var processMapModel = this._transferService.TransferKnowledgeAsset_To_ProcessMapModel(knowledgeAsset);

                return this.AddActivityContainerToActivityBlock(processMapModel);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetStepFlowContentIdAsync
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<List<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(int id, string contentId, int version)
        {
            var knowledgeAsset = await this._kaRepo.GetKnowledgeAssets_ForStepFlowAsyn(id, contentId, version);

            if (knowledgeAsset != null)
            {
                var stepFlowModel = this._transferService.TransferKnowledgeAsset_To_StepFlowModel(knowledgeAsset);
                stepFlowModel.SFSwimLanes = this.OrderBySequenceNumber(stepFlowModel.SFSwimLanes);
                return new List<StepFlowModel> { stepFlowModel };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// GetStepByContentIdAsync
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(int id, string contentId, int version)
        {
            var knowledgeAssetStep = this._kaRepo.GetKnowledgeAssets_ForStepAsyn(id, contentId, version);

            if (knowledgeAssetStep != null)
            {
                var stepModel = this._transferService.TransferKnowledgeAsset_To_StepModel(knowledgeAssetStep);
                stepModel = this.OrderByStepModel(stepModel);
                return new List<StepModel> { stepModel };
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// OrderBySequenceNumber
        /// </summary>
        /// <param name="swimLanes"></param>
        /// <returns></returns>
        private List<SFSwimLanesModel> OrderBySequenceNumber(List<SFSwimLanesModel> swimLanes)
        {
            foreach (var swimLane in swimLanes)
            {
                if (swimLane.Steps != null && swimLane.Steps.Count > 0)
                {
                    swimLane.Steps = swimLane?.Steps.OrderBy(x => x.SequenceNumber).ToList();
                    foreach (var step in swimLane.Steps)
                    {
                        if (step.StepSwimLanes != null && step.StepSwimLanes.Count > 0)
                        {
                            step.StepSwimLanes = step.StepSwimLanes.OrderBy(x => x.SequenceNumber).ToList();
                            foreach (var sw in step.StepSwimLanes)
                            {
                                if (sw.ActivityBlocks != null && sw.ActivityBlocks.Count > 0)
                                {
                                    sw.ActivityBlocks = sw.ActivityBlocks.OrderBy(x => x.SequenceNumber).ToList();
                                }
                            }
                        }
                    }
                }
            }

            return swimLanes;
        }

        /// <summary>
        /// OrderByStepModel
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        private StepModel OrderByStepModel(StepModel step)
        {
            if (step.StepSwimLanes != null && step.StepSwimLanes.Count > 0)
            {
                step.StepSwimLanes = step.StepSwimLanes.OrderBy(x => x.SequenceNumber).ToList();
                foreach (var sw in step.StepSwimLanes)
                {
                    if (sw.ActivityBlocks != null && sw.ActivityBlocks.Count > 0)
                    {
                        sw.ActivityBlocks = sw.ActivityBlocks.OrderBy(x => x.SequenceNumber).ToList();
                    }
                }
            }

            return step;
        }

        /// <summary>
        /// AddActivityContainerToActivityBlock
        /// </summary>
        /// <param name="processMap"></param>
        /// <returns></returns>
        private ProcessMapModel AddActivityContainerToActivityBlock(ProcessMapModel processMap)
        {
            foreach (var ab in processMap.ActivityBlocks)
            {
                if (ab.ActivityTypeId == (int)EksEnum.ActivityBlockTypes.Activity)
                {
                    if (!string.IsNullOrEmpty(ab.AssetContentId))
                    {
                        var content = this._transferService.GetActivityContainerByActivityPageContentIdId(ab.AssetContentId);
                        if (content != null)
                        {
                            if (ab.ActivityContainers == null)
                            {
                                ab.ActivityContainers = new List<ActivityContainerModel>();
                            }
                            ab.ActivityContainers.AddRange(content);
                        }
                    }
                }
            }

            return processMap;
        }
    }
}
