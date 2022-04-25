namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AssetStatus
    {
        // AssetStatusId
        public int AssetStatusId { get; set; }

        // Name
        public string Name { get; set; }

        // Description
        public string Description { get; set; }

        // Version
        public int? Version { get; set; }

        // EffectiveFrom
        public DateTime? EffectiveFrom { get; set; }

        // EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        // CreatedDateTime
        public DateTime? CreatedDateTime { get; set; }

        // CreatedUser
        public string CreatedUser { get; set; }

        // LastUpdateDateTime
        public DateTime? LastUpdateDateTime { get; set; }

        // LastUpdateUser
        public string LastUpdateUser { get; set; }

        /*
          // KnowledgePacks
        public virtual ICollection<KnowledgePack> KnowledgePacks { get; set; }

        // RelatedContents
        public virtual ICollection<RelatedContent> RelatedContents { get; set; }

        // CriteriaGroup
        public virtual ICollection<CriteriaGroup> CriteriaGroup { get; set; }

        // ActivityPage
        public virtual ICollection<ActivityPage> ActivityPage { get; set; }

        // TableOfContent
        public virtual ICollection<TableOfContent> TableOfContent { get; set; }

        // WorkflowAudits
        public virtual ICollection<WorkflowAudit> WorkflowAudits { get; set; }
         */

    }
}
