namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IActivityGroupsAppService
    /// </summary>
    public interface IActivityGroupsAppService
    {
        Task<SwimLanesModel> CreateActivityGroupsAsync(SwimLanesModel activityGroupsModel);

        Task<SwimLanesModel> UpdateActivityGroupsAsync(SwimLanesModel activityGroupsModel);

        Task<List<SwimLanesModel>> UpdateAllActivityGroupsAsync(List<SwimLanesModel> activityGroupsModel);

        Task<IEnumerable<SwimLanesModel>> UpdateActivityGroupsSequenceAsync(IEnumerable<SequenceUpdateModel> activityGroupsModel);

        Task<bool> DeleteActivityGroupsAsync(long id);
    }
}
