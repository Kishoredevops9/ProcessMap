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
    [Route("api/swimlanes")]
    [ApiController]
    public class SwimLanesController : ControllerBase
    {
        private readonly IActivityGroupsAppService _activityGroupsAppService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="SwimLanesController"/> class.
        /// </summary>
        /// <param name="activityGroupsAppService"></param>
        /// <param name="logManager"></param>
        public SwimLanesController(IActivityGroupsAppService activityGroupsAppService, ILogManager logManager)
        {
            this._activityGroupsAppService = activityGroupsAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Create swimlanes.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "processMapId": 232,
        ///             "sequenceNumber": 1,
        ///             "color": "White",
        ///                 "borderColor": "string",
        ///                 "borderStyle": "string",
        ///                 "borderWidth": 0,
        ///                 "version": 0,
        ///                 "effectiveFrom": "2021-06-08T19:48:58.829Z",
        ///                 "effectiveTo": "2021-06-08T19:48:58.829Z",
        ///                 "createdDateTime": "2021-06-08T19:48:58.829Z",
        ///                 "createdUser": "string",
        ///                 "lastUpdateDateTime": "2021-06-08T19:48:58.829Z",
        ///                 "lastUpdateUser": "string",
        ///                 "disciplineId": 5,
        ///                 "protectedInd": true,
        ///                 "requiredInd": true,
        ///                 "size": "string"
        ///          }
        /// Sample Response:
        ///
        ///        {
        ///            "id": 509,
        ///            "processMapId": 232,
        ///            "sequenceNumber": 1,
        ///            "color": "White",
        ///            "backgroundColor": "Blue",
        ///            "borderColor": "Yellow",
        ///            "borderStyle": "Line",
        ///            "borderWidth": 3,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-02-03T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-02-03T17:51:07.250676",
        ///            "createdUser": "pwesw1",
        ///            "lastUpdateDateTime": "2021-02-03T17:51:07.250676",
        ///            "lastUpdateUser": "pwesw1",
        ///            "disciplineId": 5,
        ///            "protectedInd": true,
        ///            "requiredInd": true,
        ///            "size": "string"
        ///        }
        /// </remarks>
        /// <param name="swimLanesModel"></param>
        /// <returns></returns>
        [HttpPost("createswimlanes")]
        public async Task<IActionResult> CreateActivityGroups([FromBody] SwimLanesModel swimLanesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "SwimLanes", this.GetType().Name);
            try
            {
                SwimLanesModel obj = await this._activityGroupsAppService.CreateActivityGroupsAsync(swimLanesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates a specified swimlanes.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 509,
        ///             "name": "Test-Swimlane-1",
        ///             "description": "Test purpose",
        ///             "label": "SM",
        ///             "processMapId": 232,
        ///             "sequenceNumber": 1,
        ///             "color": "White",
        ///             "backgroundColor": "Blue",
        ///             "borderColor": "Yellow",
        ///             "borderStyle": "Line",
        ///             "borderWidth": 3,
        ///             "location": "None"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 509,
        ///            "name": "Test-Swimlane-1",
        ///            "description": "Test purpose",
        ///            "label": "SM",
        ///            "processMapId": 232,
        ///            "sequenceNumber": 1,
        ///            "color": "White",
        ///            "backgroundColor": "Blue",
        ///            "borderColor": "Yellow",
        ///            "borderStyle": "Line",
        ///            "borderWidth": 3,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-02-03T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-02-03T17:51:07.250676",
        ///            "createdUser": "pwesw1",
        ///            "lastUpdateDateTime": "2021-02-03T17:51:07.250676",
        ///            "lastUpdateUser": "pwesw1",
        ///            "location": "None"
        ///        }
        /// </remarks>
        /// <param name="swimLanesModel"></param>
        /// <returns></returns>
        [HttpPut("updateswimlanes")]
        public async Task<IActionResult> UpdateActivityGroups([FromBody] SwimLanesModel swimLanesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "SwimLanes", this.GetType().Name);
            try
            {
                SwimLanesModel obj = await this._activityGroupsAppService.UpdateActivityGroupsAsync(swimLanesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates all given swimlanes.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        [
        ///          {
        ///             "id": 26,
        ///             "name": "Test Name",
        ///             "description": "Test Desc",
        ///             "label": "Test Label",
        ///             "processMapId": 4,
        ///             "sequenceNumber": 1,
        ///             "color": "Test",
        ///             "backgroundColor": "Test",
        ///             "borderColor": "Test",
        ///             "borderStyle": "Test",
        ///             "borderWidth": 0,
        ///             "version": 2,
        ///             "effectiveFrom": "2021-01-08T06:30:12.308Z",
        ///             "effectiveTo": "2021-01-08T06:30:12.308Z",
        ///             "createdDateTime": "2021-01-08T06:30:12.308Z",
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateDateTime": "2021-01-08T06:30:12.308Z",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "location": "Test"
        ///             },
        ///             {
        ///                 "id": 28,
        ///                 "name": "Test",
        ///                 "description": "Test",
        ///                 "label": "Test",
        ///                 "processMapId": 7,
        ///                 "sequenceNumber": 0,
        ///                 "color": "Test",
        ///                 "backgroundColor": "Test",
        ///                 "borderColor": "Test",
        ///                 "borderStyle": "Test",
        ///                 "borderWidth": 0,
        ///                 "version": 1,
        ///                 "effectiveFrom": "2021-01-08T06:30:12.308Z",
        ///                 "effectiveTo": "2021-01-08T06:30:12.308Z",
        ///                 "createdDateTime": "2021-01-08T06:30:12.308Z",
        ///                 "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "lastUpdateDateTime": "2021-01-08T06:30:12.308Z",
        ///                 "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "location": "Test"
        ///               }
        ///        ]
        ///
        /// Sample Response:
        ///
        ///        [
        ///          {
        ///             "id": 26,
        ///             "name": "Test Name1",
        ///             "description": "Test Desc1",
        ///             "label": "Test Label",
        ///             "processMapId": 4,
        ///             "sequenceNumber": 1,
        ///             "color": "Test",
        ///             "backgroundColor": "Test",
        ///             "borderColor": "Test",
        ///             "borderStyle": "Test",
        ///             "borderWidth": 0,
        ///             "version": 2,
        ///             "effectiveFrom": "2021-01-08T06:30:12.308Z",
        ///             "effectiveTo": "2021-01-08T06:30:12.308Z",
        ///             "createdDateTime": "2021-01-08T06:30:12.308Z",
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateDateTime": "2021-01-08T06:30:12.308Z",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "location": "Test"
        ///             },
        ///             {
        ///                 "id": 28,
        ///                 "name": "Test",
        ///                 "description": "Test",
        ///                 "label": "Test",
        ///                 "processMapId": 7,
        ///                 "sequenceNumber": 0,
        ///                 "color": "Test",
        ///                 "backgroundColor": "Test",
        ///                 "borderColor": "Test",
        ///                 "borderStyle": "Test",
        ///                 "borderWidth": 0,
        ///                 "version": 1,
        ///                 "effectiveFrom": "2021-01-08T06:30:12.308Z",
        ///                 "effectiveTo": "2021-01-08T06:30:12.308Z",
        ///                 "createdDateTime": "2021-01-08T06:30:12.308Z",
        ///                 "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "lastUpdateDateTime": "2021-01-08T06:30:12.308Z",
        ///                 "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "location": "Test"
        ///             }
        ///        ]
        /// </remarks>
        /// <param name="swimLanesModel"></param>
        /// <returns></returns>
        [HttpPut("updateallswimlanes")]
        public async Task<IActionResult> UpdateAllActivityGroups([FromBody] List<SwimLanesModel> swimLanesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "SwimLanes", this.GetType().Name);
            try
            {
                List<SwimLanesModel> obj = await this._activityGroupsAppService.UpdateAllActivityGroupsAsync(swimLanesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates sequence number of swimlanes.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        [
        ///            {
        ///                 "id": 1108,
        ///                 "sequenceNumber": 1,
        ///             },
        ///             {
        ///                 "id": 1109,
        ///                 "sequenceNumber": 2,
        ///             }
        ///        ]
        ///
        /// Sample Response:
        ///
        ///        [
        ///             {
        ///                 "id": 1108,
        ///                 "sequenceNumber": 1,
        ///             },
        ///             {
        ///                 "id": 1109,
        ///                 "sequenceNumber": 2,
        ///             }
        ///        ]
        /// </remarks>
        /// <param name="swimLanesModel"></param>
        /// <returns></returns>
        [HttpPut("updateswimlanessequence")]
        public async Task<IActionResult> UpdateActivityGroupsSequence([FromBody] IEnumerable<SequenceUpdateModel> sequenceModels)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "SwimLanes", this.GetType().Name);
            try
            {
                var result = await this._activityGroupsAppService.UpdateActivityGroupsSequenceAsync(sequenceModels).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete swimlanes
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 503
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            true
        ///        }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("deleteswimlanes")]
        public async Task<IActionResult> DeleteActivityGroups(int id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Swimlanes", this.GetType().Name);
            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please provide valid id.");
                }

                bool obj = await this._activityGroupsAppService.DeleteActivityGroupsAsync(id).ConfigureAwait(false);

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
