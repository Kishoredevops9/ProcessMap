using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ActivityStates
    {
        public ActivityStates()
        {
            ExcludedContainerItems = new HashSet<ExcludedContainerItems>();
        }

        public int Id { get; set; }
        public int ShapeId { get; set; }
        public string Performer { get; set; }
        public string Reviewer { get; set; }
        public int ExecutionStateId { get; set; }
        public int AcceptedAssetVersion { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ExecutionStates ExecutionState { get; set; }
        public virtual ActivityBlocks Shape { get; set; }
        public virtual ICollection<ExcludedContainerItems> ExcludedContainerItems { get; set; }
    }
}
