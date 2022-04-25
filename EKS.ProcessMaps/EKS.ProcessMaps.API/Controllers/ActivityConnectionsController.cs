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
    [Route("api/activityconnections")]
    [ApiController]
    public class ActivityConnectionsController : ControllerBase
    {
        private readonly IActivityConnectionsAppService _activityConnectionsAppService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Constructor of activity connections.
        /// </summary>
        /// <param name="activityConnectionsAppService"></param>
        /// <param name="logManager"></param>
        public ActivityConnectionsController(IActivityConnectionsAppService activityConnectionsAppService, ILogManager logManager)
        {
            this._activityConnectionsAppService = activityConnectionsAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Create a new activity connections on a specified activity.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "activityBlockId": 171,
        ///             "previousActivityBlockId": 170,
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 519,
        ///            "activityBlockId": 171,
        ///            "previousActivityBlockId": 170,
        ///        }.
        /// </remarks>
        /// <param name="activityConnectionsModel"></param>
        /// <returns>ActivityConnectionsModel</returns>
        [HttpPost("createactivityconnections")]
        public async Task<IActionResult> CreateActivityConnections([FromBody] ActivityConnectionsModel activityConnectionsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Connections", this.GetType().Name);
            try
            {
                ActivityConnectionsModel obj = await this._activityConnectionsAppService.CreateActivityConnectionsAsync(activityConnectionsModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update a specified activity connections
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 519,
        ///             "activityBlockId": 171,
        ///             "previousActivityBlockId": 170,
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 519,
        ///            "activityBlockId": 171,
        ///            "previousActivityBlockId": 170,
        ///        }.
        /// </remarks>
        /// <param name="activityConnectionsModel"></param>
        /// <returns>ActivityConnectionsModel</returns>
        [HttpPut("updateactivityconnections")]
        public async Task<IActionResult> UpdateActivityConnections([FromBody] ActivityConnectionsModel activityConnectionsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Connections", this.GetType().Name);
            try
            {
                if (activityConnectionsModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid id.");
                }

                ActivityConnectionsModel obj = await this._activityConnectionsAppService.UpdateActivityConnectionsAsync(activityConnectionsModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Deletes a specified activity connections.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 519
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             true
        ///        }.
        /// </remarks>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        [HttpDelete("deleteactivityconnections")]
        public async Task<IActionResult> DeleteActivityConnections(int id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Connections", this.GetType().Name);
            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter valid id.");
                }

                bool result = await this._activityConnectionsAppService.DeleteActivityConnectionsAsync(id).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
    }
}
