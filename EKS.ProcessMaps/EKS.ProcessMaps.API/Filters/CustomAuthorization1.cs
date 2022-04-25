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
    public class CustomAuthorization1 : Attribute, IAuthorizationFilter
    {
        private ICustomAppService customAppService;
        private IApplicationAuthorization applicationAuthorization;
        private IConfiguration configuration;
        private ILogManager logManager;
        private IAuthorizationLogService authorizationLogService;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public void OnAuthorization(AuthorizationFilterContext filterContext)
        {
            customAppService = (ICustomAppService)filterContext.HttpContext.RequestServices.GetService(typeof(ICustomAppService));
            logManager = (ILogManager)filterContext.HttpContext.RequestServices.GetService(typeof(ILogManager));
            //applicationAuthorization = (IApplicationAuthorization)filterContext.HttpContext.RequestServices.GetService(typeof(IApplicationAuthorization));
            if (filterContext != null)
            {
                string methodType = filterContext.HttpContext.Request.Method.ToString();
                string remoteIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
                string userName = filterContext.HttpContext.User.ToString();
                string functionName = filterContext.HttpContext.Request.RouteValues["action"].ToString();
                string controllerName = filterContext.HttpContext.Request.RouteValues["controller"].ToString();
                string forwardedFor = filterContext.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();

                logManager.Info(string.Format("RequestPath:{0}", filterContext.HttpContext.Request.Path.Value));

                var dictionaryData = new Dictionary<string, string>();
                Microsoft.Extensions.Primitives.StringValues authTokens;
                filterContext.HttpContext.Request.Headers.TryGetValue("Authorization", out authTokens);

                var headerData = filterContext.HttpContext.Request.Headers["userinfo"].ToString();
                //var headerData = JsonConvert.DeserializeObject<HeaderDataModel>(userInfoHeader);

                //Added dummy token as its not there in client env
                var _token = "temptoken"; //authTokens.FirstOrDefault();
                //if (filterContext.HttpContext.User.Identity is ClaimsIdentity identity)
                //{
                //    var email = identity.FindFirst(ClaimTypes.Name).Value;
                //}


                if ((filterContext.HttpContext.Request).QueryString.HasValue)
                {
                    var keys = filterContext.HttpContext.Request.Query.Keys;

                    foreach (var item in keys)
                    {
                        Microsoft.Extensions.Primitives.StringValues valueData;
                        filterContext.HttpContext.Request.Query.TryGetValue(item, out valueData);
                        if (item == "version" && valueData == "null")
                        {
                            dictionaryData.Add(item, "1");
                        }
                        else
                        {
                            dictionaryData.Add(item, valueData);
                        }

                    }
                }
                else
                {
                    string _getUri = filterContext.HttpContext.Request.Path.Value;
                    string[] strArray = _getUri.Split('/');
                    int count = strArray.Length;
                    string ReqValue = strArray[count - 1];
                }

                if (_token != null)
                {
                    string authToken = _token;
                    if (authToken != string.Empty)
                    {
                        string operationCode = "RC";
                        string contentId = string.Empty;
                        string eksGroup = string.Empty;
                        string pwEmploymentCode = string.Empty;
                        string nationality = string.Empty;
                        string globalUId = string.Empty;
                        bool authorized = true;
                        string status = "published";

                        if (IsValidToken(authToken))
                        {
                            if (headerData != "")
                            {
                                logManager.Info(string.Format("headerData:{0}", headerData));
                                //eksgroup:IsPwEmp:nationality:userEmail:GlobalUId 

                                string[] parameter = headerData.Split(':');
                                eksGroup = parameter[0];
                                pwEmploymentCode = parameter[1];
                                nationality = parameter[2];
                                string headerEmail = parameter[3];
                                globalUId = parameter[4];

                                string dbConnection = string.Empty;

                                int id = 0;
                                string contentType = "";
                                var applicationAuthorization = new ApplicationAuthorization();

                                if (dictionaryData.Count > 0)
                                {
                                    status = GetKey(dictionaryData, "status");
                                    contentType = GetKey(dictionaryData, "contentType");
                                    string IdData = GetKey(dictionaryData, "id");
                                    if (IdData != "")
                                    {
                                        id = Convert.ToInt32(IdData);
                                    }
                                    if (status == "")
                                    {
                                        status = "published";
                                    }
                                    contentId = GetKey(dictionaryData, "contentId");
                                    userName = GetKey(dictionaryData, "currentUserEmail");
                                    operationCode = "RC";

                                    status = string.Equals(status, "Draft", StringComparison.OrdinalIgnoreCase) ? "draft" : "published";

                                    switch (status)
                                    {
                                        case "draft":
                                            authorized = CheckDraftPermission(userName, nationality, pwEmploymentCode, eksGroup, id, contentType);
                                            logManager.Info(string.Format("authorized:{0}", authorized.ToString()));
                                            break;
                                        case "published":
                                            configuration = (IConfiguration)filterContext.HttpContext.RequestServices.GetService(typeof(IConfiguration));
                                            dbConnection = configuration["ConnectionString:PublishedContentDB"];


                                            authorized = applicationAuthorization.AuthorizeAssetAccess(contentId.ToString(), 0, globalUId, nationality, pwEmploymentCode, eksGroup, remoteIP, forwardedFor, methodType, functionName, controllerName, dbConnection).Result;
                                            logManager.Info(string.Format("authorized:{0}", authorized.ToString()));
                                            break;
                                    }
                                }
                                //else if (methodType == "POST")
                                //{
                                //    operationCode = "CC";    
                                //    authorized = CheckCreatePermission(filterContext, pwEmploymentCode, nationality);
                                //}

                                if (authorized == false)
                                {

                                    filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                                    filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                                    filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                                    filterContext.Result = new JsonResult("NotAuthorized")
                                    {
                                        Value = new
                                        {
                                            Status = "Error",
                                            Message = "User Not Authorized!"
                                        },
                                    };
                                }
                                else
                                {
                                    filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                                    filterContext.HttpContext.Response.Headers.Add("AuthStatus", "Authorized");

                                    filterContext.HttpContext.Response.Headers.Add("storeAccessiblity", "Authorized");

                                    return;
                                }
                            }

                            // Adding log here for draft content only. For published content AuthorizationLog getting added in sp AuthorizeAssetAccess
                            if (status == "draft")
                            {
                                authorizationLogService = (IAuthorizationLogService)filterContext.HttpContext.RequestServices.GetService(typeof(IAuthorizationLogService));
                                AddAuthorizationLog(operationCode, globalUId, contentId, 1, authorized, nationality, pwEmploymentCode, eksGroup, null, null, null, null, remoteIP, forwardedFor);
                            }
                        }
                        else
                        {
                            filterContext.HttpContext.Response.Headers.Add("authToken", authToken);
                            filterContext.HttpContext.Response.Headers.Add("AuthStatus", "NotAuthorized");

                            filterContext.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                            filterContext.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                            filterContext.Result = new JsonResult("NotAuthorized")
                            {
                                Value = new
                                {
                                    Status = "Error",
                                    Message = "Invalid Token"
                                },
                            };
                        }

                    }

                }
                else
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
            }
        }


        public bool IsValidToken(string authToken)
        {
            //validate Token here  
            return true;
        }

        public string GetIp(AuthorizationFilterContext filterContext)
        {
            string methodName = filterContext.HttpContext.Request.Method.ToString();
            string remoteIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();

            //ServerVariables["HTTP_X_FORWARDED_FOR"];
            //if (string.IsNullOrEmpty(ip))
            //{
            //    ip = 
            //}
            return methodName;
        }

        private static string GetKey(IReadOnlyDictionary<string, string> dictValues, string keyValue)
        {
            return dictValues.ContainsKey(keyValue) ? dictValues[keyValue] : "";
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

        private bool CheckCreatePermission(AuthorizationFilterContext filterContext, string pwEmploymentCode, string nationality)
        {
            var authorized = true;
            try
            {
                string bodyData = ReadBodyAsString(filterContext.HttpContext.Request);
                if (!string.IsNullOrWhiteSpace(bodyData))
                {
                    JObject requestData = JObject.Parse(bodyData);
                    int exportAuthorityId = Convert.ToInt32(requestData["exportAuthorityId"]);
                    bool outsourceable = Convert.ToBoolean(requestData["outsourceable"]);

                    if (CheckOutsourcable(pwEmploymentCode, outsourceable)
                        && CheckNationality(nationality, exportAuthorityId))
                    {
                        authorized = true;
                    }
                    else
                    {
                        authorized = false;
                    }
                }
            }
            catch (Exception ex)
            {
                logManager.Error(ex.Message, ex, null, ex.StackTrace);
            }
            return authorized;
        }

        private string ReadBodyAsString(Microsoft.AspNetCore.Http.HttpRequest request)
        {
            var initialBody = request.Body;
            try
            {
                using (StreamReader reader = new StreamReader(request.Body, leaveOpen: true))
                {
                    return reader.ReadToEndAsync().Result;
                }
            }
            finally
            {
                request.Body.Position = 0;
                request.Body = initialBody;
            }
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
