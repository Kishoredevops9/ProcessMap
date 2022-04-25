using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class ConfidentialitiesRef
    {
        public ConfidentialitiesRef()
        {
            KnowledgeAssets = new HashSet<KnowledgeAsset>();
        }

        public int ConfidentialityId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string GroupName { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<KnowledgeAsset> KnowledgeAssets { get; set; }
    }
}
