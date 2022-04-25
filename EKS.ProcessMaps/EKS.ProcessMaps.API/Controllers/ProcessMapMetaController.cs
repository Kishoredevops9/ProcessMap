using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EKS.Common.ExceptionHandler;
using EKS.Common.Logging;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EKS.ProcessMaps.API.Controllers
{
    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/processmapmeta")]
    [ApiController]
    public class ProcessMapMetaController : ControllerBase
    {
        private readonly IProcessMapMetaAppService _processMapMetaAppService;
        private readonly ILogManager _logManager;
        
        public ProcessMapMetaController(IProcessMapMetaAppService processMapMetaAppService, ILogManager logManager)
        {
            _processMapMetaAppService = processMapMetaAppService;
            _logManager = logManager;
        }


        /// <summary>
        /// Create process map metadata 
        /// </summary>
        /// <remarks>
        ///           Sample Request:       
        ///                
        ///                 {
        ///                 "key": "Edited",
        ///                 "value":"true", 
        ///                 "processMapId": 545, 
        ///                 "createdbyUserid":575
        ///                 }
        ///
        ///            Sample Response:
        ///            
        ///                 {	
        ///                     "id": 52,
        ///                     "processMapId": 545,
        ///                     "key": "Edited",
        ///                     "value": "true",
        ///                     "version": null,
        ///                     "createdon": null,
        ///                     "createdbyUserid": "575",
        ///                     "modifiedon": null,
        ///                     "modifiedbyUserid": null,
        ///                     "processMap": null
        ///                  }
        /// </remarks>
        /// <param name="processMapMetaModel"></param>
        /// <returns></returns>
        // POST: api/ProcessMapMeta
        [HttpPost("createprocessmapmeta")]
        public async Task<IActionResult> CreateProcessMapMeta([FromBody]ProcessMapMetaModel processMapMetaModel)
        {

            Dictionary<string, string> errorProperties =
                _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(processMapMetaModel.Key))
                {
                    return BadRequest("Please enter key.");
                }

                ProcessMapMetaModel obj = await _processMapMetaAppService.CreateProcessMapMetaAsync(processMapMetaModel);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
        /// <summary>
        /// Update process map metadata
        /// </summary>
        /// <remarks>
        /// 
        ///  Sample Request:
        ///  
        ///     {
        ///      "id" : 52,
        ///      "processMapId": 545,
        ///      "key": "TestMeta",
        ///      "value":"true",
        ///      "createdbyUserid":600
        ///     }
        ///   
        /// Sample Response:
        /// 
        ///        {
        ///         "processMapId": 545,
        ///         "key": "TestMeta",
        ///         "value": "true",
        ///         "version": null,
        ///         "createdon": null,
        ///         "createdbyUserid": "600",
        ///         "modifiedon": null,
        ///         "modifiedbyUserid": null,
        ///         "processMap": null
        ///        }
        /// </remarks>
        /// <param name="processMapsMeta"></param>
        /// <returns></returns>
        // POST: api/ProcessMapMeta
        [HttpPut("updateprocessmapmeta")]
        public async Task<IActionResult> UpdateProcessMapMeta([FromBody]ProcessMapMetaModel processMapMetaModel)
        {

            Dictionary<string, string> errorProperties =
                _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(processMapMetaModel.Key))
                {
                    return BadRequest("Please enter key.");
                }

                ProcessMapMetaModel obj = await _processMapMetaAppService.UpdateProcessMapMetaAsync(processMapMetaModel);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete process map metadata
        /// </summary>
        /// <remarks>
        /// 
        /// Sample Response:
        /// 
        ///        20
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE: api/ProcessMapMeta/{id}
        [HttpDelete("deleteprocessmapmeta")]
        public async Task<IActionResult> DeleteProcessMapMeta(int id)
        {

            Dictionary<string, string> errorProperties =
                _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (id <= 0)
                {
                    return BadRequest("id should be greater than zero");
                }

                bool obj = await _processMapMetaAppService.DeleteProcessMapMetaAsync(id);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

    }


}
