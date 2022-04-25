using System;
using System.Collections.Generic;
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
    [Route("api/knowledgeassets")]
    [ApiController]
    public class KnowledgeAssetsController : ControllerBase
    {
        private readonly IKnowledgeAssetsAppService _knowledgeAssetsAppService;
        private readonly ILogManager _logManager;

        public KnowledgeAssetsController(IKnowledgeAssetsAppService knowledgeAssetsAppService, ILogManager logManager)
        {
            this._knowledgeAssetsAppService = knowledgeAssetsAppService;
            this._logManager = logManager;
        }
        
        [HttpPost("createknowledgeasset")]
        public async Task<IActionResult> CreateKnowledgeAsset([FromBody] KnowledgeAssetModel model)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name,
                this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Knowledge Asset", this.GetType().Name);
            try
            {
                if (model.AssetStatusId <= 0)
                {
                    return this.BadRequest("Please enter a valid assetStatusId.");
                }
                if (string.IsNullOrWhiteSpace(model.AssetTypeCode))
                {
                    return this.BadRequest("Please enter a valid assetTypeCode.");
                }
                if (model.RevisionTypeId <= 0)
                {
                    return this.BadRequest("Please enter a valid revisionTypeId.");
                }

                var result = await this._knowledgeAssetsAppService.CreateKnowledgeAssetAsync(model).ConfigureAwait(false);

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
    }
}