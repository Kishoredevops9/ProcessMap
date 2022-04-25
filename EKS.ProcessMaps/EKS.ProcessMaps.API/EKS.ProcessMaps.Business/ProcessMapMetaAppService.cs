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

    public class ProcessMapMetaAppService : IProcessMapMetaAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ProcessMapMeta> _processMapMetaRepo;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IRepository<ActivityBlocks> _activitiesRepo;
        private readonly IRepository<SwimLanes> _activityGroupsRepo;

        public ProcessMapMetaAppService(
            IRepository<ProcessMap> processMapsRepo,
            IMapper mapper,
            IRepository<ActivityBlocks> activitiesRepo,
            IRepository<SwimLanes> activityGroupsRepo,
            IRepository<ProcessMapMeta> processMapMetaRepo)
        {
            this._processMapsRepo = processMapsRepo;
            this._mapper = mapper;
            this._activitiesRepo = activitiesRepo;
            this._activityGroupsRepo = activityGroupsRepo;
            this._processMapMetaRepo = processMapMetaRepo;
        }

        public async Task<ProcessMapMetaModel> CreateProcessMapMetaAsync(ProcessMapMetaModel processMapMetaModel)
        {
            ProcessMapMeta resultData = await this._processMapMetaRepo.AddAsyn(this._mapper.Map<ProcessMapMeta>(processMapMetaModel)).ConfigureAwait(false);
            return this._mapper.Map<ProcessMapMetaModel>(resultData);
        }

        public async Task<ProcessMapMetaModel> UpdateProcessMapMetaAsync(ProcessMapMetaModel processMapMetaModel)
        {
            int resultData = await this._processMapMetaRepo.UpdateAsyn(this._mapper.Map<ProcessMapMeta>(processMapMetaModel), processMapMetaModel.Id).ConfigureAwait(false);
            ProcessMapMeta result = this._processMapMetaRepo.FindBy(x => x.Id == processMapMetaModel.Id).FirstOrDefault();
            return this._mapper.Map<ProcessMapMetaModel>(result);
        }

        public async Task<bool> DeleteProcessMapMetaAsync(long id)
        {
            ProcessMapMeta resultData = this._processMapMetaRepo.FindBy(x => x.Id == id).FirstOrDefault();
            if (resultData != null)
            {
                int result = await this._processMapMetaRepo.DeleteAsyn(resultData).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
