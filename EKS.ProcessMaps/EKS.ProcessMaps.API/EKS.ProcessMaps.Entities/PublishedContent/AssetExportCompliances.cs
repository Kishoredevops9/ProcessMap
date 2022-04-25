using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AssetExportCompliances
    {
        public int Id { get; set; }
        public int KnowledgeAssetId { get; set; }
        public int ExportComplianceId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ExportCompliances ExportCompliance { get; set; }
        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
    }
}
