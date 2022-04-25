namespace EKS.ProcessMaps.Business
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    public class PhasesAppService : IPhasesAppService
    {
        private readonly IRepository<Phases> _phasesRepo;
        private readonly IMapper _mapper;

        public PhasesAppService(IRepository<Phases> phasesRepo, IMapper mapper)
        {
            this._mapper = mapper;
            this._phasesRepo = phasesRepo;
        }

        public async Task<PhasesModel> CreatePhasesAsync(PhasesModel phasesModel)
        {
            if (string.IsNullOrEmpty(phasesModel.Size))
            {
                phasesModel.Size = API.Constants.PhaseSize;
            }
            Phases resultData = await this._phasesRepo.AddAsyn(this._mapper.Map<Phases>(phasesModel)).ConfigureAwait(false);
            return this._mapper.Map<PhasesModel>(resultData);
        }

        public async Task<bool> DeletePhasesAsync(int id)
        {
            Phases resultData = this._phasesRepo.GetAllIncluding(x => x.ActivityBlocks).Where(x => x.Id == id).FirstOrDefault();

            if (resultData != null)
            {
                int result = await this._phasesRepo.DeleteAsyn(resultData).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<PhasesModel> GetPhasesByIdAsync(long id)
        {
            Phases resultData = await this._phasesRepo.FindAsync(x => x.Id == id).ConfigureAwait(false);

            return this._mapper.Map<PhasesModel>(resultData);
        }

        public async Task<IEnumerable<PhasesModel>> GetPhasesByProcessMapIdAsync(int? processMapId)
        {
            var phases = await this._phasesRepo.FindAllAsync(x => x.ProcessMapId == processMapId).ConfigureAwait(false);

            return this._mapper.Map<IEnumerable<PhasesModel>>(phases);
        }

        public async Task<PhasesModel> UpdatePhasesAsync(PhasesModel phasesModel)
        {
            int resultData = await this._phasesRepo.UpdateAsyn(this._mapper.Map<Phases>(phasesModel), phasesModel.Id).ConfigureAwait(false);
            Phases result = await this._phasesRepo.FindAsync(x => x.Id == phasesModel.Id).ConfigureAwait(false);
            return this._mapper.Map<PhasesModel>(result);
        }
    }
}
