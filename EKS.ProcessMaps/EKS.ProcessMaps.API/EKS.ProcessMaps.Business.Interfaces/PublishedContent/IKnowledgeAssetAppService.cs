using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IKnowledgeAssetAppService
    {
        Task<ProcessMapModel> GetProcessMapByIdOrContentId(int id, string contentId, int version);
        Task<List<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(int id, string contentId, int version);
        Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(int id, string contentId, int version);
        Task<ProcessMapModel> GetProcessMapFlowViewByIdOrContentId(int id, string contentId, int version);
    }
}
