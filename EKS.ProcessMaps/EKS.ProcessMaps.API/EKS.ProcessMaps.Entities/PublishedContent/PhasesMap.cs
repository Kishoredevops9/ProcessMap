using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class PhasesMap
    {
        public PhasesMap()
        {
            ActivityBlocks = new HashSet<ActivityBlocks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int SequenceNumber { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public int KnowledgeAssetId { get; set; }
        public string Location { get; set; }
        public bool? ProtectedInd { get; set; }
        public bool? ExcludedInd { get; set; }
        public string Size { get; set; }

        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
        public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
