namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// AuthorizationLog
    /// </summary>
    public class AuthorizationLog
    {
        /// AuthorizationLogId
        public int AuthorizationLogId { get; set; }

        /// User
        public string User { get; set; }

        /// OperationCode
        public string OperationCode { get; set; }

        /// KnowledgeAssetId
        public int? KnowledgeAssetId { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Version
        public int? Version { get; set; }

        /// RequestDateTime
        public DateTime RequestDateTime { get; set; }

        /// Outcome
        public bool Outcome { get; set; }

        /// Nationality
        public string Nationality { get; set; }

        /// PWEmploymentCode
        public string PWEmploymentCode { get; set; }

        /// Groups
        public string Groups { get; set; }

        /// USJurisdictionId
        public int? USJurisdictionId { get; set; }

        /// USClassificationId
        public int? USClassificationId { get; set; }

        /// OutsourcableInd
        public bool? OutsourcableInd { get; set; }

        /// ConfidentialityId
        public int? ConfidentialityId { get; set; }

        /// ControllingPrograms
        public string ControllingPrograms { get; set; }

        /// RemoteAddress
        public string RemoteAddress { get; set; }

        /// ForwardedFor
        public string ForwardedFor { get; set; }
    }
}
