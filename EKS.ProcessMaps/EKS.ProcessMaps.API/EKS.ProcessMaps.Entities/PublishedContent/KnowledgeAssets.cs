namespace EKS.ProcessMaps.Entities.PublishedContent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Enitity class for knowledge assets.
    /// </summary>
    public class KnowledgeAssets
    {
        public KnowledgeAssets()
        {
            ActivityBlocks = new HashSet<ActivityBlocks>();
            AssetControllingPrograms = new HashSet<AssetControllingPrograms>();
            AssetExportCompliances = new HashSet<AssetExportCompliances>();
            AssetKeywords = new HashSet<AssetKeywords>();
            AssetParts = new HashSet<AssetParts>();
            AssetPhases = new HashSet<AssetPhases>();
            AssetReferences = new HashSet<AssetReferences>();
            AssetStatements = new HashSet<AssetStatements>();
            AssetTags = new HashSet<AssetTags>();
            AssetUsers = new HashSet<AssetUsers>();
            ContainerItems = new HashSet<ContainerItems>();
            //ContainerItemsRelations = new HashSet<ContainerItemsRelations>();
            PhasesMap = new HashSet<PhasesMap>();
            PrivateAssetsParentAsset = new HashSet<PrivateAssets>();
            RelatedContentCategories = new HashSet<RelatedContentCategories>();
            SwimLanes = new HashSet<SwimLanes>();
            //TaskComponentAssetInstances = new HashSet<TaskComponentAssetInstances>();
            //TaskMaps = new HashSet<TaskMaps>();
        }

        public int Id { get; set; }
        public int ContentNumber { get; set; }
        public string Title { get; set; }
        public bool PrivateInd { get; set; }
        public string AssetTypeCode { get; set; }
        public int? DisciplineId { get; set; }
        public string DisciplineCode { get; set; }
        public bool ProgramControlledInd { get; set; }
        public int? ConfidentialityId { get; set; }
        public bool OutsourcableInd { get; set; }
        public int? UsjurisdictionId { get; set; }
        public int? UsclassificationId { get; set; }
        public int? ClassificationRequestNumber { get; set; }
        public DateTime? ClassificationDate { get; set; }
        public DateTime? Tpmdate { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public int ApprovalRequirementId { get; set; }
        public int RevisionTypeId { get; set; }
        public int AssetStatusId { get; set; }
        public string ExportPdfurl { get; set; }
        public string Usclassification { get; set; }
        public string SourceFileUrl { get; set; }
        public string PresentationUrl { get; set; }
        public string ContentId { get; set; }

        //public virtual ApprovalRequirements ApprovalRequirement { get; set; }
        public virtual AssetStatuses AssetStatus { get; set; }
        public virtual AssetTypes AssetTypeCodeNavigation { get; set; }
        public virtual Confidentialities Confidentiality { get; set; }
        public virtual Disciplines Discipline { get; set; }
        public virtual RevisionTypes RevisionType { get; set; }
        public virtual Usclassifications UsclassificationNavigation { get; set; }
        public virtual Usjurisdictions Usjurisdiction { get; set; }
        public virtual Kpacks Kpacks { get; set; }
        public virtual PrivateAssets PrivateAssetsKnowledgeAsset { get; set; }
        public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
        public virtual ICollection<AssetControllingPrograms> AssetControllingPrograms { get; set; }
        public virtual ICollection<AssetExportCompliances> AssetExportCompliances { get; set; }
        public virtual ICollection<AssetKeywords> AssetKeywords { get; set; }
        public virtual ICollection<AssetParts> AssetParts { get; set; }
        public virtual ICollection<AssetPhases> AssetPhases { get; set; }
        public virtual ICollection<AssetReferences> AssetReferences { get; set; }
        public virtual ICollection<AssetStatements> AssetStatements { get; set; }
        public virtual ICollection<AssetTags> AssetTags { get; set; }
        public virtual ICollection<AssetUsers> AssetUsers { get; set; }
        public virtual ICollection<ContainerItems> ContainerItems { get; set; }
        //public virtual ICollection<ContainerItemsRelations> ContainerItemsRelations { get; set; }
        public virtual ICollection<PhasesMap> PhasesMap { get; set; }
        public virtual ICollection<PrivateAssets> PrivateAssetsParentAsset { get; set; }
        public virtual ICollection<RelatedContentCategories> RelatedContentCategories { get; set; }
        public virtual ICollection<SwimLanes> SwimLanes { get; set; }
        //public virtual ICollection<TaskComponentAssetInstances> TaskComponentAssetInstances { get; set; }
        //public virtual ICollection<TaskMaps> TaskMaps { get; set; }
    }
}
