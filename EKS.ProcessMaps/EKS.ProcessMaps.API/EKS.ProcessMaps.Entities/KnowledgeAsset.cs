using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class KnowledgeAsset
    {
        public KnowledgeAsset()
        {
            AssetControllingPrograms = new HashSet<AssetControllingPrograms>();
            AssetExportCompliances = new HashSet<AssetExportCompliances>();
            AssetKeywords = new HashSet<AssetKeywords>();
            AssetParts = new HashSet<AssetParts>();
            AssetPhases = new HashSet<AssetPhases>();
            AssetTags = new HashSet<AssetTags>();
            AssetUsers = new HashSet<AssetUsers>();
        }

        public int KnowledgeAssetId { get; set; }
        public string ContentId { get; set; }
        public int ContentNumber { get; set; }
        public string Title { get; set; }
        public int? AssetHandleId { get; set; }
        public int? SourceAssetHandleId { get; set; }
        public int AssetStatusId { get; set; }
        public bool PrivateInd { get; set; }
        public string AssetTypeCode { get; set; }
        public int? ApprovalRequirementId { get; set; }
        public int DisciplineId { get; set; }
        public string DisciplineCode { get; set; }
        public bool ProgramControlled { get; set; }
        public int? ConfidentialityId { get; set; }
        public bool Outsourcable { get; set; }
        public int? UsjurisdictionId { get; set; }
        public int? UsclassificationId { get; set; }
        public int? ClassificationRequestNumber { get; set; }
        public DateTime? ClassificationDate { get; set; }
        public DateTime? Tpmdate { get; set; }
        public int RevisionTypeId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual AssetTypes AssetTypeCodeNavigation { get; set; }
        public virtual ConfidentialitiesRef Confidentiality { get; set; }
        public virtual RevisionTypes RevisionType { get; set; }
        public virtual ICollection<AssetControllingPrograms> AssetControllingPrograms { get; set; }
        public virtual ICollection<AssetExportCompliances> AssetExportCompliances { get; set; }
        public virtual ICollection<AssetKeywords> AssetKeywords { get; set; }
        public virtual ICollection<AssetParts> AssetParts { get; set; }
        public virtual ICollection<AssetPhases> AssetPhases { get; set; }
        public virtual ICollection<AssetTags> AssetTags { get; set; }
        public virtual ICollection<AssetUsers> AssetUsers { get; set; }
    }
}
