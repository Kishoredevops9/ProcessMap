using EKS.ProcessMaps.Models;
using System.Threading.Tasks;
using M = EKS.ProcessMaps.Models;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IKnowledgeAssetCloneAppService
    {
        Task<M.RevisionCheckingResult> IsAbleToReviseAsync(ProcessMapsReviseModel reviseModel);
        Task<M.ProcessMapModel> ReviseStepFlowAsync(ProcessMapsReviseModel reviseModel);
        Task<M.ProcessMapModel> ReviseStepAsync(ProcessMapsReviseModel reviseModel);
        Task<M.ProcessMapModel> SaveAsStepFlowAsync(ProcessMapsSaveAsModel saveAsModel);
        Task<M.ProcessMapModel> SaveAsStepAsync(ProcessMapsSaveAsModel saveAsModel);
    }
}
