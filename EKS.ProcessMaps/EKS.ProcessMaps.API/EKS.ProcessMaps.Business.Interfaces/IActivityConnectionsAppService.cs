namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IActivityConnectionsAppService
    /// </summary>
    public interface IActivityConnectionsAppService
    {
        Task<ActivityConnectionsModel> CreateActivityConnectionsAsync(ActivityConnectionsModel activityConnectionsModel);

        Task<ActivityConnectionsModel> UpdateActivityConnectionsAsync(ActivityConnectionsModel activityConnectionsModel);

        Task<bool> DeleteActivityConnectionsAsync(long id);
    }
}
