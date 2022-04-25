namespace EKS.ProcessMaps.Business
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// App service class for activity pages
    /// </summary>
    public class ActivityPagesAppService : IActivityPagesAppService
    {
        private readonly IRepository<ActivityPage> _activityPagesRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor - ActivityPagesAppService
        /// </summary>
        /// <param name="activityPagesRepo"></param>
        /// <param name="mapper"></param>
        public ActivityPagesAppService(
            IRepository<ActivityPage> activityPagesRepo,
            IMapper mapper)
        {
            this._activityPagesRepo = activityPagesRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all activity pages
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ActivityPageModel>> GetAllActivityPagesAsync()
        {
            ICollection<ActivityPage> result = await this._activityPagesRepo.GetAllAsyn().ConfigureAwait(false);
            return this._mapper.Map<IEnumerable<ActivityPageModel>>(result);
        }
    }
}
