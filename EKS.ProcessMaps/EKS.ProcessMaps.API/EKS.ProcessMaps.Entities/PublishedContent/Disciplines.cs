using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class Disciplines
    {
        public Disciplines()
        {
            KnowledgeAssets = new HashSet<KnowledgeAssets>();
        }

        public int Id { get; set; }
        public string Discipline1 { get; set; }
        public string Discipline2 { get; set; }
        public string Discipline3 { get; set; }
        public string Discipline4 { get; set; }
        public string DisciplineCode { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<KnowledgeAssets> KnowledgeAssets { get; set; }
        public virtual ICollection<SwimLanes> SwimLanes { get; set; }
    }
}
