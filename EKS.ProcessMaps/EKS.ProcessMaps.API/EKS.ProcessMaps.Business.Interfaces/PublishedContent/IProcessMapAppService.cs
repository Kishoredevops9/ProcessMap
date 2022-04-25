namespace EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    using EKS.ProcessMaps.Models.PublishedContent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IProcessMapAppService
    {
        Task<IEnumerable<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(string contentId, int? version);

        Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(string contentId, int? version);
    }
}
