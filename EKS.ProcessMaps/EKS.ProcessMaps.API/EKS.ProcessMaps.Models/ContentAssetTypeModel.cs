using System;
using System.Collections.Generic;
namespace EKS.ProcessMaps.Models
{
    /// <summary>
    /// RelatedContent
    /// </summary>
    public class ContentAssetTypeModel
    {
        /// Id
        public int Id { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Title
        public string Title { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// DisciplineCode
        public string DisciplineCode { get; set; }

        /// SubDisciplineId
        public int? SubDisciplineId { get; set; }

        /// SubSubDisciplineId
        public int? SubSubDisciplineId { get; set; }

        /// SubSubSubDisciplineId
        public int? SubSubSubDisciplineId { get; set; }

        /// DisciplineCodeId
        public int? DisciplineCodeId { get; set; }

        /// AssetTypeId
        public int? AssetTypeId { get; set; }

        /// AssetTypeCode
        public string AssetTypeCode { get; set; }

        /// AssetStatusId
        public int? AssetStatusId { get; set; }

        /// AssetStatus
        public string AssetStatus { get; set; }

        /// ContentOwnerId
        public string ContentOwnerId { get; set; }

        /// ClassifierId
        public int? ClassifierId { get; set; }

        /// ConfidentialityId
        public int? ConfidentialityId { get; set; }

        /// Purpose
        public string Purpose { get; set; }

        /// IntentOfCriteria
        public string IntentOfCriteria { get; set; }

        /// BasisOfCriteria
        public string BasisOfCriteria { get; set; }

        /// ValidationOfCriteria
        public string ValidationOfCriteria { get; set; }

        /// RequiredReferences
        public string RequiredReferences { get; set; }

        /// InformationalReferences
        public string InformationalReferences { get; set; }

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

        /// ClassifierName
        public string ClassifierName { get; set; }

        /// ContentStatus
        public string ContentStatus { get; set; }

        /// ClassificationDate
        public DateTime? ClassificationDate { get; set; }

        /// Tpmdate
        public DateTime? Tpmdate { get; set; }

        /// Keywords
        public string Keywords { get; set; }

        /// Author
        public string Author { get; set; }

        /// Confidentiality
        public bool? Confidentiality { get; set; }

        /// ProcessInstId
        public string ProcessInstId { get; set; }

        /// CustomId
        public string CustomId { get; set; }

        /// ContentPhase   
        public List<int> ContentPhase { get; set; }

        /// ContentExportCompliance
        public List<int> ContentExportCompliance { get; set; }

        /// ContentTag   
        public List<int> ContentTag { get; set; }

        /// File Bytes
        public byte[] FileBytes { get; set; }

        /// RelatedContentInformation
        //public virtual ICollection<RelatedContentInformation> RelatedContentInformation { get; set; }

        /// ControllingProgramId
        public int? ControllingProgramId { get; set; }

        /// ExportAuthorityId
        public int? ExportAuthorityId { get; set; }

        /// IsContentIdRequired
        public bool IsContentIdRequired { get; set; }

        /// IsNOCRequired
        public bool IsNOCRequired { get; set; }

        /// IsNOCRequired
        public DateTime? NOCCreatedDate { get; set; }

        /// DocumentName
        public string DocumentName { get; set; }

        /// CoAuthor
        public string CoAuthor { get; set; }

        /// ApprovalRequirementsId
        public string ApprovalRequirementsId { get; set; }

        /// AssetHandle
        public string AssetHandle { get; set; }

        /// BAER
        public string BAER { get; set; }

        /// Class
        public string Class { get; set; }

        /// ClassifiactionDate
        public string ClassifiactionDate { get; set; }

        /// DocumentType
        public string DocumentType { get; set; }

        /// EngineeringOrg
        public string EngineeringOrg { get; set; }

        /// CreatorClockId
        public string CreatorClockId { get; set; }

        /// CriteriaGroupDefinition
        //public ICollection<CriteriaGroupDefinition> Definitions { get; set; }

        /// CriteriaGroupCriteria
        //public ICollection<CriteriaGroupCriteria> Criteria { get; set; }

        /// CGNatureOfChange
        //public ICollection<NatureOfChange> CGNatureOfChange { get; set; }

        /// Discipline
        public string Discipline { get; set; }

        /// Keyword
        public string Keyword { get; set; }

        /// Phase
        public string Phase { get; set; }

        /// ContentTags
        public string[] ContentTags { get; set; }

        /// Tags
        public string Tags { get; set; }

        /// ExportComplienceTouchPointData
        public string ExportComplienceTouchPointData { get; set; }

        /// AuthorId
        public string AuthorId { get; set; }

        /// EngineFamily
        public string EngineFamily { get; set; }

        /// ESWReferenceID
        public string ESWReferenceID { get; set; }

        /// JC
        public string JC { get; set; }

        /// PWCreatedBy
        public string PWCreatedBy { get; set; }

        /// SourceDocUrl
        public string SourceDocUrl { get; set; }

        /// PWStatus
        public string PWStatus { get; set; }

        /// SourceDocID
        public int? SourceDocID { get; set; }

        /// ContentTypeId
        public string ContentTypeId { get; set; }

        /// CreatedOn
        public string CreatedOn { get; set; }

        /// ECTouchPointComments
        public string ECTouchPointComments { get; set; }

        /// EffectiveFromDate
        public string EffectiveFromDate { get; set; }

        /// EffectiveToDate
        public string EffectiveToDate { get; set; }

        /// ExportAuthority
        public string ExportAuthority { get; set; }

        /// FirstPublicationDate
        public string FirstPublicationDate { get; set; }

        /// Scope
        public string Scope { get; set; }

        /// SetofAuthors
        public string SetofAuthors { get; set; }

        /// SetofClassifiers
        public string SetofClassifiers { get; set; }

        /// SetofSecurityAttributes
        public string SetofSecurityAttributes { get; set; }

        /// SourceAssetHandle
        public string SourceAssetHandle { get; set; }

        /// ToDo: Add Status
        public string Status { get; set; }

        /// ToDo: Add Type
        public string Type { get; set; }

        /// USClassification
        public string USClassification { get; set; }

        /// UserEmail
        public string UserEmail { get; set; }

        /// USJuridiction
        public string USJuridiction { get; set; }

        /// VersionNumber
        public string VersionNumber { get; set; }

        /// ContentReviewer
        public string ContentReviewer { get; set; }

        /// ContentReviewer
        public string NOC { get; set; }

        /// ContentOwnerMail
        public string ContentOwnerMail { get; set; }

        /// ConfidentialityName
        public string ConfidentialityName { get; set; }

        /// LesssonLearned
        //public List<LessonsLearned> LessonLearned { get; set; }

        /// NatureOfChange
        //public List<NatureOfChange> NatureOfChange { get; set; }

        /// Component
        //public List<ContainerItemModel> Component { get; set; }

        /// WIGuidence
        public string WIGuidence { get; set; }

        /// ExternalLinks
        public string ExternalLinks { get; set; }

        /// CriteriaGuidence
        public string CriteriaGuidence { get; set; }

        /// PrivateInd
        public bool PrivateInd { get; set; }

        public SwimLanesModel SwimLane { get; set; }

        /// ActivityBlocks
        //public List<ActivityBlocksModel> ActivityBlocks { get; set; }

        public List<ProcessMapModel> Steps { get; set; }

        /// ActivityBlocks
        //public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
