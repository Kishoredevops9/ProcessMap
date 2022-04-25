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
    //[EnableCors(PolicyName = "FrontEndWebApp")]
    //[Route("api/activitymeta")]
    //[ApiController]
    //public class ActivityMetaController : ControllerBase
    //{
    //    private readonly IActivityMetaAppService _activityMetaAppService;
    //    private readonly ILogManager _logManager;

        
    //    public ActivityMetaController(IActivityMetaAppService activityMetaAppService, ILogManager logManager)
    //    {
    //        _activityMetaAppService = activityMetaAppService;
    //        _logManager = logManager;
    //    }


    //    /// <summary>
    //    /// Create activity meta 
    //    /// </summary>
    //    /// <remarks>
    //    /// Sample Request:
    //    /// 
    //    ///            {
    //    ///                 "ActivityId":2167,
    //    ///                 "key":"difficulty",
    //    ///                 "value":"easy"
    //    ///             }
    //    ///             
    //    /// Sample Response:
    //    /// 
    //    ///     {
    //    ///      "id": 15,
    //    ///     "activityId": 2167,
    //    ///     "key": "difficulty",
    //    ///     "value": "easy",
    //    ///     "version": null,
    //    ///     "createdon": null,
    //    ///     "createdbyUserid": null,
    //    ///     "modifiedon": null,
    //    ///     "modifiedbyUserid": null,
    //    ///     "activity": null
    //    ///     }
    //    /// </remarks>
    //    /// <param name="activityMetaModel"></param>
    //    /// <returns></returns>
    //    // POST: api/ActivityMeta
    //    [HttpPost("createactivitymeta")]
    //    public async Task<IActionResult> CreateActivityMeta([FromBody]ActivityMetaModel activityMetaModel)
    //    {

    //        Dictionary<string, string> errorProperties =
    //            _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
    //        try
    //        {
    //            if (string.IsNullOrWhiteSpace(activityMetaModel.Key))
    //            {
    //                return BadRequest("Please enter key.");
    //            }

    //            ActivityMetaModel obj = await _activityMetaAppService.CreateActivityMetaAsync(activityMetaModel);

    //            return Ok(obj);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
    //            throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
    //        }
    //    }


    //    /// <summary>
    //    /// Deletes activity meta
    //    /// </summary>
    //    /// <remarks>
    //    /// Sample Response:
    //    /// 
    //    ///     {
    //    ///      "id": 19   
    //    ///    }
    //    /// </remarks>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    // DELETE: api/ActivityMeta/{id}
    //    [HttpDelete("deleteactivitymeta")]
    //    public async Task<IActionResult> DeleteActivityMeta(int id)
    //    {

    //        Dictionary<string, string> errorProperties =
    //            _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
    //        try
    //        {
    //            if (id <= 0)
    //            {
    //                return BadRequest("id should be greater than zero");
    //            }

    //            bool obj = await _activityMetaAppService.DeleteActivityMetaAsync(id);

    //            return Ok(obj);
    //        }
    //        catch (Exception ex)
    //        {
    //            _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
    //            throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
    //        }
    //    }
        

    //}
}
