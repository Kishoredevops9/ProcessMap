namespace EKS.ProcessMaps.Business
{
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// AuthorizationLogService
    /// </summary>
    public class AuthorizationLogService : IAuthorizationLogService
    {
        private readonly IRepository<AuthorizationLog> authorizationLogRepo;

        /// <summary>
        /// AuthorizationLogService
        /// </summary>
        /// <param name="authorizationLogRepo"></param>
        public AuthorizationLogService(IRepository<AuthorizationLog> authorizationLogRepo)
        {
            this.authorizationLogRepo = authorizationLogRepo;
        }


        /// <summary>
        /// AddAuthorizationLogAsync
        /// </summary>
        /// <param name="operationCode"></param>
        /// <param name="user"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <param name="outcome"></param>
        /// <param name="nationality"></param>
        /// <param name="pwEmploymentCode"></param>
        /// <param name="groups"></param>
        /// <param name="usJurisdictionId"></param>
        /// <param name="usClassificationId"></param>
        /// <param name="outsourcableInd"></param>
        /// <param name="confidentialityId"></param>
        /// <param name="remoteAddress"></param>
        /// <param name="forwardedFor"></param>
        /// <returns></returns>
        public async Task AddAuthorizationLogAsync(string operationCode, string user, string contentId, int version, bool outcome, string nationality,
            string pwEmploymentCode, string groups, int? usJurisdictionId, int? usClassificationId, bool? outsourcableInd, int? confidentialityId,
            string remoteAddress, string forwardedFor)
        {
            var authorizationLog = new AuthorizationLog
            {
                OperationCode = operationCode,
                User = user,
                ContentId = contentId,
                Version = version,
                Outcome = outcome,
                Nationality = nationality,
                PWEmploymentCode = pwEmploymentCode,
                Groups = groups,
                USJurisdictionId = usJurisdictionId,
                USClassificationId = usClassificationId,
                OutsourcableInd = outsourcableInd,
                ConfidentialityId = confidentialityId,
                RemoteAddress = remoteAddress,
                ForwardedFor = forwardedFor,
                RequestDateTime = DateTime.Now
            };

            await this.authorizationLogRepo.AddAsyn(authorizationLog).ConfigureAwait(false);
        }
    }
}

