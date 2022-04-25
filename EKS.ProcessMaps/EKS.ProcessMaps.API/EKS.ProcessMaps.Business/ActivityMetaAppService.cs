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

    //public class ActivityMetaAppService : IActivityMetaAppService
    //{
    //    private readonly IMapper _mapper;
    //    private readonly IRepository<ActivityMeta> _activityMetaRepo;
    //    private readonly IRepository<ProcessMap> _processMapsRepo;
    //    private readonly IRepository<ActivityBlocks> _activitiesRepo;
    //    private readonly IRepository<SwimLanes> _activityGroupsRepo;
    //    private readonly IRepository<ProcessMapMeta> _processMapMetaRepo;

    //    public ActivityMetaAppService(
    //        IRepository<ProcessMap> processMapsRepo,
    //        IMapper mapper,
    //        IRepository<ActivityBlocks> activitiesRepo,
    //        IRepository<SwimLanes> activityGroupsRepo,
    //        IRepository<ProcessMapMeta> processMapMetaRepo,
    //        IRepository<ActivityMeta> activityMetaRepo)
    //    {
    //        this._processMapsRepo = processMapsRepo;
    //        this._mapper = mapper;
    //        this._activitiesRepo = activitiesRepo;
    //        this._activityGroupsRepo = activityGroupsRepo;
    //        this._processMapMetaRepo = processMapMetaRepo;
    //        this._activityMetaRepo = activityMetaRepo;
    //    }

    //    public async Task<ActivityMetaModel> CreateActivityMetaAsync(ActivityMetaModel activityMetaModel)
    //    {
    //        ActivityMeta resultData = await this._activityMetaRepo.AddAsyn(this._mapper.Map<ActivityMeta>(activityMetaModel)).ConfigureAwait(false);
    //        return this._mapper.Map<ActivityMetaModel>(resultData);
    //    }

    //    public async Task<bool> DeleteActivityMetaAsync(long id)
    //    {
    //        ActivityMeta resultData = this._activityMetaRepo.FindBy(x => x.Id == id).FirstOrDefault();
    //        if (resultData != null)
    //        {
    //            int result = await this._activityMetaRepo.DeleteAsyn(resultData).ConfigureAwait(false);
    //            return true;
    //        }
    //        else
    //        {
    //            return false;
    //        }
    //    }
    //}
}
