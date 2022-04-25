namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// IActivityGroupsAppService
    /// </summary>
    public interface IActivitiesAppService
    {
        Task<ActivityBlocksModel> CreateActivitiesAsync(ActivityBlocksModel activitiesModel);

        Task<ActivityBlocksModel> UpdateActivitiesAsync(ActivityBlocksModel activitiesModel);

        Task<IEnumerable<ActivityBlocksModel>> UpdateActivitiesBlockSequenceAsync(IEnumerable<SequenceUpdateModel> sequenceUpdateModel);

        Task<bool> DeleteActivitiesAsync(long id);

        Task<IEnumerable<ActivityBlocksModel>> GetActivityBlocksByProcessMapAsync(long processMapId);
    }
}
