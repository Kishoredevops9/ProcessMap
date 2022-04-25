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

    public class ActivityGroupsAppService : IActivityGroupsAppService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<SwimLanes> _activityGroupsRepo;
        private readonly IRepository<Discipline> _disciplineRepo;

        public ActivityGroupsAppService(
            IMapper mapper,
            IRepository<SwimLanes> activityGroupsRepo,
            IRepository<Discipline> disciplineRepo)
        {
            this._mapper = mapper;
            this._activityGroupsRepo = activityGroupsRepo;
            this._disciplineRepo = disciplineRepo;
        }

        public async Task<SwimLanesModel> CreateActivityGroupsAsync(SwimLanesModel activityGroupsModel)
        {
            var largest = _activityGroupsRepo.FindBy(x => x.ProcessMapId == activityGroupsModel.ProcessMapId && x.SequenceNumber != null)
                .Select(x => x.SequenceNumber).ToList().DefaultIfEmpty(0).Max();
            activityGroupsModel.SequenceNumber = largest + 1;
            SwimLanes resultData = await this._activityGroupsRepo.AddAsyn(this._mapper.Map<SwimLanes>(activityGroupsModel)).ConfigureAwait(false);
            if (resultData.DisciplineId.HasValue)
            {
                resultData.Discipline = await this._disciplineRepo.GetAsync(resultData.DisciplineId.Value);
            }
            return this._mapper.Map<SwimLanesModel>(resultData);
        }

        public async Task<SwimLanesModel> UpdateActivityGroupsAsync(SwimLanesModel activityGroupsModel)
        {
            int resultData = await this._activityGroupsRepo.UpdateAsyn(this._mapper.Map<SwimLanes>(activityGroupsModel), activityGroupsModel.Id).ConfigureAwait(false);
            SwimLanes result = this._activityGroupsRepo.FindBy(x => x.Id == activityGroupsModel.Id).FirstOrDefault();
            return this._mapper.Map<SwimLanesModel>(result);
        }

        public async Task<bool> DeleteActivityGroupsAsync(long id)
        {
            SwimLanes resultData = this._activityGroupsRepo.FindBy(x => x.Id == id).FirstOrDefault();
            if (resultData != null)
            {
                int result = await this._activityGroupsRepo.DeleteAsyn(resultData).ConfigureAwait(false);
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<SwimLanesModel>> UpdateAllActivityGroupsAsync(List<SwimLanesModel> activityGroupsModelList)
        {
            List<SwimLanes> result = new List<SwimLanes>();

            foreach (var activityGroupsModel in activityGroupsModelList)
            {
                int resultData = await this._activityGroupsRepo.UpdateAsyn(this._mapper.Map<SwimLanes>(activityGroupsModel), activityGroupsModel.Id).ConfigureAwait(false);

                if (resultData > 0)
                {
                    SwimLanes swimLanesData = this._activityGroupsRepo.FindBy(x => x.Id == activityGroupsModel.Id).FirstOrDefault();
                    result.Add(swimLanesData);
                }
            }

            return this._mapper.Map<List<SwimLanesModel>>(result);
        }

        public async Task<IEnumerable<SwimLanesModel>> UpdateActivityGroupsSequenceAsync(IEnumerable<SequenceUpdateModel> sequenceUpdateModels)
        {
            var result = new List<SwimLanesModel>();

            foreach (var updateModel in sequenceUpdateModels)
            {
                var entity = this._activityGroupsRepo.Get(updateModel.Id);
                entity.SequenceNumber = updateModel.SequenceNumber;

                result.Add(this._mapper.Map<SwimLanesModel>(entity));
            }

            await this._activityGroupsRepo.SaveAsync().ConfigureAwait(false);
            return result;
        }
    }
}
