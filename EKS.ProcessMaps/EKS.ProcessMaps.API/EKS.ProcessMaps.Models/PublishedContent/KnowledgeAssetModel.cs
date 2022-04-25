namespace EKS.ProcessMaps.Models.PublishedContent
{
    using EKS.ProcessMaps.Entities.PublishedContent;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class KnowledgeAssetModel
    {
        /// Id
        public int Id { get; set; }

        /// ContentNumber
        public int ContentNumber { get; set; }

        /// Title
        public string Title { get; set; }

        /// PrivateInd
        public bool PrivateInd { get; set; }

        /// AssetTypeCode
        public string AssetTypeCode { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// DisciplineCode
        public string DisciplineCode { get; set; }

        /// ProgramControlledInd
        public bool ProgramControlledInd { get; set; }

        /// ConfidentialityId
        public int? ConfidentialityId { get; set; }

        /// OutsourcableInd
        public bool OutsourcableInd { get; set; }

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

        /// Version
        public int Version { get; set; }

        /// EffectiveFrom
        public DateTime EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

        /// ApprovalRequirementId
        public int ApprovalRequirementId { get; set; }

        /// RevisionTypeId
        public int RevisionTypeId { get; set; }

        /// AssetStatusId
        public int AssetStatusId { get; set; }

        /// ExportPdfurl
        public string ExportPdfurl { get; set; }

        /// Usclassification
        public string Usclassification { get; set; }

        /// SourceFileUrl
        public string SourceFileUrl { get; set; }

        /// PresentationUrl
        public string PresentationUrl { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// SwimLanes
        public virtual List<SwimLanes> SwimLanes { get; set; }

        /// ActivityBlocks
        public virtual List<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
