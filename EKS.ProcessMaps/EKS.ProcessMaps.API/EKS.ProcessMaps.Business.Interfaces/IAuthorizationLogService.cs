namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Threading.Tasks;

    /// <summary>
    /// IAuthorizationLogService
    /// </summary>
    public interface IAuthorizationLogService
    {
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
        Task AddAuthorizationLogAsync(string operationCode, string user, string contentId, int version, bool outcome, string nationality,
            string pwEmploymentCode, string groups, int? usJurisdictionId, int? usClassificationId, bool? outsourcableInd, int? confidentialityId,
            string remoteAddress, string forwardedFor);
    }
}
