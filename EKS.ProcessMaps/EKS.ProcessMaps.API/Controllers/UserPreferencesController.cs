namespace EKS.ProcessMaps.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::EKS.Common.ExceptionHandler;
    using global::EKS.Common.Logging;
    using global::EKS.ProcessMaps.Business.Interfaces;
    using global::EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// User preferences class
    /// </summary>
    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/userpreferences")]
    [ApiController]
    public class UserPreferencesController : ControllerBase
    {
        private readonly IUserPreferencesAppService _userPreferencesAppService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// User Preferences Controller constructor.
        /// </summary>
        /// <param name="userPreferencesAppService">IUserPreferencesAppService</param>
        /// <param name="logManager">ILogManager</param>
        public UserPreferencesController(IUserPreferencesAppService userPreferencesAppService, ILogManager logManager)
        {
            this._userPreferencesAppService = userPreferencesAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get user preferences by email.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 2,
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "tiles": "[4, 2, 3, 1]"
        ///        }
        /// </remarks>
        /// <param name="emailId"></param>
        /// <returns>UserPreferencesModel</returns>
        [HttpGet("getuserpreferencesbyid")]
        public async Task<IActionResult> GetUserPreferencesById(string emailId)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "UserPreferences", this.GetType().Name);

            try
            {
                if (string.IsNullOrWhiteSpace(emailId))
                {
                    return this.BadRequest("Please enter valid email.");
                }

                UserPreferencesModel list = await this._userPreferencesAppService.GetUserPreferencesByIdAsync(emailId).ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Create user preferences.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "tiles": "[4, 2, 3, 1]"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 2,
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "tiles": "[4, 2, 3, 1]"
        ///        }
        /// </remarks>
        /// <param name="userPreferencesModel"></param>
        /// <returns>UserPreferencesModel</returns>
        [HttpPost("createuserprefences")]
        public async Task<IActionResult> CreateUserPrefences([FromBody] UserPreferencesModel userPreferencesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "UserPreferences", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(userPreferencesModel.UserIdentifier))
                {
                    return this.BadRequest("Please enter email.");
                }

                UserPreferencesModel obj = await this._userPreferencesAppService.CreateUserPreferencesAsync(userPreferencesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update user preferences.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "id": 2
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "tiles": "[4, 2, 1, 3]"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 2,
        ///            "userIdentifier": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "tiles": "[4, 2, 1, 3]"
        ///        }
        /// </remarks>
        /// <param name="userPreferencesModel"></param>
        /// <returns>UserPreferencesModel</returns>
        [HttpPut("updateuserpreferences")]
        public async Task<IActionResult> UpdateUserPreferences([FromBody] UserPreferencesModel userPreferencesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "UserPreferences", this.GetType().Name);
            try
            {
                if (userPreferencesModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid id");
                }

                UserPreferencesModel obj = await this._userPreferencesAppService.UpdateUserPreferencesAsync(userPreferencesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
    }
}
