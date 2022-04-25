namespace EKS.ProcessMaps.Business
{
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// App service class for activity connections
    /// </summary>
    public class ActivityConnectionsAppService : IActivityConnectionsAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ActivityConnections> _activityConnectionsRepo;

        /// <summary>
        /// Constructor - ActivityConnectionsAppService
        /// </summary>
        /// <param name="activityConnections"></param>
        /// <param name="mapper"></param>
        public ActivityConnectionsAppService(IRepository<ActivityConnections> activityConnections, IMapper mapper)
        {
            this._activityConnectionsRepo = activityConnections;
            this._mapper = mapper;
        }

        /// <summary>
        /// Create activity connections
        /// </summary>
        /// <param name="activityConnectionsModel"></param>
        /// <returns></returns>
        public async Task<ActivityConnectionsModel> CreateActivityConnectionsAsync(ActivityConnectionsModel activityConnectionsModel)
        {
            ActivityConnections resultData = await this._activityConnectionsRepo.AddAsyn(this._mapper.Map<ActivityConnections>(activityConnectionsModel)).ConfigureAwait(false);
            return this._mapper.Map<ActivityConnectionsModel>(resultData);
        }

        /// <summary>
        /// Delete activity connections on basis of id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteActivityConnectionsAsync(long id)
        {
            ActivityConnections resultData = this._activityConnectionsRepo.FindBy(x => x.Id == id).FirstOrDefault();

            if (resultData != null)
            {
                int result = await this._activityConnectionsRepo.DeleteAsyn(resultData).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Update activity connections
        /// </summary>
        /// <param name="activityConnectionsModel"></param>
        /// <returns></returns>
        public async Task<ActivityConnectionsModel> UpdateActivityConnectionsAsync(ActivityConnectionsModel activityConnectionsModel)
        {
            int resultData = await this._activityConnectionsRepo.UpdateAsyn(this._mapper.Map<ActivityConnections>(activityConnectionsModel), activityConnectionsModel.Id).ConfigureAwait(false);
            ActivityConnections result = this._activityConnectionsRepo.FindBy(x => x.Id == activityConnectionsModel.Id).FirstOrDefault();
            return this._mapper.Map<ActivityConnectionsModel>(result);
        }
    }
}
