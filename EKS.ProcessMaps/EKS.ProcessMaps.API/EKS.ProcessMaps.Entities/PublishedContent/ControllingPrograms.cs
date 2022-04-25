using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ControllingPrograms
    {
        public ControllingPrograms()
        {
            AssetControllingPrograms = new HashSet<AssetControllingPrograms>();
            //TaskControllingPrograms = new HashSet<TaskControllingPrograms>();
        }

        public int Id { get; set; }
        public string ProgramName { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string GroupName { get; set; }

        public virtual ICollection<AssetControllingPrograms> AssetControllingPrograms { get; set; }
        //public virtual ICollection<TaskControllingPrograms> TaskControllingPrograms { get; set; }
    }
}
