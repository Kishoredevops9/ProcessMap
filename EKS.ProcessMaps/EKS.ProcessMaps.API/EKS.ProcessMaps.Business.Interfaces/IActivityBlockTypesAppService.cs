namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IActivityBlockTypesAppService
    /// </summary>
    public interface IActivityBlockTypesAppService
    {
        Task<IEnumerable<ActivityBlockTypesModel>> GetAllActivityBlockTypesAsync();
    }
}
