using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public partial class RevisionTypes
    {
        public RevisionTypes()
        {
            KnowledgeAssets = new HashSet<KnowledgeAsset>();
        }

        public int RevisionTypeId { get; set; }
        public string RevisionTypeCode { get; set; }
        public string Description { get; set; }
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
