using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class AssetParts
    {
        public int AssetPartId { get; set; }
        public int KnowledgeAssetId { get; set; }
        public string AssetPartTypeCode { get; set; }
        public int SequenceNumber { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
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
