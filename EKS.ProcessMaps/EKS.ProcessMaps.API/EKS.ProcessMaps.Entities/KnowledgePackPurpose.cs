using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class KnowledgePackPurpose
    {
        public int KnowledgePackPurposeId { get; set; }
        public int KnowledgePackId { get; set; }
        public string Layout { get; set; }
        public int FrameNumber { get; set; }
        public string FrameType { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string Links { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual KnowledgePack KnowledgePack { get; set; }
    }
}
