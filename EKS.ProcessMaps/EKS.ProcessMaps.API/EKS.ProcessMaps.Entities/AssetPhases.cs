using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class AssetPhases
    {
        public int AssetPhaseId { get; set; }
        public int KnowledgeAssetId { get; set; }
        public string AssetPhaseCode { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual KnowledgeAsset KnowledgeAsset { get; set; }
    }
}
