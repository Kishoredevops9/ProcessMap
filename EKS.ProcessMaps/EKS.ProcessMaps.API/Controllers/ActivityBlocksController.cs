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
    [Route("api/activityblocks")]
    [ApiController]
    public class ActivityBlocksController : ControllerBase
    {
        private readonly IActivitiesAppService _activitiesAppService;
        private readonly ILogManager _logManager;

        public ActivityBlocksController(IActivitiesAppService activitiesAppService, ILogManager logManager)
        {
            this._activitiesAppService = activitiesAppService;
            this._logManager = logManager;
        }

        /// <summary>
        ///  Create a activity blocks
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "swimLaneId": 509,
        ///             "activityTypeId": 1,
        ///             "reviewGate": "Test RG",
        ///             "name": "Test-AB",
        ///             "description": "Test purpose",
        ///             "label": "Label-AB",
        ///             "sequenceNumber": 1,
        ///             "color": "Blue",
        ///             "backgroundColor": "Yellow",
        ///             "borderColor": "Black",
        ///             "borderStyle": "Line",
        ///             "borderWidth": 5,
        ///             "location": "None",
        ///             "processMapId": 232,
        ///             "phaseId": null,
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 491,
        ///            "swimLaneId": 509,
        ///            "activityTypeId": 1,
        ///            "reviewGate": "Test RG",
        ///            "name": "Test-AB",
        ///            "description": "Test purpose",
        ///            "label": "Label-AB",
        ///            "sequenceNumber": 1,
        ///            "color": "Blue",
        ///            "backgroundColor": "Yellow",
        ///            "borderColor": "Black",
        ///            "borderStyle": "Line",
        ///            "borderWidth": 5,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-02-03T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-02-03T18:17:24.3289541",
        ///            "createdUser": "pwesw1",
        ///            "lastUpdateDateTime": "2021-02-03T18:17:24.3289541",
        ///            "lastUpdateUser": "pwesw1",
        ///            "location": "None",
        ///            "processMapId": 232,
        ///            "phaseId": 1,
        ///            "activityMeta": [],
        ///            "activityConnections": [],
        ///            "activityDocuments": []
        ///        }
        /// </remarks>
        /// <param name="activitiesModel"></param>
        /// <returns></returns>
        [HttpPost("createactivityblocks")]
        public async Task<IActionResult> CreateActivities([FromBody]ActivityBlocksModel activitiesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Blocks", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(activitiesModel.Name))
                {
                    return this.BadRequest("Please enter name.");
                }

                ActivityBlocksModel obj = await this._activitiesAppService.CreateActivitiesAsync(activitiesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates a specified activity block.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 491,
        ///             "swimLaneId": 509,
        ///             "activityTypeId": 1,
        ///             "reviewGate": "Test RG-1",
        ///             "name": "Test-AB",
        ///             "description": "Test purpose",
        ///             "label": "Label-AB",
        ///             "sequenceNumber": 1,
        ///             "color": "Blue",
        ///             "backgroundColor": "Yellow",
        ///             "borderColor": "Black",
        ///             "borderStyle": "Line",
        ///             "borderWidth": 5,
        ///             "location": "None",
        ///             "processMapId": 232,
        ///             "phaseId": null
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 491,
        ///            "swimLaneId": 509,
        ///            "activityTypeId": 1,
        ///            "reviewGate": "Test RG-1",
        ///            "name": "Test-AB",
        ///            "description": "Test purpose",
        ///            "label": "Label-AB",
        ///            "sequenceNumber": 1,
        ///            "color": "Blue",
        ///            "backgroundColor": "Yellow",
        ///            "borderColor": "Black",
        ///            "borderStyle": "Line",
        ///            "borderWidth": 5,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-02-03T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-02-03T18:17:24.3289541",
        ///            "createdUser": "pwesw1",
        ///            "lastUpdateDateTime": "2021-02-03T18:17:24.3289541",
        ///            "lastUpdateUser": "pwesw1",
        ///            "location": "None",
        ///            "processMapId": 232,
        ///            "phaseId": 1,
        ///            "activityMeta": [],
        ///            "activityConnections": [],
        ///            "activityDocuments": []
        ///        }
        /// </remarks>
        /// <param name="activitiesModel"></param>
        /// <returns></returns>
        [HttpPut("updateactivityblocks")]
        public async Task<IActionResult> UpdateActivities([FromBody]ActivityBlocksModel activitiesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Blocks", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(activitiesModel.Name))
                {
                    return this.BadRequest("Please enter name.");
                }

                ActivityBlocksModel obj = await this._activitiesAppService.UpdateActivitiesAsync(activitiesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates sequence number of activity blocks.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///     [
        ///        {
        ///             "id": 438,
        ///             "sequenceNumber": 2,
        ///        },
        ///        {
        ///             "id": 439,
        ///             "sequenceNumber": 3,
        ///        }
        ///     ]
        ///
        /// Sample Response:
        ///
        ///     [
        ///        {
        ///             "id": 438,
        ///             "sequenceNumber": 2,
        ///        },
        ///        {
        ///             "id": 439,
        ///             "sequenceNumber": 3,
        ///        }
        ///     ]
        /// </remarks>
        /// <param name="activitiesModel"></param>
        /// <returns></returns>
        [HttpPut("updateactivityblockssequence")]
        public async Task<IActionResult> UpdateActivitiesSequence([FromBody] IEnumerable<SequenceUpdateModel> sequenceUpdateModels)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Blocks", this.GetType().Name);
            try
            {
                var result = await this._activitiesAppService.UpdateActivitiesBlockSequenceAsync(sequenceUpdateModels).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Deletes a activity block.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 490
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
        [HttpDelete("deleteactivityblocks")]
        public async Task<IActionResult> DeleteActivities(int id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Blocks", this.GetType().Name);
            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter valid id.");
                }

                bool obj = await this._activitiesAppService.DeleteActivitiesAsync(id).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get all activity blocks of a specified process map.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "Id": 231
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 438,
        ///             "swimLaneId": 504,
        ///             "activityTypeId": null,
        ///             "reviewGate": null,
        ///             "name": "Test-ActivityBlocks-0",
        ///             "description": null,
        ///             "label": null,
        ///             "sequenceNumber": null,
        ///             "color": null,
        ///             "backgroundColor": null,
        ///             "borderColor": null,
        ///             "borderStyle": null,
        ///             "borderWidth": null,
        ///             "location": null,
        ///             "processMapId": 231,
        ///             "phaseId": 1,
        ///             "activityMeta": [
        ///             {
        ///                 "id": 98,
        ///                 "activityBlockId": 438,
        ///                 "key": "Test-Key-ActivityMeta-0",
        ///                 "value": "Test-Value-ActivityMeta-0"
        ///             }
        ///             ],
        ///             "activityConnections": [
        ///             {
        ///                 "id": 241,
        ///                 "activityBlockId": 438,
        ///                 "previousActivityBlockId": null,
        ///                 "label": "Test-Label-0"
        ///              }
        ///              ],
        ///              "activityDocuments": [
        ///              {
        ///                 "id": 173,
        ///                 "contentId": "Test-ActivityDocuments-0",
        ///                 "activityBlockId": 438,
        ///                 "subProcessMapId": null,
        ///                 "activityPageId": null
        ///               }
        ///               ]
        ///               },
        ///      {
        ///         "id": 439,
        ///         "swimLaneId": 504,
        ///         "activityTypeId": null,
        ///         "processMapId": 231,
        ///         "activityMeta": [
        ///         {
        ///             "id": 99,
        ///             "activityBlockId": 439
        ///         }
        ///         ],
        ///         "activityConnections": [
        ///         {
        ///             "id": 242,
        ///             "activityBlockId": 439,
        ///             "previousActivityBlockId": 440
        ///         },
        ///         {
        ///             "id": 243,
        ///             "activityBlockId": 439,
        ///             "previousActivityBlockId": 438
        ///         }
        ///         ],
        ///         "activityDocuments": [
        ///         {
        ///             "id": 174,
        ///             "contentId": "Test-ActivityDocuments-0",
        ///             "activityBlockId": 439
        ///          }
        ///          ]
        ///     }
        /// </remarks>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        [HttpGet("getactivityblocksbyprocessmap")]
        public async Task<IActionResult> GetActivitiesByProcessMapAsync(long processMapId)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Activity Blocks", this.GetType().Name);

            try
            {
                if (processMapId <= 0)
                {
                    return this.BadRequest("Please enter correct process map id.");
                }

                IEnumerable<ActivityBlocksModel> list = await this._activitiesAppService.GetActivityBlocksByProcessMapAsync(processMapId).ConfigureAwait(false);

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
