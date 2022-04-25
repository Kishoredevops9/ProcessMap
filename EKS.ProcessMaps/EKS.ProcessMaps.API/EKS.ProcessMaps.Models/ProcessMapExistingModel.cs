namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model class of existing process map
    /// </summary>
    public class ProcessMapExistingModel
    {
        /// Id
        public int Id { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Title
        public string Title { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// DisciplineCodeId
        public int? DisciplineCodeId { get; set; }

        /// AssetTypeId
        public int? AssetTypeId { get; set; }

        /// AssetStatusId
        public int? AssetStatusId { get; set; }

        /// ApprovalRequirementId
        public int? ApprovalRequirementId { get; set; }

        /// ClassifierId
        public int? ClassifierId { get; set; }

        /// ConfidentialityId
        public int? ConfidentialityId { get; set; }

        /// RevisionTypeId
        public int? RevisionTypeId { get; set; }

        /// ProgramControlled
        public bool? ProgramControlled { get; set; }

        /// Outsourceable
        public bool? Outsourceable { get; set; }

        /// Version
        public int? Version { get; set; }

        /// EffectiveFrom
        public DateTime? EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime? CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime? LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

        /// UsjurisdictionId
        public int? UsjurisdictionId { get; set; }

        /// UsclassificationId
        public int? UsclassificationId { get; set; }

        /// ClassificationRequestNumber
        public int? ClassificationRequestNumber { get; set; }

        /// ClassificationDate
        public DateTime? ClassificationDate { get; set; }

        /// Tpmdate
        public DateTime? Tpmdate { get; set; }

        /// ExportAuthorityId
        public int? ExportAuthorityId { get; set; }

        /// ControllingProgramId
        public int? ControllingProgramId { get; set; }

        /// ContentOwnerId
        public string ContentOwnerId { get; set; }

        /// Keywords
        public string Keywords { get; set; }

        /// Author
        public string Author { get; set; }

        /// Confidentiality
        public bool? Confidentiality { get; set; }

        /// Purpose
        public string Purpose { get; set; }

        /// ProcessInstId
        public string ProcessInstId { get; set; }

        /// CustomId
        public string CustomId { get; set; }

        /// DisciplineCode
        public string DisciplineCode { get; set; }

        /// PrivateInd
        public bool PrivateInd { get; set; }

        /// SourceFileUrl
        public string SourceFileUrl { get; set; }

        /// ExportPdfurl
        public string ExportPdfurl { get; set; }

        /// SwimLanes
        public List<SwimLanesInputOutputModel> SwimLanes { get; set; }

        ///// ProcessMapMeta
        //public List<ProcessMapMetaModel> ProcessMapMeta { get; set; }

        ///// ActivityDocuments
        //public List<ActivityDocumentsModel> ActivityDocuments { get; set; }

        /// Phases
        public List<PhasesModel> Phases { get; set; }

        /// ContentPhases
        public List<ContentPhasesModel> ContentPhases { get; set; }

        /// ContentExportCompliances
        public List<ContentExportCompliancesModel> ContentExportCompliances { get; set; }

        /// ContentTags
        public List<ContentTagsModel> ContentTags { get; set; }
    }
}
