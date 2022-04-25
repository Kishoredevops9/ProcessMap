using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ExportCompliances
    {
        public ExportCompliances()
        {
            AssetExportCompliances = new HashSet<AssetExportCompliances>();
        }

        public int Id { get; set; }
        public string DocumentContentId { get; set; }
        public string ProcessMapTitle { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<AssetExportCompliances> AssetExportCompliances { get; set; }
    }
}
