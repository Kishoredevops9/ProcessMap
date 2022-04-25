namespace EKS.ProcessMaps.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::EKS.Common.ExceptionHandler;
    using global::EKS.Common.Logging;
    using global::EKS.ProcessMaps.Business.Interfaces;
    using global::EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/publicsteps")]
    [ApiController]
    public class PublicStepsController : ControllerBase
    {
        private readonly ILogManager _logManager;
        private readonly IPublicStepsAppService _publicStepsAppService;

        public PublicStepsController(
            ILogManager logManager,
            IPublicStepsAppService publicStepsAppService)
        {
            this._logManager = logManager;
            this._publicStepsAppService = publicStepsAppService;
        }

        /// <summary>
        /// Create public steps.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///         {
        ///             "title": "steps title",
        ///             "assetTypeId": 14 // It is 14 for public steps
        ///         }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 1179,
        ///             "contentId": "AA-F-012746",
        ///             "title": "valid title 2",
        ///             "disciplineId": null,
        ///             "disciplineCodeId": null,
        ///             "assetTypeId": 4,
        ///             "assetStatusId": 1,
        ///             "approvalRequirementId": null,
        ///             "classifierId": null,
        ///             "confidentialityId": null,
        ///             "revisionTypeId": null,
        ///             "programControlled": null,
        ///             "outsourceable": null,
        ///             "version": 1,
        ///             "effectiveFrom": "2021-06-08T00:00:00",
        ///             "effectiveTo": null,
        ///             "createdDateTime": "2021-06-08T20:51:10.1347306+00:00",
        ///             "createdUser": "pwesw1",
        ///             "lastUpdateDateTime": "2021-06-08T20:51:10.4304123+00:00",
        ///             "lastUpdateUser": "pwesw1",
        ///             "usjurisdictionId": null,
        ///             "usclassificationId": null,
        ///             "classificationRequestNumber": null,
        ///             "classificationDate": null,
        ///             "tpmdate": null,
        ///             "exportAuthorityId": null,
        ///             "controllingProgramId": null,
        ///             "contentOwnerId": null,
        ///             "keywords": null,
        ///             "author": null,
        ///             "confidentiality": null,
        ///             "purpose": null,
        ///             "processInstId": null,
        ///             "customId": null,
        ///             "disciplineCode": null,
        ///             "privateInd": false,
        ///             "sourceFileUrl": null,
        ///             "exportPdfurl": null,
        ///             "swimLanes": [],
        ///             "contentPhase": null,
        ///             "contentExportCompliances": null,
        ///             "contentTag": null
        ///        }
        /// </remarks>
        /// <param name="processMapModel"></param>
        /// <returns>ProcessMapModel</returns>
        [HttpPost("createpublicsteps")]
        public async Task<IActionResult> CreatePublicSteps([FromBody] ProcessMapModel processMapModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "PublicSteps", this.GetType().Name);
            try
            {
                if (processMapModel.AssetTypeId <= 0)
                {
                    return this.BadRequest("Please enter asset type id.");
                }

                PublicStepsInputOutputModel obj = await this._publicStepsAppService.CreatePublicStepsAsync(processMapModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update purpose in public steps.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///         {
        ///             "id": 3744,
        ///             "contentId": "P-1006414",
        ///             "purpose": "Steps purpose"
        ///         }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 3744,
        ///             "contentId": "P-1006414",
        ///             "purpose": "Steps purpose"
        ///        }
        /// </remarks>
        /// <param name="PublicStepsPurposeModel"></param>
        /// <returns>PublicStepsPurposeModel</returns>
        [HttpPut("updatepublicstepspurpose")]
        public async Task<IActionResult> UpdatePublicStepsPurpose([FromBody] PublicStepsPurposeModel model)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Public steps", this.GetType().Name);
            try
            {
                if (model.Id <= 0)
                {
                    return this.BadRequest("Please enter Id.");
                }

                if (string.IsNullOrWhiteSpace(model.ContentId))
                {
                    return this.BadRequest("Please enter ContentId.");
                }

                PublicStepsPurposeModel obj = await this._publicStepsAppService.UpdatePublicStepsPurposeAsync(model).ConfigureAwait(false);

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
