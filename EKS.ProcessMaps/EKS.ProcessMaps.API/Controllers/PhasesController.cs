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
    [Route("api/phases")]
    [ApiController]
    public class PhasesController : ControllerBase
    {
        private readonly IPhasesAppService _phasesAppService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// Constructor of phases
        /// </summary>
        /// <param name="phasesAppService"></param>
        /// <param name="logManager"></param>
        public PhasesController(IPhasesAppService phasesAppService, ILogManager logManager)
        {
            this._phasesAppService = phasesAppService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get phases by id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 1
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 1,
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "500 0",
        ///             "location": null
        ///     }
        /// </remarks>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        [HttpGet("getphasesbyid")]
        public async Task<IActionResult> GetPhasesById(long id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);

            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter valid id.");
                }

                PhasesModel list = await this._phasesAppService.GetPhasesByIdAsync(id).ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get phases by processmap id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "processMapId": 405
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 1,
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "500 0",
        ///             "location": null
        ///     }
        /// </remarks>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        [HttpGet("getphasesbymapid")]
        public async Task<IActionResult> GetPhasesByProcessMapId(int? processMapId)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);

            try
            {
                var phaseModels = await this._phasesAppService.GetPhasesByProcessMapIdAsync(processMapId).ConfigureAwait(false);

                return this.Ok(phaseModels);
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
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "500 0",
        ///             "location": null
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 1,
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "500 0",
        ///             "location": null
        ///        }
        /// </remarks>
        /// <param name="phasesModel"></param>
        /// <returns></returns>
        [HttpPost("createphases")]
        public async Task<IActionResult> CreatePhases([FromBody] PhasesModel phasesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(phasesModel.Name))
                {
                    return this.BadRequest("Please enter Name.");
                }

                PhasesModel obj = await this._phasesAppService.CreatePhasesAsync(phasesModel).ConfigureAwait(false);

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
        ///             "id": 1,
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "400 0",
        ///             "location": null
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 1,
        ///             "name": "Test Phase3",
        ///             "caption": null,
        ///             "sequenceNumber": 1,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "processMapId": 405,
        ///             "size": "400 0",
        ///             "location": null
        ///        }
        /// </remarks>
        /// <param name="phasesModel"></param>
        /// <returns></returns>
        [HttpPut("updatephases")]
        public async Task<IActionResult> Updatephases([FromBody] PhasesModel phasesModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);
            try
            {
                if (phasesModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid id");
                }

                PhasesModel obj = await this._phasesAppService.UpdatePhasesAsync(phasesModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete phases by id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 1
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
        [HttpDelete("deletephases")]
        public async Task<IActionResult> Deletephases(int id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);
            try
            {
                if (id < 0)
                {
                    return this.BadRequest("Please enter valid id");
                }

                bool obj = await this._phasesAppService.DeletePhasesAsync(id).ConfigureAwait(false);

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
