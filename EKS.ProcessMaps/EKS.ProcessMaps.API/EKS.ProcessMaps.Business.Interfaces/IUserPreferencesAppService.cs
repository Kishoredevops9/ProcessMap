namespace EKS.ProcessMaps.Business.Interfaces
{
    using EKS.ProcessMaps.Models;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface - IUserPreferencesAppService
    /// </summary>
    public interface IUserPreferencesAppService
    {
        Task<UserPreferencesModel> CreateUserPreferencesAsync(UserPreferencesModel userPreferencesModel);

        Task<UserPreferencesModel> GetUserPreferencesByIdAsync(string emailId);

        Task<UserPreferencesModel> UpdateUserPreferencesAsync(UserPreferencesModel userPreferencesModel);
    }
}
