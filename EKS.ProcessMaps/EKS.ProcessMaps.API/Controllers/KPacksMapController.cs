using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EKS.Common.ExceptionHandler;
using EKS.Common.Logging;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace EKS.ProcessMaps.API.Controllers
{
    [EnableCors(PolicyName = "FrontEndWebApp")]
    [Route("api/kpack")]
    [ApiController]
    public class KPacksMapController : ControllerBase
    {
        private readonly IKPacksAppService _kPacksAppService;
        private readonly IContainerItemAppService _containerItemAppService;
        
        private readonly ILogManager _logManager;

        public KPacksMapController(
            IKPacksAppService kPacksAppService,
            IContainerItemAppService containerItemAppService,
            ILogManager logManager)
        {
            this._kPacksAppService = kPacksAppService;
            this._containerItemAppService = containerItemAppService;
            this._logManager = logManager;
        }

        /// <summary>
        ///  Create a kpacks map
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "kPacksMapId": 493,
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        /// </remarks>
        /// <param name="kPackModel"></param>
        /// <returns></returns>
        [HttpPost("createkpackmap")]
        public async Task<IActionResult> CreateKPacksMap([FromBody] KPackMapModel kPackModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "KPacks", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(kPackModel.ContentAssetId))
                {
                    return this.BadRequest("Please enter id.");
                }

                KPackMapModel obj = await this._kPacksAppService.CreateKPacksMapAsync(kPackModel).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        ///  Create more than one kpacks map
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///     [
        ///        {
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        },
        ///        {
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///      ]
        ///
        /// Sample Response:
        ///
        ///      [
        ///        {
        ///             "kPacksMapId": 494,
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        },
        ///        {
        ///             "kPacksMapId": 495,
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///             "createdUser": "pwesw1@pwesw2.onmicrosoft.com",
        ///             "lastUpdateUser": "pwesw1@pwesw2.onmicrosoft.com"
        ///        }
        ///      ]
        /// </remarks>
        /// <param name="kPackModel"></param>
        /// <returns></returns>
        [HttpPost("addkpackmaps")]
        public async Task<IActionResult> AddKPacksMaps([FromBody] IEnumerable<KPackMapModel> kPackMapModels)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "KPacks", this.GetType().Name);
            try
            {
                foreach (var model in kPackMapModels)
                {
                    if (string.IsNullOrWhiteSpace(model.ContentAssetId))
                    {
                        return this.BadRequest("Please enter ContentAssetId.");
                    }
                }

                IEnumerable<KPackMapModel> obj = await this._kPacksAppService.AddKPacksMapAsync(kPackMapModels).ConfigureAwait(false);

                return this.Ok(obj);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Get kpackmaps by parent content asset id
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "parentContentAssetId": "AA-F-012995",
        ///             "version": 1
        ///        }
        ///
        /// Sample Response:
        ///
        ///        {
        ///             "title": "Kpack_05_19_21",
        ///             "disciplineCode": "FC",
        ///             "subSubDisciplineName": "Fan and Compressor",
        ///             "contentTypeName": "Knowledge Pack",
        ///             "kPacksMapId": 25,
        ///             "parentContentAssetId": "AA-F-012995","contentAssetId": "FC-K-012271",
        ///             "parentContentTypeId": 13,
        ///     }
        /// </remarks>
        /// <param name="parentContentAssetId"></param>
        /// <returns></returns>
        [HttpGet("getkpackmapbyparentcontentassetid")]
        public async Task<IActionResult> GetKPacksMapByParentContentAssetId(string parentContentAssetId, int? version, string status)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "KPacks", this.GetType().Name);

            try
            {
                if (string.IsNullOrEmpty(parentContentAssetId))
                {
                    return this.BadRequest("Please enter valid ParentContentAssetId.");
                }

                var result = new List<KPackMapExtendModel>();
                if (string.Equals(status, "published", StringComparison.CurrentCultureIgnoreCase))
                {
                    result = await this._containerItemAppService.GetKpackMapByKnowledgeAssetId(parentContentAssetId, version ?? 0).ConfigureAwait(false);
                }
                else
                {
                    result = await this._kPacksAppService.GetKPacksMapByParentContentAssetIdAsync(parentContentAssetId, version).ConfigureAwait(false);
                }

                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }

        /// <summary>
        /// Deletes a specified kpack map.
        /// </summary>
        /// <remarks>
        /// Sample Request:
        ///
        ///        {
        ///             "id": 519
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
        [HttpDelete("deletekpackmap")]
        public async Task<IActionResult> Deletekpackmap(int id)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);
            try
            {
                if (id < 0)
                {
                    return this.BadRequest("Please enter valid id");
                }

                bool obj = await this._kPacksAppService.DeleteKPacksMapAsync(id).ConfigureAwait(false);

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