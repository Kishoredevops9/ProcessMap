namespace EKS.ProcessMaps.Business.PublishedContent
{
    using EKS.ProcessMaps.Business.Interfaces.PublishedContent;
    using EKS.ProcessMaps.Models.PublishedContent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Published stepflow and publsihed step operation.
    /// </summary>
    public class ProcessMapAppService : IProcessMapAppService
    {
        public Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(string contentId, int? version)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(string contentId, int? version)
        {
            throw new NotImplementedException();
        }
    }
}
