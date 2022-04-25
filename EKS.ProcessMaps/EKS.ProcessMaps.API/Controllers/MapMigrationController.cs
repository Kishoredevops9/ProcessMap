using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKS.Common.ExceptionHandler;
using EKS.Common.Logging;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EKS.ProcessMaps.API.Controllers
{

    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/MapMigration")]
    [ApiController]
    public class MapMigrationController : ControllerBase
    {
        private readonly ILogManager _logManager;
        private readonly IMigrateMapsAppService _migrateMapsAppService;

        /// <summary>
        /// MigrateMapsController.
        /// </summary>
        /// <param name="logManager"></param>
        /// <param name="migrateMapsAppService"></param>
        public MapMigrationController(
            ILogManager logManager,
            IMigrateMapsAppService migrateMapsAppService)
        {
            this._logManager = logManager;
            this._migrateMapsAppService = migrateMapsAppService;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="contentAssetTypeModel"></param>
        /// <returns></returns>
        [HttpPost("MigrateMaps")]
        public async Task<IActionResult> MigrateMaps([FromBody]ContentAssetTypeModel contentAssetTypeModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Map Migration", this.GetType().Name);
            try
            {
                var obj = await this._migrateMapsAppService.MigrateMapsAsync(contentAssetTypeModel).ConfigureAwait(false);

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
