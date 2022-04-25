using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IKnowledgeAssetTransferService
    {
        ProcessMapModel TransferKnowledgeAsset_To_ProcessMapModel(PE.KnowledgeAssets ka);
        ProcessMap TransferKnowledgeAsset_To_ProcessMap(PE.KnowledgeAssets ka);
        List<NatureOfChange> TransferAssetPartToNatureOfChange(ICollection<PE.AssetParts> assetParts);
        StepFlowModel TransferKnowledgeAsset_To_StepFlowModel(PE.KnowledgeAssets ka);
        StepModel TransferKnowledgeAsset_To_StepModel(PE.KnowledgeAssets step);
        List<ActivityContainerModel> GetActivityContainerByActivityPageContentIdId(string activityPageContentId);
    }
}
