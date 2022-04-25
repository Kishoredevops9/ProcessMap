using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AssetReferences
    {
        public int Id { get; set; }
        public int ReferencingKnowledgeAssetId { get; set; }
        public string ReferencedContentId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual KnowledgeAssets ReferencingKnowledgeAsset { get; set; }
    }
}
