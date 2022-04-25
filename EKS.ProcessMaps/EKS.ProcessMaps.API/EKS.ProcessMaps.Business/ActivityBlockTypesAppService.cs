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
    /// App service class for activity block types
    /// </summary>
    public class ActivityBlockTypesAppService : IActivityBlockTypesAppService
    {
        private readonly IRepository<ActivityBlockTypes> _activityBlockTypesRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor - ActivityBlockTypesAppService
        /// </summary>
        /// <param name="activityBlockTypesRepo"></param>
        /// <param name="mapper"></param>
        public ActivityBlockTypesAppService(
            IRepository<ActivityBlockTypes> activityBlockTypesRepo,
            IMapper mapper)
        {
            this._activityBlockTypesRepo = activityBlockTypesRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Get all activity block types
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<ActivityBlockTypesModel>> GetAllActivityBlockTypesAsync()
        {
            ICollection<ActivityBlockTypes> result = await this._activityBlockTypesRepo.GetAllAsyn().ConfigureAwait(false);
            return this._mapper.Map<IEnumerable<ActivityBlockTypesModel>>(result);
        }
    }
}
