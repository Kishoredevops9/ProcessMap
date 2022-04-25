namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IProcessMapMetaAppService
    /// </summary>
    public interface IProcessMapMetaAppService
    {
        Task<ProcessMapMetaModel> CreateProcessMapMetaAsync(ProcessMapMetaModel processMapMetaModel);

        Task<ProcessMapMetaModel> UpdateProcessMapMetaAsync(ProcessMapMetaModel processMapMetaModel);

        Task<bool> DeleteProcessMapMetaAsync(long id);
    }
}
