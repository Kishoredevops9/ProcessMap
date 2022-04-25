namespace EKS.ProcessMaps.Models
{
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;

    public class AuthContextModel
    {
        public string MethodType { get; set; }
        public string RemoteIP { get; set; }
        public string UserName { get; set; }
        public string FunctionName { get; set; }
        public string ControllerName { get; set; }
        public string ForwardedFor { get; set; }
        public string Token { get; set; }
        public string HeaderData { get; set; }
        public AuthContextModel(AuthorizationFilterContext filterContext)
        {
            MethodType = filterContext.HttpContext.Request.Method.ToString();
            RemoteIP = filterContext.HttpContext.Connection.RemoteIpAddress.ToString();
            UserName = filterContext.HttpContext.User.ToString();
            FunctionName = filterContext.HttpContext.Request.RouteValues["action"].ToString();
            ControllerName = filterContext.HttpContext.Request.RouteValues["controller"].ToString();
            ForwardedFor = filterContext.HttpContext.Request.Headers["HTTP_X_FORWARDED_FOR"].ToString();
            HeaderData = filterContext.HttpContext.Request.Headers["userinfo"].ToString();
            Token = "temptoken";
        }
    }
}
