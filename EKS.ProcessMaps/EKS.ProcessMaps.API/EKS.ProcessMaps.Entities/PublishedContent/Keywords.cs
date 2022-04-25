using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class Keywords
    {
        public Keywords()
        {
            AssetKeywords = new HashSet<AssetKeywords>();
        }

        public int Id { get; set; }
        public string Keyword { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<AssetKeywords> AssetKeywords { get; set; }
    }
}
