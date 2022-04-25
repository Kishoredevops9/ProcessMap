using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class Kpacks
    {
        public int KnowledgeAssetId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public int LevelId { get; set; }
        public int ApplicabilityId { get; set; }

        //public virtual Applicabilities Applicability { get; set; }
        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
        //public virtual Levels Level { get; set; }
    }
}
