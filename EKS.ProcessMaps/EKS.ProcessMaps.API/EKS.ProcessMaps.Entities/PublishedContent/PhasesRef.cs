using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class PhasesRef
    {
        public PhasesRef()
        {
            AssetPhases = new HashSet<AssetPhases>();
            StatementPhases = new HashSet<StatementPhases>();
            //TaskPhases = new HashSet<TaskPhases>();
        }

        public int Id { get; set; }
        public string PhaseCode { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<AssetPhases> AssetPhases { get; set; }
        public virtual ICollection<StatementPhases> StatementPhases { get; set; }
        //public virtual ICollection<TaskPhases> TaskPhases { get; set; }
    }
}
