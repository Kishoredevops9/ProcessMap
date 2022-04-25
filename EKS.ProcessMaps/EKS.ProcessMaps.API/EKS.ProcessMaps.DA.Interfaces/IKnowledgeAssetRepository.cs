namespace EKS.ProcessMaps.DA.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Entities;
    using PE = EKS.ProcessMaps.Entities.PublishedContent;

    public interface IKnowledgeAssetRepository
    {
        Task<PE.KnowledgeAssets> GetKnowledgeAssetsByIdNoTrackingAsync(int id);
        Task<PE.KnowledgeAssets> GetKnowledgeAssets_NoStepAsyn(int id, string contentId, int version);
        Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForCloneAsyn(int knowledgeAssetId);
        Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForClone_StepAsyn(int knowledgeAssetStepId);
        Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForStepFlowAsyn(int id, string contentId, int version);
        PE.KnowledgeAssets GetKnowledgeAssets_ForStepAsyn(int id, string contentId, int version);
    }
}
