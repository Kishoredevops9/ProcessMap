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

    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/activityblocktypes")]
    [ApiController]
    public class ActivityBlockTypesController : ControllerBase
    {
        private readonly IActivityBlockTypesAppService _activityBlockTypesAppService;
        private readonly ILogManager _logManager;

        public ActivityBlockTypesController(IActivityBlockTypesAppService activityBlockTypesAppService, ILogManager logManager)
        {
            this._activityBlockTypesAppService = activityBlockTypesAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get all the activity block types
        /// </summary>
        /// <remarks>
        /// Sample Response:
        ///
        ///
        ///           [
        ///             {
        ///                 "id": 1,
        ///                 "name": "Square"
        ///             },
        ///             {
        ///                 "id": 2,
        ///                 "name": "Diamond"
        ///             }
        ///           ]
        /// </remarks>
        /// <returns></returns>
        [HttpGet("getallactivityblocktypes")]
        public async Task<IActionResult> GetAllActivityBlockTypes()
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ActivityBlockTypes", this.GetType().Name);

            try
            {
                IEnumerable<ActivityBlockTypesModel> list = await this._activityBlockTypesAppService.GetAllActivityBlockTypesAsync().ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
    }
}
