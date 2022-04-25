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

    /// <summary>
    /// CRUD operations of activity blocks.
    /// </summary>
    public class ActivitiesAppService : IActivitiesAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<ActivityBlocks> _activitiesRepo;
        private readonly IRepository<ActivityConnections> _activityConnectionsRepo;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IRepository<SwimLanes> _activityGroupsRepo;
        private readonly IRepository<ProcessMapMeta> _processMapMetaRepo;
        private readonly IProcessMapsAppService _processMapService;

        public ActivitiesAppService(
            IRepository<ProcessMap> processMapsRepo,
            IMapper mapper,
            IRepository<ActivityBlocks> activitiesRepo,
            IRepository<SwimLanes> activityGroupsRepo,
            IRepository<ProcessMapMeta> processMapMetaRepo,
            IRepository<ActivityConnections> activityConnectionsRepo,
            IProcessMapsAppService processMapService)
        {
            this._processMapsRepo = processMapsRepo;
            this._mapper = mapper;
            this._activitiesRepo = activitiesRepo;
            this._activityGroupsRepo = activityGroupsRepo;
            this._processMapMetaRepo = processMapMetaRepo;
            this._activityConnectionsRepo = activityConnectionsRepo;
            this._processMapService = processMapService;
        }

        public async Task<ActivityBlocksModel> CreateActivitiesAsync(ActivityBlocksModel activitiesModel)
        {
            var largest = _activitiesRepo.FindBy(x => x.SwimLaneId == activitiesModel.SwimLaneId && x.SequenceNumber != null)
                .Select(x => x.SequenceNumber).ToList().DefaultIfEmpty(0).Max();
            activitiesModel.SequenceNumber = largest + 1;
            if ((!activitiesModel.ProcessMapId.HasValue || activitiesModel.ProcessMapId == 0) && activitiesModel.SwimLaneId.HasValue)
            {
                var swimLane = await _activityGroupsRepo.GetAsync(activitiesModel.SwimLaneId.Value);
                activitiesModel.ProcessMapId = swimLane?.ProcessMapId;
            }

            var activityBlock = await this._activitiesRepo.AddAsyn(this._mapper.Map<ActivityBlocks>(activitiesModel)).ConfigureAwait(false);
            var activityBlockModel = this._mapper.Map<ActivityBlocksModel>(activityBlock);

            var content = this._processMapService.GetActivityPageByContentId(activityBlockModel.AssetContentId)?.Content;
            if (content != null)
            {
                activityBlockModel.ActivityContainers.AddRange(content);
            }

            return activityBlockModel;
        }

        public async Task<ActivityBlocksModel> UpdateActivitiesAsync(ActivityBlocksModel activitiesModel)
        {
            int resultData = await this._activitiesRepo.UpdateAsyn(this._mapper.Map<ActivityBlocks>(activitiesModel), activitiesModel.Id).ConfigureAwait(false);
            ActivityBlocksModel result = await this.GetActivitiesByIdAsync(activitiesModel.Id).ConfigureAwait(false);
            return this._mapper.Map<ActivityBlocksModel>(result);
        }

        public async Task<IEnumerable<ActivityBlocksModel>> UpdateActivitiesBlockSequenceAsync(IEnumerable<SequenceUpdateModel> sequenceUpdateModels)
        {
            var result = new List<ActivityBlocksModel>();

            foreach (var updateModel in sequenceUpdateModels)
            {
                var entity = this._activitiesRepo.Get(updateModel.Id);
                entity.SequenceNumber = updateModel.SequenceNumber;
                result.Add(this._mapper.Map<ActivityBlocksModel>(entity));
            }

            await this._activitiesRepo.SaveAsync().ConfigureAwait(false);
            return result;
        }

        public async Task<bool> DeleteActivitiesAsync(long id)
        {
            ActivityBlocks resultData = this._activitiesRepo.FindBy(x => x.Id == id).FirstOrDefault();
            if (resultData != null)
            {
                int result = await this._activitiesRepo.DeleteAsyn(resultData).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<ActivityBlocksModel> GetActivitiesByIdAsync(long id)
        {
            ActivityBlocks activities = await this._activitiesRepo.GetAsync((int)id);
            ActivityBlocksModel data = new ActivityBlocksModel();
            if (activities != null)
            {
                data = new ActivityBlocksModel
                {
                    Id = activities.Id,
                    SwimLaneId = activities.SwimLaneId,
                    ActivityTypeId = activities.ActivityTypeId,
                    Name = activities.Name,
                    SequenceNumber = activities.SequenceNumber,
                    Color = activities.Color,
                    BorderColor = activities.BorderColor,
                    BorderStyle = activities.BorderStyle,
                    BorderWidth = activities.BorderWidth,
                    Version = activities.Version,
                    EffectiveFrom = activities.EffectiveFrom,
                    EffectiveTo = activities.EffectiveTo,
                    CreatedDateTime = activities.CreatedDateTime,
                    CreatedUser = activities.CreatedUser,
                    LastUpdateDateTime = activities.LastUpdateDateTime,
                    LastUpdateUser = activities.LastUpdateUser,
                    LocationX = activities.LocationX,
                    LocationY = activities.LocationY,
                    AssetContentId = activities.AssetContentId,
                    ProtectedInd = activities.ProtectedInd,
                    ProcessMapId = activities.ProcessMapId,
                    PhaseId = activities.PhaseId,
                    ActivityConnections = this.GetActivityConnectionsByActivitiesId(activities.Id),
                    Length = activities.Length,
                    Width = activities.Width,
                };
            }

            return await Task.FromResult(data).ConfigureAwait(false);
        }

        public async Task<IEnumerable<ActivityBlocksModel>> GetActivityBlocksByProcessMapAsync(long processMapId)
        {
            List<ActivityBlocksModel> activityBlockModellist = new List<ActivityBlocksModel>();
            var activitiesList = await this._activitiesRepo.FindAllAsync(x => x.ProcessMapId == processMapId);
            if (activitiesList != null)
            {
                foreach (var activities in activitiesList)
                {
                    ActivityBlocksModel data = new ActivityBlocksModel();
                    data = new ActivityBlocksModel
                    {
                        Id = activities.Id,
                        SwimLaneId = activities.SwimLaneId,
                        ActivityTypeId = activities.ActivityTypeId,
                        Name = activities.Name,
                        SequenceNumber = activities.SequenceNumber,
                        Color = activities.Color,
                        BorderColor = activities.BorderColor,
                        BorderStyle = activities.BorderStyle,
                        BorderWidth = activities.BorderWidth,
                        Version = activities.Version,
                        EffectiveFrom = activities.EffectiveFrom,
                        EffectiveTo = activities.EffectiveTo,
                        CreatedDateTime = activities.CreatedDateTime,
                        CreatedUser = activities.CreatedUser,
                        LastUpdateDateTime = activities.LastUpdateDateTime,
                        LastUpdateUser = activities.LastUpdateUser,
                        LocationX = activities.LocationX,
                        LocationY = activities.LocationY,
                        AssetContentId = activities.AssetContentId,
                        ProtectedInd = activities.ProtectedInd,
                        ProcessMapId = activities.ProcessMapId,
                        PhaseId = activities.PhaseId,
                        ActivityConnections = this.GetActivityConnectionsByActivitiesId(activities.Id),
                        Length = activities.Length,
                        Width = activities.Width,
                    };
                    activityBlockModellist.Add(data);
                }
            }

            return await Task.FromResult(activityBlockModellist).ConfigureAwait(false);
        }

        public List<ActivityConnectionsModel> GetActivityConnectionsByActivitiesId(long id)
        {
            var objData = this._activityConnectionsRepo.FindAll(x => x.ActivityBlockId == id);
            return this._mapper.Map<List<ActivityConnectionsModel>>(objData);
        }
    }
}
