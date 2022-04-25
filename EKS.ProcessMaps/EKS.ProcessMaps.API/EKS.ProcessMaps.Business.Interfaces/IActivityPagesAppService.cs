namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IActivityPagesAppService
    /// </summary>
    public interface IActivityPagesAppService
    {
        Task<IEnumerable<ActivityPageModel>> GetAllActivityPagesAsync();
    }
}
