namespace EKS.ProcessMaps.Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface of IPublicStepsAppService
    /// </summary>
    public interface IPublicStepsAppService
    {
        /// <summary>
        /// Create public steps.
        /// </summary>
        /// <param name="processMapModel"></param>
        /// <returns></returns>
        Task<PublicStepsInputOutputModel> CreatePublicStepsAsync(ProcessMapModel processMapModel);

        /// <summary>
        /// Update purpose of public steps
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<PublicStepsPurposeModel> UpdatePublicStepsPurposeAsync(PublicStepsPurposeModel model);
    }
}
