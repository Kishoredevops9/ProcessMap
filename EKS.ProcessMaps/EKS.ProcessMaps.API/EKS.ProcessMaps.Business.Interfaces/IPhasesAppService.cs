namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    public interface IPhasesAppService
    {
        Task<PhasesModel> CreatePhasesAsync(PhasesModel phasesModel);

        Task<PhasesModel> GetPhasesByIdAsync(long id);

        Task<IEnumerable<PhasesModel>> GetPhasesByProcessMapIdAsync(int? processMapId);

        Task<PhasesModel> UpdatePhasesAsync(PhasesModel phasesModel);

        Task<bool> DeletePhasesAsync(int id);
    }
}
