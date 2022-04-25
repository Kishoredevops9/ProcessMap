using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ExcludedContainerItems
    {
        public int Id { get; set; }
        public int ActivityStateId { get; set; }
        public int ContainerItemId { get; set; }
        public bool Excluded { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ActivityStates ActivityState { get; set; }
        public virtual ContainerItems ContainerItem { get; set; }
    }
}
