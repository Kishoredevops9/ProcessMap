namespace EKS.ProcessMaps.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using global::EKS.Common.ExceptionHandler;
    using global::EKS.Common.Logging;
    using global::EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
    using global::EKS.ProcessMaps.API.Helper;
    using global::EKS.ProcessMaps.Business.Interfaces;
    using global::EKS.ProcessMaps.Filters;
    using global::EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Cors;
    using Microsoft.AspNetCore.Mvc;

    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/processmaps")]
    [ApiController]
    public class ProcessMapsController : ControllerBase
    {
        private readonly IProcessMapsAppService _processMapsAppService;
        private readonly IKnowledgeAssetAppService _kaService;
        private readonly IKnowledgeAssetExportAppService _kaExportService;
        private readonly IKnowledgeAssetCloneAppService _cloneService;
        private readonly ILogManager _logManager;

        /// <summary>
        /// ProcessMapsController
        /// </summary>
        /// <param name="processMapsAppService"></param>
        /// <param name="logManager"></param>
        public ProcessMapsController(IProcessMapsAppService processMapsAppService, 
            IKnowledgeAssetAppService kaService,
            IKnowledgeAssetExportAppService kaExportService,
            IKnowledgeAssetCloneAppService  cloneService,
            ILogManager logManager)
        {
            this._processMapsAppService = processMapsAppService;
            this._kaService = kaService;
            this._kaExportService = kaExportService;
            this._cloneService = cloneService;
            this._logManager = logManager;
        }

        /// <summary>
        /// Get all process maps
        /// </summary>
        /// <remarks>
        /// Sample Response:
        ///
        ///          {
        ///            "id": 89,
        ///            "contentId": "M-0009",
        ///            "title": "GSE Expanded Services",
        ///            "disciplineId": null,
        ///            "subDisciplineId": null,
        ///            "subSubDisciplineId": null,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "phaseId": 1,
        ///            "swimLanes":[
        ///                                     {"id": 245, "name": "Design RDE", "processMapId": 89, "version": null,…},
        ///                                     {"id": 241, "name": "Project", "processMapId": 89, "version": 1,…},
        ///                                     {"id": 242, "name": "Milestone", "processMapId": 89, "version": 1,…}
        ///                         ],
        ///             "processMapMetaData":[
        ///                                 {"id": 90, "processMapId": 89, "key": "Test-Key", "value": "Test-Value",…}
        ///           },
        ///           {
        ///             "id": 232,
        ///             "contentId": "M-0001",
        ///             "title": "Test-01",
        ///             "disciplineId": null,
        ///             "subDisciplineId": null,
        ///             "subSubDisciplineId": null,
        ///             "subSubSubDisciplineId": null,
        ///             "disciplineCodeId": null,
        ///             "swimLanes":[
        ///                                     {"id": 506, "name": "Test-Swimlane-1", "processMapId": 232, "version": 1,…},
        ///                                     {"id": 507, "name": "Test-Swimlane-2", "processMapId": 232, "version": 1,…}
        ///                         ],
        ///             "processMapMetaData":[
        ///                         {"id": 565, "processMapId": 232, "key": "Test-PMM", "value": "Test-PMM-Value",…}
        ///                 ],
        ///                 "phases": []
        ///                 "contentPhases": [
        ///             {
        ///                 "id": 217,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "phaseId": 3
        ///             },
        ///             {
        ///                 "id": 218,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "phaseId": 5
        ///             }
        ///             ],
        ///             "contentExportCompliances": [
        ///             {
        ///                 "id": 213,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "exportComplianceId": 1
        ///             },
        ///             {
        ///                 "id": 214,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "exportComplianceId": 2
        ///             }
        ///             ],
        ///             "contentTags": [
        ///             {
        ///                 "id": 90,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "tagId": 72
        ///             },
        ///             {
        ///                 "id": 91,
        ///                 "contentId": 89,
        ///                 "typeId": 4,
        ///                 "tagId": 838
        ///             }
        ///             ]
        ///             }
        /// </remarks>
        /// <returns></returns>
        // GET: api/ProcessMaps
        [HttpGet("getallprocessmaps")]
        public async Task<IActionResult> GetAllProcessMaps()
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMap", this.GetType().Name);

            try
            {
                IEnumerable<ProcessMapModel> list = await this._processMapsAppService.GetAllProcessMapsAsync().ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get process maps by process map id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "id": 459
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 459,
        ///            "contentId": "M-0001",
        ///            "title": "Test-01",
        ///            "disciplineId": null,
        ///            "subDisciplineId": null,
        ///            "subSubDisciplineId": null,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "assetTypeId": null,
        ///            "assetStatusId": null,
        ///            "approvalRequirementId": null,
        ///            "classifierId": null,
        ///            "confidentialityId": null,
        ///            "revisionTypeId": null,
        ///            "programControlled": true,
        ///            "outsourceable": true,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-02-03T15:38:00.969Z",
        ///            "effectiveTo": "2021-02-03T15:38:00.969Z",
        ///            "createdDateTime": "2021-02-03T15:38:00.969Z",
        ///            "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "lastUpdateDateTime": "2021-02-03T15:38:00.969Z",
        ///            "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "usjurisdictionId": null,
        ///            "usclassificationId": null,
        ///            "classificationRequestNumber": null,
        ///            "classificationDate": "2021-02-03T15:38:00.969Z",
        ///            "Tpmdate": null,
        ///            "swimLanes": [
        ///            {
        ///                 "id": 506,
        ///                 "name": "Test-Swimlane",
        ///                 "description": null,
        ///                 "label": null,
        ///                 "processMapId": 459,
        ///                 "sequenceNumber": null,
        ///                 "color": null,
        ///                 "backgroundColor": null,
        ///                 "borderColor": null,
        ///                 "borderStyle": null,
        ///                 "borderWidth": null,
        ///                 "version": 1,
        ///                 "effectiveFrom": "2021-02-03T00:00:00",
        ///                 "effectiveTo": null,
        ///                 "createdDateTime": "2021-02-03T15:53:32.4654106",
        ///                 "createdUser": "pwesw1",
        ///                 "lastUpdateDateTime": "2021-02-03T15:53:32.4654106",
        ///                 "lastUpdateUser": "pwesw1",
        ///                 "location": null
        ///            }
        ///            ],
        ///            "processMapMeta": [
        ///            {
        ///                 "id": 565,
        ///                 "processMapId": 459,
        ///                 "key": "Test-PMM",
        ///                 "value": "Test-PMM-Value",
        ///                 "version": null,
        ///                 "createdon": null,
        ///                 "createdbyUserid": null,
        ///                 "modifiedon": null,
        ///                 "modifiedbyUserid": null
        ///            }
        ///            ],
        ///            "activityBlocks": [],
        ///            "activityDocuments": [],
        ///            "phases": [],
        ///            "contentPhases": [
        ///             {
        ///                 "id": 217,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "phaseId": 3
        ///             },
        ///             {
        ///                 "id": 218,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "phaseId": 5
        ///             }
        ///             ],
        ///             "contentExportCompliances": [
        ///             {
        ///                 "id": 213,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "exportComplianceId": 1
        ///             },
        ///             {
        ///                 "id": 214,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "exportComplianceId": 2
        ///             }
        ///             ],
        ///             "contentTags": [
        ///             {
        ///                 "id": 90,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "tagId": 72
        ///             },
        ///             {
        ///                 "id": 91,
        ///                 "contentId": 459,
        ///                 "typeId": 4,
        ///                 "tagId": 838
        ///             }
        ///             ]
        ///        }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="contentType"></param>
        /// <param name="status"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <param name="currentUserEmail"></param>
        /// <returns>ProcessMapModel</returns>
        [HttpGet("getprocessmapsbyid")]
        [CustomAuthorization]
        public async Task<IActionResult> GetProcessMapsById(long id, string contentType, string status, string contentId, int version, string currentUserEmail)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);

            try
            {
                status = StringHelper.GetApiStatus(status);
                
                if (id <= 0 && string.IsNullOrEmpty(contentId))
                {
                    return this.BadRequest("Please enter valid process map id or contentId & version.");
                }

                var processMap = new ProcessMapModel();
                if (string.Equals(status, "published", StringComparison.CurrentCultureIgnoreCase))
                {
                    processMap = await this._kaService.GetProcessMapByIdOrContentId((int)id, contentId, version).ConfigureAwait(false);
                }
                else
                {
                    if (id > 0)
                    {
                        processMap = await this._processMapsAppService.GetProcessMapByIdAsync(id).ConfigureAwait(false);
                    }
                    else
                    {
                        processMap = await this._processMapsAppService.GetProcessMapByContentId(contentId, version);
                    }
                }
                return this.Ok(processMap);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// GetProcessMapsFlowViewById
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("getprocessmapsflowviewbyid")]
        public async Task<IActionResult> GetProcessMapsFlowViewById(long id, string status, string contentId, int version)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);

            try
            {
                status = StringHelper.GetApiStatus(status);
                
                if (id <= 0 && string.IsNullOrEmpty(contentId))
                {
                    return this.BadRequest("Please enter valid process map id or contentId & version.");
                }

                var processMap = new ProcessMapModel();
                if (string.Equals(status, "published", StringComparison.CurrentCultureIgnoreCase))
                {
                    processMap = await this._kaService.GetProcessMapFlowViewByIdOrContentId((int)id, contentId, version).ConfigureAwait(false);
                }
                else
                {
                    processMap = await this._processMapsAppService.GetProcessMapFlowViewByIdAsync(id, contentId, version).ConfigureAwait(false);
                }
                return this.Ok(processMap);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Create process maps.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///         {
        ///             "id": 0,
        ///             "title": "valid title"
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
        [HttpPost("createprocessmaps")]
        public async Task<IActionResult> CreateProcessMaps([FromBody] ProcessMapModel processMapModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(processMapModel.Title))
                {
                    return this.BadRequest("Please enter valid title.");
                }

                ProcessMapInputOutputModel obj = await this._processMapsAppService.CreateProcessMapAsync(processMapModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Create new process maps from existing process maps.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "id": 1031,
        ///            "contentId": "MAP-000009",
        ///            "title": "ORM9",
        ///            "disciplineId": 1,
        ///            "subDisciplineId": 1,
        ///            "subSubDisciplineId": 2,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "assetTypeId": 4,
        ///            "assetStatusId": 1,
        ///            "approvalRequirementId": null,
        ///            "classifierId": null,
        ///            "confidentialityId": null,
        ///            "revisionTypeId": null,
        ///            "programControlled": null,
        ///            "outsourceable": false,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-01-18T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-01-18T09:21:52.5725952",
        ///            "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "lastUpdateDateTime": "2021-01-18T09:21:52.5725952",
        ///            "lastUpdateUser": "",
        ///            "usjurisdictionId": null,
        ///            "usclassificationId": null,
        ///            "classificationRequestNumber": null,
        ///            "classificationDate": null,
        ///            "tpmdate": null,
        ///            "exportAuthorityId": null,
        ///            "controllingProgramId": null,
        ///            "contentOwnerId": pwesw1@pwesw2.onmicrosoft.com,
        ///            "keywords": null,
        ///            "author": null,
        ///            "confidentiality": null,
        ///            "purpose": null,
        ///            "processInstId": null,
        ///            "customId": null
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 1031,
        ///            "contentId": "MAP-000009",
        ///            "title": "ORM9",
        ///            "disciplineId": 1,
        ///            "subDisciplineId": 1,
        ///            "subSubDisciplineId": 2,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "assetTypeId": 4,
        ///            "assetStatusId": 1,
        ///            "approvalRequirementId": null,
        ///            "classifierId": null,
        ///            "confidentialityId": null,
        ///            "revisionTypeId": null,
        ///            "programControlled": null,
        ///            "outsourceable": false,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-01-18T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-01-18T09:21:52.5725952",
        ///            "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "lastUpdateDateTime": "2021-01-18T09:21:52.5725952",
        ///            "lastUpdateUser": "",
        ///            "usjurisdictionId": null,
        ///            "usclassificationId": null,
        ///            "classificationRequestNumber": null,
        ///            "classificationDate": null,
        ///            "tpmdate": null,
        ///            "exportAuthorityId": null,
        ///            "controllingProgramId": null,
        ///            "contentOwnerId": pwesw1@pwesw2.onmicrosoft.com,
        ///            "keywords": null,
        ///            "author": null,
        ///            "confidentiality": null,
        ///            "purpose": null,
        ///            "processInstId": null,
        ///            "customId": null
        ///        }
        /// </remarks>
        /// <param name="processMapExistingModel"></param>
        /// <returns>processMapExistingModel</returns>
        [HttpPost("createfromexistingprocessmaps")]
        public async Task<IActionResult> CreateFromExistingProcessMaps([FromBody] ProcessMapExistingModel processMapExistingModel)
        {
            return this.BadRequest("This api is not available anymore, please use api/processmaps/saveasstepflow api instead.");
        }

        /// <summary>
        /// SaveAsStepFlow
        /// </summary>
        /// <param name="saveAsModel"></param>
        /// <returns></returns>
        [HttpPost("saveasstepflow")]
        public async Task<IActionResult> SaveAsStepFlow([FromBody] ProcessMapsSaveAsModel saveAsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (saveAsModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid Id.");
                }
                if (string.IsNullOrEmpty(saveAsModel.CreatedUser))
                {
                    return this.BadRequest("Please enter valid createdUser.");
                }

                var newStepFlow = await this._cloneService.SaveAsStepFlowAsync(saveAsModel).ConfigureAwait(false);

                return this.Ok(newStepFlow);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// SaveAsProcessMap
        /// </summary>
        /// <param name="stepFlow"></param>
        /// <returns></returns>
        [HttpPost("saveasstep")]
        public async Task<IActionResult> SaveAsStep([FromBody] ProcessMapsSaveAsModel saveAsModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (saveAsModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid Id.");
                }
                if (string.IsNullOrEmpty(saveAsModel.CreatedUser))
                {
                    return this.BadRequest("Please enter valid createdUser.");
                }

                var newStepFlow = await this._cloneService.SaveAsStepAsync(saveAsModel).ConfigureAwait(false);

                return this.Ok(newStepFlow);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ReviseStepFlow
        /// </summary>
        /// <param name="reviseModel"></param>
        /// <returns></returns>
        [HttpPost("revisestepflow")]
        public async Task<IActionResult> ReviseStepFlow([FromBody] ProcessMapsReviseModel reviseModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (reviseModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid Id.");
                }
                if (string.IsNullOrEmpty(reviseModel.CreatedUser))
                {
                    return this.BadRequest("Please enter valid createdUser.");
                }

                var revisionChecking = await this._cloneService.IsAbleToReviseAsync(reviseModel).ConfigureAwait(false);
                if (!revisionChecking.IsAbleToRevise)
                {
                    return this.BadRequest(revisionChecking.Message);
                }

                var newStepFlow = await this._cloneService.ReviseStepFlowAsync(reviseModel).ConfigureAwait(false);
                return this.Ok(newStepFlow);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ReviseProcessMap
        /// </summary>
        /// <param name="reviseModel"></param>
        /// <returns></returns>
        [HttpPost("revisestep")]
        public async Task<IActionResult> ReviseStep([FromBody] ProcessMapsReviseModel reviseModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (reviseModel.Id <= 0)
                {
                    return this.BadRequest("Please enter valid Id.");
                }
                if (string.IsNullOrEmpty(reviseModel.CreatedUser))
                {
                    return this.BadRequest("Please enter valid createdUser.");
                }

                var revisionChecking = await this._cloneService.IsAbleToReviseAsync(reviseModel).ConfigureAwait(false);
                if (!revisionChecking.IsAbleToRevise)
                {
                    return this.BadRequest(revisionChecking.Message);
                }

                var newStepFlow = await this._cloneService.ReviseStepAsync(reviseModel).ConfigureAwait(false);
                return this.Ok(newStepFlow);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates a specified process map
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 231,
        ///             "contentId": "M-0231",
        ///             "title": "GSE Expanded Services",
        ///             "disciplineId": null,
        ///             "subDisciplineId": null,
        ///             "subSubDisciplineId": null,
        ///             "subSubSubDisciplineId": null,
        ///             "disciplineCodeId": null,
        ///             "assetTypeId": 4,
        ///             "assetStatusId": 2,
        ///             "approvalRequirementId": null,
        ///             "classifierId": null,
        ///             "phaseId": null,
        ///             "confidentialityId": null,
        ///             "controllingProgramId": null,
        ///             "exportComplianceTouchPointId": null,
        ///             "revisionTypeId": null,
        ///             "contentOwnerId": null,
        ///             "programControlled": null,
        ///             "outsourceable": null,
        ///             "tagId": null
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 231,
        ///            "contentId": "M-0231",
        ///            "title": "GSE Expanded Services",
        ///            "disciplineId": null,
        ///            "subDisciplineId": null,
        ///            "subSubDisciplineId": null,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "assetTypeId": 4,
        ///            "assetStatusId": 2,
        ///            "approvalRequirementId": null,
        ///            "classifierId": null,
        ///            "phaseId": null,
        ///            "confidentialityId": null,
        ///            "controllingProgramId": null,
        ///            "exportComplianceTouchPointId": null,
        ///            "revisionTypeId": null,
        ///            "contentOwnerId": null,
        ///            "programControlled": null,
        ///            "outsourceable": null,
        ///            "tagId": null,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-01-19T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-01-19T10:25:35.1683054",
        ///            "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "lastUpdateDateTime": "2021-01-19T10:25:35.1683054",
        ///            "lastUpdateUser": "",
        ///            "firstPublicationDateTime": null,
        ///            "usjurisdictionId": null,
        ///            "usclassificationId": null,
        ///            "classificationRequestNumber": null,
        ///            "classificationDate": null,
        ///            "securityAttributesId": null,
        ///            "exportComplianceTouchPointComments": "",
        ///            "assetHandleId": null,
        ///            "sourceAssetHandleId": null,
        ///            "scopeId": null,
        ///            "assetClassId": null,
        ///            "swimLanes": [],
        ///            "processMapMeta": [],
        ///            "activityBlocks": [],
        ///            "activityDocuments": []
        ///        }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="map"></param>
        // POST: api/ProcessMaps
        [HttpPut("updateprocessmaps")]
        public async Task<IActionResult> UpdateProcessMaps([FromBody]ProcessMapModel processMapModel)
        {
            Dictionary<string, string> errorProperties =
                _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(processMapModel.ContentId))
                {
                    return BadRequest("Please enter ContentId.");
                }

                ProcessMapModel obj = await _processMapsAppService.UpdateProcessMapAsync(processMapModel);

                return Ok(obj);
            }
            catch (Exception ex)
            {
                _logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update properties in process map
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 9,
        ///             "title": "Dummy M-9",
        ///             "approvalRequirementId":3,
        ///             "DisciplineCode":"ABS",
        ///             "exportComplienceTouchPoint":[2,3],
        ///             "phaseId": [2,4],
        ///             "confidentialityId":3,
        ///             "ProgramControlled": true,
        ///             "DocumentType":"1",
        ///             "ClassifierId":2,
        ///             "CreatorClockId":"sm@pwesw2.onmicrosoft.com",
        ///             "ContentStatus":"Draft",
        ///             "ContentTypeId":4,
        ///             "CreatedOn":"2020-12-24T07:20:00.823Z"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "id": 9,
        ///             "title": "Dummy M-9",
        ///             "approvalRequirementId":3,
        ///             "DisciplineCode":"ABS",
        ///             "exportComplienceTouchPoint":[2,3],
        ///             "phaseId": [2,4],
        ///             "confidentialityId":3,
        ///             "ProgramControlled": true,
        ///             "DocumentType":"1",
        ///             "ClassifierId":2,
        ///             "CreatorClockId":"sm@pwesw2.onmicrosoft.com",
        ///             "ContentStatus":"Draft",
        ///             "ContentTypeId":4,
        ///             "CreatedOn":"2020-12-24T07:20:00.823Z"
        ///        }
        /// </remarks>
        /// <param name="processMapModel"></param>       
        // POST: api/ProcessMaps
        [HttpPut("UpdatePropertiesInProcessMap")]
        public async Task<IActionResult> UpdatePropertiesInProcessMap(ProcessMapModel processMapModel)
        {
            Dictionary<string, string> errorProperties =
               this._logManager.GetTrackingProperties(
                   this.GetType().Assembly.GetName().Name,
                   this.GetType().Namespace,
                   System.Reflection.MethodBase.GetCurrentMethod().Name,
                   "Activity Page", this.GetType().Name);
            try
            {
                if (processMapModel.Id <= 0)
                {
                    return this.BadRequest("Please enter correct process map id.");
                }

                if (string.IsNullOrWhiteSpace(processMapModel.Title))
                {
                    return BadRequest("Please enter title.");
                }

                ProcessMapModel obj = await this._processMapsAppService.UpdatePropertiesInProcessMapAsync(processMapModel).ConfigureAwait(false);
                return Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Updates asset status of process maps
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 962,
        ///             "assetStatusId": 2
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 962,
        ///            "assetStatusId": 2,
        ///        }
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="assetStatusId"></param>
        // POST: api/ProcessMaps
        [HttpPut("updateprocessmapstatus")]
        public async Task<IActionResult> UpdateProcessMapStatus([FromBody] ProcessMapModel processMapModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (processMapModel.Id <= 0)
                {
                    return this.BadRequest("Please enter id.");
                }

                ProcessMapModel obj = await this._processMapsAppService.UpdateProcessMapStatusAsync(processMapModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Update ProcessMaps Purpose Only
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("updateprocessmapspurpose")]
        public async Task<IActionResult> UpdateProcessMapsPurpose([FromBody] ProcessMapsPurposeModel model)
        {
            Dictionary<string, string> errorProperties =
                _logManager.GetTrackingProperties(GetType().Assembly.GetName().Name, GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(model.ContentId))
                {
                    return BadRequest("Please enter ContentId.");
                }

                var result = await this._processMapsAppService.UpdateProcessMapPurposeAsync(model).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Delete process map
        /// </summary>
        /// <remarks>
        ///Sample Response
        ///
        ///          192
        /// </remarks>
        /// <param name="id"></param>
        // DELETE: api/ProcessMaps/{id}
        [HttpDelete("deleteprocessmaps")]
        public async Task<IActionResult> DeleteProcessMaps(int id)
        {

            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Test Entity", GetType().Name);
            try
            {
                if (id < 0)
                {
                    return this.BadRequest("Please enter valid id");
                }

                bool obj = await this._processMapsAppService.DeleteProcessMapAsync(id).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get all process maps on basis of user id.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "Userid": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///            "id": 89,
        ///            "contentId": "M-0009",
        ///            "title": "GSE Expanded Services",
        ///            "disciplineId": null,
        ///            "subDisciplineId": null,
        ///            "subSubDisciplineId": null,
        ///            "subSubSubDisciplineId": null,
        ///            "disciplineCodeId": null,
        ///            "assetTypeId": 4,
        ///            "assetStatusId": 2,
        ///            "approvalRequirementId": null,
        ///            "classifierId": null,
        ///            "phaseId": null,
        ///            "confidentialityId": null,
        ///            "controllingProgramId": null,
        ///            "exportComplianceTouchPointId": null,
        ///            "revisionTypeId": null,
        ///            "contentOwnerId": null,
        ///            "programControlled": null,
        ///            "outsourceable": null,
        ///            "tagId": null,
        ///            "version": 1,
        ///            "effectiveFrom": "2021-01-19T00:00:00",
        ///            "effectiveTo": null,
        ///            "createdDateTime": "2021-01-19T10:25:35.1683054",
        ///            "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///            "lastUpdateDateTime": "2021-01-19T10:25:35.1683054",
        ///            "lastUpdateUser": "",
        ///            "firstPublicationDateTime": null,
        ///            "usjurisdictionId": null,
        ///            "usclassificationId": null,
        ///            "classificationRequestNumber": null,
        ///            "classificationDate": null,
        ///            "securityAttributesId": null,
        ///            "exportComplianceTouchPointComments": "",
        ///            "assetHandleId": null,
        ///            "sourceAssetHandleId": null,
        ///            "scopeId": null,
        ///            "assetClassId": null,
        ///            "swimLanes": [],
        ///            "processMapMeta": [],
        ///            "activityBlocks": [],
        ///            "activityDocuments": []
        ///        }
        /// </remarks>
        /// <param name="userId"></param>
        /// <returns>ProcessMapModel</returns>
        [Obsolete("This api is depricated")]
        [HttpGet("getallprocessmapsbyuserid")]
        public async Task<IActionResult> GetAllProcessMapsByUserId(string userId)
        {/*
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);

            try
            {
                if (string.IsNullOrWhiteSpace(userId))
                {
                    return this.BadRequest("Please enter valid user id.");
                }

                IEnumerable<ProcessMapModel> list = await this._processMapsAppService.GetAllProcessMapsByUserIdAsync(userId).ConfigureAwait(false);

                return this.Ok(list);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }*/
            return this.BadRequest("This api is depricated.");
        }

        /// <summary>
        /// ExportStepFlowToExcel 
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///            "id": "89",
        ///            "url": "https://eswnextgen.azurewebsites.net/view-document/"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///        }
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("exportstepflowtoexcel")]
        public async Task<IActionResult> ExportStepFlowToExcel(int id, string url)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);

            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter valid process map id.");
                }
                if (string.IsNullOrEmpty(url))
                {
                    return this.BadRequest("Please enter valid universal url.");
                }

                // For xlsx file
                // string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // string fileName = "map.xlsx";

                // For xls file
                string contentType = "application/vnd.ms-excel";
                string fileName = "StepFlow.xlsx";
                byte[] content = await this._kaExportService.ExportStepFlowToExcel(id, url).ConfigureAwait(false);

                return this.File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// ExportProcessMapToExcel
        /// </summary>
        /// <param name="id"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        [HttpGet("exportsteptoexcel")]
        public async Task<IActionResult> ExportStepToExcel(int id, string url)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);

            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter valid process map id.");
                }
                if (string.IsNullOrEmpty(url))
                {
                    return this.BadRequest("Please enter valid universal url.");
                }

                // For xlsx file
                // string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                // string fileName = "map.xlsx";

                // For xls file
                string contentType = "application/vnd.ms-excel";
                string fileName = "Step.xlsx";
                byte[] content = await this._kaExportService.ExportStepToExcel(id, url).ConfigureAwait(false);

                return this.File(content, contentType, fileName);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// CreateStepAsync.
        /// </summary>
        /// <param name="activityBlock"></param>
        /// <returns></returns>
        [HttpPost("CreateStep")]
        public async Task<IActionResult> CreateStepAsync([FromBody] ActivityBlocksModel activityBlock)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (activityBlock.ProcessMapId == 0)
                {
                    return this.BadRequest("Please enter ProcessMap Id.");
                }

                ProcessMapModel obj = await this._processMapsAppService.CreateStepAsync(activityBlock).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// GetStepFlowByIdOrContentId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("GetStepFlowByIdOrContentId")]
        public async Task<IActionResult> GetStepFlowByIdOrContentId(int id, string contentId, int? version, string status)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                status = StringHelper.GetApiStatus(status);

                if (id <= 0 && string.IsNullOrEmpty(contentId?.Trim()))
                {
                    return this.BadRequest("Please enter any of valid StepFlow id or valid contentId.");
                }

                if (string.Equals(status, "published", StringComparison.CurrentCultureIgnoreCase))
                {
                    var result = await this._kaService.GetStepFlowByIdOrContentIdAsync(id, contentId, version ?? 0);
                    return this.Ok(result);
                }
                else
                {
                    var obj = await this._processMapsAppService.GetStepFlowByIdOrContentIdAsync(id, contentId, version).ConfigureAwait(false);
                    return this.Ok(obj);
                }
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// GetStepByIdOrContentIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        [HttpGet("GetStepByIdOrContentId")]
        public async Task<IActionResult> GetStepByIdOrContentIdAsync(int id, string contentId, int? version, string status)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                status = StringHelper.GetApiStatus(status);
                
                if (id <= 0 && string.IsNullOrEmpty(contentId?.Trim()))
                {
                    return this.BadRequest("Please enter any of valid Step id or valid contentId.");
                }

                if (string.Equals(status, "published", StringComparison.CurrentCultureIgnoreCase))
                {
                    var obj = await this._kaService.GetStepByIdOrContentIdAsync(id, contentId, version ?? 0).ConfigureAwait(false);
                    return this.Ok(obj);
                }
                else
                {
                    var obj = await this._processMapsAppService.GetStepByIdOrContentIdAsync(id, contentId, version).ConfigureAwait(false);
                    return this.Ok(obj);
                }
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// DeleteStep.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        [HttpDelete("DeleteStep")]
        public async Task<IActionResult> DeleteStepAsync(long id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "DeleteStep ProcessMaps", this.GetType().Name);
            try
            {
                if (id <= 0)
                {
                    return this.BadRequest("Please enter any of valid Step id.");
                }

                var obj = await this._processMapsAppService.DeleteStep(id).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// GetProcessMapIdByContentIdAndVersion.
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        [HttpGet("GetProcessMapIdByContentIdAndVersion")]
        public async Task<IActionResult> GetProcessMapIdByContentIdAndVersionAsync(string contentId, int version)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "GetProcessMapIdByContentIdAndVersionAsync", this.GetType().Name);
            try
            {
                //if (string.IsNullOrEmpty(contentId)
                //    || (contentId.Split('-').Length != 3
                //    || (contentId.Split('-')[1].ToUpper() != "F"
                //    && contentId.Split('-')[1].ToUpper() != "P"
                //    && contentId.Split('-')[1].ToUpper() != "M")
                //    || !int.TryParse(contentId.Split('-')[2], out int contentNo)))
                if (string.IsNullOrEmpty(contentId))
                {
                    return this.BadRequest("Please provide a valid process map Content Id.");
                }

                if (version <= 0)
                {
                    return this.BadRequest("Please provide a valid non-zero Version number.");
                }

                var obj = await this._processMapsAppService.GetProcessMapIdByContentIdAndVersionAsync(contentId, version).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// RemoveStepFromStepFlow
        /// </summary>
        /// <param name="stepFlowId"></param>
        /// <param name="stepContentId"></param>
        /// <returns></returns>
        [HttpDelete("RemoveStepFromStepFlow")]
        public async Task<IActionResult> RemoveStepFromStepFlow(int stepFlowId, string stepContentId)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "ProcessMaps", this.GetType().Name);
            try
            {
                if (stepFlowId <= 0)
                {
                    return this.BadRequest("Please enter any of valid StepFlowId.");
                }
                if (string.IsNullOrEmpty(stepContentId))
                {
                    return this.BadRequest("Please enter any of valid Step ContentId.");
                }

                var obj = await this._processMapsAppService.RemoveStepFromStepFlowAsync(stepFlowId, stepContentId).ConfigureAwait(false);

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
