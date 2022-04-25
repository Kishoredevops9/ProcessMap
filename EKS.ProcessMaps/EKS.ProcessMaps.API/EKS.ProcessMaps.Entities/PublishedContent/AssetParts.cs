using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AssetParts
    {
        public int Id { get; set; }
        public int KnowledgeAssetId { get; set; }
        public int SeqNum { get; set; }
        public string Text { get; set; }
        public string Link { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public int SequenceNumber { get; set; }
        public int AssetPartTypeId { get; set; }

        public virtual AssetPartTypes AssetPartType { get; set; }
        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
    }
}
