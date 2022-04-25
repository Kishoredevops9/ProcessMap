namespace EKS.ProcessMaps.Filters
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Security.Claims;
    using EKS.Common.Authorization;
    using EKS.Common.Logging;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using EKS.ProcessMaps.Helper.Enum;

    /// <summary>
    /// CustomAuthorization
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorization : Attribute, IAuthorizationFilter
    {
        private ICustomAppService customAppService;
        private IConfiguration configuration;
        private ILogManager logManager;
        private IAuthorizationLogService authorizationLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            if (filterContext == null)
            {
                return;
            }

            customAppService = (ICustomAppService)filterContext.HttpContext.RequestServices.GetService(typeof(ICustomAppService));
            logManager = (ILogManager)filterContext.HttpContext.RequestServices.GetService(typeof(ILogManager));

            var authModel = new AuthContextModel(filterContext);
            var requestQuery = new AuthRequestQueryModel(filterContext.HttpContext.Request);
            var requestHeader = new AuthRequestHeaderModel(authModel.HeaderData);
           
            if (string.IsNullOrEmpty(authModel.Token))
            {
                ApplyExpectationFailed(filterContext);
            }
            else
            {
                if (!IsValidToken(authModel.Token))
                {
                    ApplyUnAuthorize(filterContext, authModel.Token, "Invalid Token");
                }
                else
                {
                    bool authorized = true;

                    if (requestHeader.HasValue && requestQuery.HasValue)
                    {
                        logManager.Info(string.Format("headerData:{0}", authModel.HeaderData));

                        switch (requestQuery.Status)
                        {
                            case "draft":
                                authorized = CheckDraftPermission(requestQuery.UserName, requestHeader.Nationality, requestHeader.PwEmploymentCode, requestHeader.EksGroup, requestQuery.Id, requestQuery.ContentType);
                                logManager.Info(string.Format("authorized:{0}", authorized.ToString()));
                                break;
                            case "published":
                                configuration = (IConfiguration)filterContext.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                                var dbConnection = configuration["ConnectionString:PublishedContentDB"];

                                var applicationAuthorization = new ApplicationAuthorization();
                                authorized = applicationAuthorization.AuthorizeAssetAccess(requestQuery.ContentId, requestQuery.Id, requestHeader.GlobalUId, requestHeader.Nationality, requestHeader.PwEmploymentCode,
                                    requestHeader.EksGroup, authModel.RemoteIP, authModel.ForwardedFor, authModel.MethodType, authModel.FunctionName, authModel.ControllerName, dbConnection).Result;
                                logManager.Info(string.Format("authorized:{0}", authorized.ToString()));
                                break;
                        }
                    }

                    if (authorized == false)
                    {
                        ApplyUnAuthorize(filterContext, authModel.Token, "User Not Authorized!");
                    }
                    else
                    {
                        ApplyAuthorize(filterContext, authModel.Token);
                        return;
                    }

                    // Adding log here for draft content only. For published content AuthorizationLog getting added in sp AuthorizeAssetAccess
                    if (requestQuery.Status == "draft")
                    {
                        string operationCode = "RC";
                        authorizationLogService = (IAuthorizationLogService)filterContext.HttpContext.RequestServices.GetService(typeof(IAuthorizationLogService));
                        AddAuthorizationLog(operationCode, requestHeader.GlobalUId, requestQuery.ContentId, 1, authorized, requestHeader.Nationality, 
                            requestHeader.PwEmploymentCode, requestHeader.EksGroup, null, null, null, null, authModel.RemoteIP, authModel.ForwardedFor);
                    }
                }
            }
        }

        private static void ApplyExpectationFailed(AuthorizationFilterContext filterContext)
        {
            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.ExpectationFailed;
            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Please Provide authToken";
            filterContext.Result = new JsonResult("Please Provide authToken")
            {
                Value = new
                {
                    Status = "Error",
                    Message = "Please Provide authToken"
                },
            };
        }

        private static void ApplyAuthorize(AuthorizationFilterContext filterContext, string authToken)
        {
            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

            filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");
        }

        private static void ApplyUnAuthorize(AuthorizationFilterContext filterContext, string token, string message)
        {
            filterContext.HttpContext.Response.Headers.Add("authToken", token);
            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
            filterContext.Result = new JsonResult("NotAuthorized")
            {
                Value = new
                {
                    Status = "Error",
                    Message = message
                },
            };
        }

        public bool IsValidToken(string authToken)
        {
            //validate Token here  
            return true;
        }

        private bool CheckDraftPermission(string userName, string nationality, string pwEmployment, string userGroup, int contentId, string contentType)
        {
            var retVal = false;
            try
            {
                var result = customAppService.GetContentData(contentId, contentType);

                logManager.Info(string.Format("author from db:{0}", result.Author));
                logManager.Info(string.Format("exportAuthority from db:{0}", result.ExportAuthorityId));

                if (CheckProgramControlled(result.ProgramControlled, result.ControllingProgramGroup, userGroup)
                    && (result.Author.Contains(userName, StringComparison.OrdinalIgnoreCase)
                    || result.ContentOwner.Contains(userName, StringComparison.OrdinalIgnoreCase)
                    )
                    && CheckNationality(nationality, result.ExportAuthorityId)
                    && CheckOutsourcable(pwEmployment, result.OutSourcable))
                {
                    retVal = true;
                }
            }
            catch (Exception ex)
            {
                logManager.Error(ex.Message, ex, null, ex.StackTrace);
            }
            return retVal;
        }

        private bool CheckNationality(string userNationality, int? exportAuthorityId)
        {
            var result = false;

            var nationality = (string.Equals(userNationality, "usa", StringComparison.OrdinalIgnoreCase)
                || string.Equals(userNationality, "usgov", StringComparison.OrdinalIgnoreCase)) ? 1 :
                string.Equals(userNationality, "canada", StringComparison.OrdinalIgnoreCase) ? 2 : 3;

            if (nationality <= Convert.ToInt32(exportAuthorityId))
            {
                result = true;
            }
            return result;
        }

        private bool CheckOutsourcable(string pwEmployment, bool outsourcable)
        {
            var result = true;
            if (pwEmployment == PWEmployments.CONT.ToString() && !outsourcable)
            {
                result = false;
            }
            return result;
        }

        private bool CheckProgramControlled(bool programControlled, string programControlGroup, string userGroup)
        {
            var result = true;
            if (programControlled && !userGroup.Contains(programControlGroup, StringComparison.OrdinalIgnoreCase))
            {
                result = false;
            }
            return result;
        }

        private void AddAuthorizationLog(string operationCode, string user, string contentId, int version, bool outcome, string nationality,
            string pwEmploymentCode, string groups, int? usJurisdictionId, int? usClassificationId, bool? outsourcableInd, int? confidentialityId,
            string remoteAddress, string forwardedFor)
        {
            try
            {
                authorizationLogService.AddAuthorizationLogAsync(operationCode, user, contentId, version, outcome, nationality, pwEmploymentCode, groups,
                    usJurisdictionId, usClassificationId, outsourcableInd, confidentialityId, remoteAddress, forwardedFor).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                return;
            }
        }

    }
}
