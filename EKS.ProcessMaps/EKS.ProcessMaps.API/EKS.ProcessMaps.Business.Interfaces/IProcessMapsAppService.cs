namespace EKS.ProcessMaps.Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IProcessMapsAppService
    /// </summary>
    public interface IProcessMapsAppService
    {
        Task<IEnumerable<ProcessMapModel>> GetAllProcessMapsAsync();

        Task<ProcessMapInputOutputModel> CreateProcessMapAsync(ProcessMapModel processMapModel);

        Task<ProcessMapModel> UpdateProcessMapAsync(ProcessMapModel processMapModel);

        Task<ProcessMapModel> UpdatePropertiesInProcessMapAsync(ProcessMapModel processMapModel);

        Task<ProcessMapModel> UpdateProcessMapStatusAsync(ProcessMapModel processMapModel);

        Task<bool> DeleteProcessMapAsync(long id);

        [Obsolete("This method is depricated")]
        Task<IEnumerable<ProcessMapModel>> GetAllProcessMapsByUserIdAsync(string userId);

        Task<ProcessMapModel> GetProcessMapByIdAsync(long id);
        
        Task<ProcessMapModel> GetProcessMapByContentId(string contentId, int version);

        Task<IEnumerable<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(int id, string contentId, int? version);

        Task<IEnumerable<StepModel>> GetStepByIdOrContentIdAsync(int id, string contentId, int? version);

        Task<ProcessMapModel> CreateStepAsync(ActivityBlocksModel processMapModel);

        Task<ProcessMapsPurposeModel> UpdateProcessMapPurposeAsync(ProcessMapsPurposeModel model);

        Task<bool> DeleteStep(long id);
        Task<bool> RemoveStepFromStepFlowAsync(int stepFlowId, string stepContentId);

        Task<int?> GetProcessMapIdByContentIdAndVersionAsync(string contentId, int version);
        
        Task<ProcessMapModel> GetProcessMapFlowViewByIdAsync(long id, string contentId, int version);
        ActivityPageAllOutputModel GetActivityPageByContentId(string contentId);
    }
}
