namespace EKS.ProcessMaps.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using global::EKS.Common.ExceptionHandler;
    using global::EKS.Common.Logging;
    using global::EKS.Common.Notification;
    using global::EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;

    [Route("api/emailnotification")]
    [ApiController]
    public class EmailNotificationController : Controller
    {
        private readonly IEmailNotification _emailnotification;
        private readonly ILogManager _logManager;
        public IConfiguration _config;

        /// Phases Controller constructor.
        /// </summary>
        //// <param name="phasesAppservice">IPhases</param>
        /// <param name="logManager">ILogManager</param>
        public EmailNotificationController(IEmailNotification emailNotification, ILogManager logManager, IConfiguration config)
        {
            this._emailnotification = emailNotification;
            this._logManager = logManager;
            this._config = config;
        }

        [HttpPost("SendEmail")]
        public IActionResult SendEmail([FromBody] EmailNotificationModel mailModel)
        {
            Dictionary<string, string> errorProperties =
                this._logManager.GetTrackingProperties(this.GetType().Assembly.GetName().Name, this.GetType().Namespace, System.Reflection.MethodBase.GetCurrentMethod().Name, "Phases", this.GetType().Name);
            try
            {
                if (string.IsNullOrWhiteSpace(mailModel.Username))
                {
                    return this.BadRequest("Please enter Username.");
                }

                // The mail
                var smtpServer = this._config.GetSection("SMTPServer");
                mailModel.Host = smtpServer.GetValue<string>("Host");
                mailModel.Port = smtpServer.GetValue<string>("Port");
                mailModel.Username = smtpServer.GetValue<string>("UserName");
                mailModel.EnableSsl = smtpServer.GetValue<string>("EnableSsl");
                mailModel.Password = smtpServer.GetValue<string>("Password");
                mailModel.TargetName = smtpServer.GetValue<string>("TargetName");

                var sent = this._emailnotification.SendEmailNotification(mailModel.Body, mailModel.Sender, mailModel.Recipients, mailModel.Subject, mailModel.CC, mailModel.Host, mailModel.Port, mailModel.Username, mailModel.Password, mailModel.EnableSsl, mailModel.TargetName);

                return this.Ok(sent);
            }
            catch (Exception ex)
            {
                this._logManager.Error(ex.Message, ex, errorProperties, ex.StackTrace);
                throw new TechnicalException(111, "Please contact administrator.", ex.Message, ex.StackTrace);
            }
        }
    }
}
