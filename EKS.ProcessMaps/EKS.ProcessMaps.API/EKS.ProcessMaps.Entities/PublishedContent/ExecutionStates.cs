using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ExecutionStates
    {
        public ExecutionStates()
        {
            ActivityStates = new HashSet<ActivityStates>();
        }

        public int Id { get; set; }
        public string ExecutionStateCode { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<ActivityStates> ActivityStates { get; set; }
    }
}
