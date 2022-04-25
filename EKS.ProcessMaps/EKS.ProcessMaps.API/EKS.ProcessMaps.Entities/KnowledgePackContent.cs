using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class KnowledgePackContent
    {
        public int KnowledgePackContentId { get; set; }
        public int KnowledgePackId { get; set; }
        public int? ContentId { get; set; }
        public int? LayoutId { get; set; }
        public string LayoutType { get; set; }
        public string TabCode { get; set; }
        public int? Version { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string ContentFirst { get; set; }
        public string ContentSecond { get; set; }
        public string ContentThird { get; set; }
        public int? OrderNumber { get; set; }

        public virtual KnowledgePack KnowledgePack { get; set; }
    }
}
