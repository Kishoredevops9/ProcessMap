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
    [Route("api/ActivityPages")]
    [ApiController]
    public class ActivityPagesController : ControllerBase
    {
        private readonly IActivityPagesAppService _activityPagesAppService;
        private readonly ILogManager _logManager;

        public ActivityPagesController(IActivityPagesAppService activityPagesAppService, ILogManager logManager)
        {
            this._activityPagesAppService = activityPagesAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get all the activity pages
        /// </summary>
        /// <remarks>
        /// Sample Response:
        ///
        ///        [
        ///            {
        ///               "id": 1,
        ///               contentId": "",
        ///               "title": "TestAP010321_1",
        ///               "disciplineId": 2,
        ///               "subDisciplineId": 13,
        ///               "subSubDisciplineId": 30,
        ///               "subSubSubDisciplineId": null,
        ///               "disciplineCodeId": 3,
        ///               "assetTypeId": 6,
        ///               "assetStatusId": 1,
        ///               "contentOwnerId": "2",
        ///               "classifierId": 4,
        ///               "confidentialityId": 4,
        ///               "revisionTypeId": null,
        ///               "programControlled": true,
        ///               "outsourceable": false,
        ///               "version": 1,
        ///               "effectiveFrom": "2021-03-01T00:00:00",
        ///               "effectiveTo": null,
        ///               "createdDateTime": "2021-03-01T18:59:54.6979563",
        ///               "createdUser": "sm@pwesw2.onmicrosoft.com",
        ///               "lastUpdateDateTime": "2021-03-01T18:59:55.7833333",
        ///               "lastUpdateUser": "pwesw1",
        ///               "usjurisdictionId": null,
        ///               "usclassificationId": null,
        ///               "classificationRequestNumber": null,
        ///               "classificationDate": null,
        ///               "tpmDate": null,
        ///               "keywords": "TestKey",
        ///               "author": null,
        ///               "confidentiality": true,
        ///               "purpose": "tset",
        ///               "processInstId": null,
        ///               "customId": null
        ///            }
        ///        ]
        /// </remarks>
        /// <returns></returns>
        [HttpGet("getallactivitypages")]
        public async Task<IActionResult> GetAllActivityPages()
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ActivityBlockTypes", this.GetType().Name);

            try
            {
                IEnumerable<ActivityPageModel> list = await this._activityPagesAppService.GetAllActivityPagesAsync().ConfigureAwait(false);

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
