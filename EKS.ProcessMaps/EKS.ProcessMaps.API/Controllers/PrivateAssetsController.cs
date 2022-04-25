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
    [Route("api/privateassets")]
    [ApiController]
    public class PrivateAssetsController : ControllerBase
    {
        private readonly IPrivateAssetsAppService _privateAssetsAppService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Constructor of private assets.
        /// </summary>
        /// <param name="privateAssetsAppService"></param>
        /// <param name="logManager"></param>
        public PrivateAssetsController(IPrivateAssetsAppService privateAssetsAppService, ILogManager logManager)
        {
            this._privateAssetsAppService = privateAssetsAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get all private assets
        /// </summary>
        /// <remarks>
        /// Sample Response:
        ///
        ///          [
        ///             {
        ///                 "contentAssetId": 3,
        ///                 "parentContentAssetId": 12,
        ///                 "parentTaskId": null,
        ///                 "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///             },
        ///             {
        ///                 "contentAssetId": 10,
        ///                 "parentContentAssetId": 12,
        ///                 "parentTaskId": null,
        ///                 "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///                 "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///             }
        ///          ]
        /// </remarks>
        /// <returns></returns>
        [HttpGet("getallprivateassets")]
        public async Task<IActionResult> GetAllPrivateAssets()
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "PrivateAssets", this.GetType().Name);

            try
            {
                IEnumerable<PrivateAssetsModel> list = await this._privateAssetsAppService.GetAllPrivateAssetsAsync().ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get private asset by parent content asset id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 12
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///              "contentAssetId": 3,
        ///              "parentContentAssetId": 12,
        ///              "parentTaskId": null,
        ///              "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///              "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        /// </remarks>
        /// <param name="parentContentAssetId"></param>
        /// <returns></returns>
        [HttpGet("getprivateassetsbyparentcontentassetid")]
        public async Task<IActionResult> GetPrivateAssetsByParentContentAssetId(int parentContentAssetId)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "PrivateAssets", this.GetType().Name);

            try
            {
                var models = await this._privateAssetsAppService
                    .GetPrivateAssetsByParentContentAssetIdAsync(parentContentAssetId)
                    .ConfigureAwait(false);

                return this.Ok(models);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        ///  Create phases.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///              "contentAssetId": 3,
        ///              "parentContentAssetId": 12,
        ///              "parentTaskId": null,
        ///              "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///              "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///              "contentAssetId": 3,
        ///              "parentContentAssetId": 12,
        ///              "parentTaskId": null,
        ///              "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///              "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        /// </remarks>
        /// <param name="privateAssetsModel"></param>
        /// <returns></returns>
        [HttpPost("createprivateassets")]
        public async Task<IActionResult> CreatePrivateAssets([FromBody] PrivateAssetsModel privateAssetsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "PrivateAssets", this.GetType().Name);
            try
            {
                var errorMessage = string.Empty;

                if (!this.IsModelValid(privateAssetsModel, ref errorMessage))
                {
                    return this.BadRequest(errorMessage);
                }

                PrivateAssetsModel obj = await this._privateAssetsAppService.CreatePrivateAssetsAsync(privateAssetsModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates a specified phases.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///              "contentAssetId": 3,
        ///              "parentContentAssetId": 12,
        ///              "parentTaskId": null,
        ///              "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///              "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///              "contentAssetId": 3,
        ///              "parentContentAssetId": 12,
        ///              "parentTaskId": null,
        ///              "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///              "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        /// </remarks>
        /// <param name="privateAssetsModel"></param>
        /// <returns></returns>
        [HttpPut("updateprivateassets")]
        public async Task<IActionResult> UpdatePrivateAssets([FromBody] PrivateAssetsModel privateAssetsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "PrivateAssets", this.GetType().Name);
            try
            {
                var errorMessage = string.Empty;

                if (!this.IsModelValid(privateAssetsModel, ref errorMessage))
                {
                    return this.BadRequest(errorMessage);
                }

                PrivateAssetsModel obj = await this._privateAssetsAppService.UpdatePrivateAssetsAsync(privateAssetsModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        private bool IsModelValid(PrivateAssetsModel privateAssetsModel, ref string errorMessage)
        {
            if (privateAssetsModel.ContentAssetId <= 0)
            {
                errorMessage = "Please enter valid ContentAssetId";
                return false;
            }

            if (privateAssetsModel.ParentContentAssetId.HasValue && privateAssetsModel.ParentTaskId.HasValue)
            {
                errorMessage = "One of two fields ParentContentAssetId or ParentTaskId must be null.";
                return false;
            }

            if (!privateAssetsModel.ParentContentAssetId.HasValue && !privateAssetsModel.ParentTaskId.HasValue)
            {
                errorMessage = "One of two fields ParentContentAssetId or ParentTaskId must has value.";
                return false;
            }

            return true;
        }
    }
}
