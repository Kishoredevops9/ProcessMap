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
    /// App service class for user preferences
    /// </summary>
    public class UserPreferencesAppService : IUserPreferencesAppService
    {
        private readonly IRepository<UserPreferences> _userPrefencesRepo;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor - UserPreferencesAppService
        /// </summary>
        /// <param name="userPrefencesRepo"></param>
        /// <param name="mapper"></param>
        public UserPreferencesAppService(
            IRepository<UserPreferences> userPrefencesRepo,
            IMapper mapper)
        {
            this._userPrefencesRepo = userPrefencesRepo;
            this._mapper = mapper;
        }

        /// <summary>
        /// Create the user preferences
        /// </summary>
        /// <param name="userPreferencesModel"></param>
        /// <returns></returns>
        public async Task<UserPreferencesModel> CreateUserPreferencesAsync(UserPreferencesModel userPreferencesModel)
        {
            UserPreferencesModel resultData;
            UserPreferencesModel modifiedData = new UserPreferencesModel();

            UserPreferencesModel userPreferencesResult = await this.GetUserPreferencesByIdAsync(userPreferencesModel.UserIdentifier).ConfigureAwait(false);

            if (userPreferencesResult != null)
            {
                    modifiedData.Id = userPreferencesResult.Id;
                    modifiedData.UserIdentifier = userPreferencesResult.UserIdentifier;
                    modifiedData.Tiles = userPreferencesModel.Tiles;

                    resultData = await this.UpdateUserPreferencesAsync(modifiedData).ConfigureAwait(false);

            }
            else
            {
                UserPreferences resultCreated = await this._userPrefencesRepo.AddAsyn(this._mapper.Map<UserPreferences>(userPreferencesModel)).ConfigureAwait(false);
                resultData = this._mapper.Map<UserPreferencesModel>(resultCreated);
                return resultData;
            }

            return resultData;
        }

        /// <summary>
        /// Get user preferences by emailId
        /// </summary>
        /// <param name="emailId"></param>
        /// <returns></returns>
        public async Task<UserPreferencesModel> GetUserPreferencesByIdAsync(string emailId)
        {
            UserPreferences resultData = await this._userPrefencesRepo.FindAsync(x => x.UserIdentifier == emailId).ConfigureAwait(false);
            return this._mapper.Map<UserPreferencesModel>(resultData);
        }

        /// <summary>
        /// Update the user preferences
        /// </summary>
        /// <param name="userPreferencesModel"></param>
        /// <returns></returns>
        public async Task<UserPreferencesModel> UpdateUserPreferencesAsync(UserPreferencesModel userPreferencesModel)
        {
            UserPreferences updatedResult = await this._userPrefencesRepo.UpdateExAsyn(this._mapper.Map<UserPreferences>(userPreferencesModel), userPreferencesModel.Id).ConfigureAwait(false);

           // int resultData = await this._userPrefencesRepo.UpdateAsyn(this._mapper.Map<UserPreferences>(userPreferencesModel), userPreferencesModel.Id).ConfigureAwait(false);
           // UserPreferences result = await this._userPrefencesRepo.FindAsync(x => x.Id == userPreferencesModel.Id).ConfigureAwait(false);
            return this._mapper.Map<UserPreferencesModel>(updatedResult);
        }
    }
}
