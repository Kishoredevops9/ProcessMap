using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class KnowledgePack
    {
        public KnowledgePack()
        {
            KnowledgePackContent = new HashSet<KnowledgePackContent>();
            KnowledgePackLessonLearned = new HashSet<KnowledgePackLessonLearned>();
            KnowledgePackNatureOfChange = new HashSet<KnowledgePackNatureOfChange>();
            KnowledgePackPhysics = new HashSet<KnowledgePackPhysics>();
            KnowledgePackPurpose = new HashSet<KnowledgePackPurpose>();
        }

        public int KnowledgePackId { get; set; }
        public string Title { get; set; }
        public string AssetTypeCode { get; set; }
        public int AssetTypeId { get; set; }
        public int DisciplineId { get; set; }
        public int DisciplineCodeId { get; set; }
        public int AssetStatusId { get; set; }
        public bool ProgramControlled { get; set; }
        public int? ConfidentialityId { get; set; }
        public bool Outsourceable { get; set; }
        public int? UsjurisdictionId { get; set; }
        public int? UsclassificationId { get; set; }
        public int? ClassifierId { get; set; }
        public int? RevisionTypeId { get; set; }
        public string ContentOwnerId { get; set; }
        public int? ClassificationRequestNumber { get; set; }
        public DateTime? ClassificationDate { get; set; }
        public int? SecurityAttributesId { get; set; }
        public DateTime? Tpmdate { get; set; }
        public string LevelCode { get; set; }
        public string ApplicabilityCode { get; set; }
        public int? Version { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public bool Confidentiality { get; set; }
        public string ContentId { get; set; }
        public string Keywords { get; set; }
        public string Author { get; set; }
        public int? SubDisciplineId { get; set; }
        public int? SubSubDisciplineId { get; set; }
        public int? SubSubSubDisciplineId { get; set; }
        public string ProcessInstId { get; set; }
        public string CustomId { get; set; }
        public int? ControllingProgramId { get; set; }
        public int? ExportAuthorityId { get; set; }
        public string DisciplineCode { get; set; }

        public virtual ContentType AssetType { get; set; }
        public virtual Discipline Discipline { get; set; }
        public virtual ICollection<KnowledgePackContent> KnowledgePackContent { get; set; }
        public virtual ICollection<KnowledgePackLessonLearned> KnowledgePackLessonLearned { get; set; }
        public virtual ICollection<KnowledgePackNatureOfChange> KnowledgePackNatureOfChange { get; set; }
        public virtual ICollection<KnowledgePackPhysics> KnowledgePackPhysics { get; set; }
        public virtual ICollection<KnowledgePackPurpose> KnowledgePackPurpose { get; set; }
    }
}
