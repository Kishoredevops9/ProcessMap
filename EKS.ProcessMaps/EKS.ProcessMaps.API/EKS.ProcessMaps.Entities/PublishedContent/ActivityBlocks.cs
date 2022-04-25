namespace EKS.ProcessMaps.Entities.PublishedContent
{
    using System;
    using System.Collections.Generic;

    public class ActivityBlocks
    {
        /*
        public ActivityBlocks()
        {
            ActivityStates = new HashSet<ActivityStates>();
            ConnectorsChildActivityBlock = new HashSet<Connectors>();
            ConnectorsParentActivityBlock = new HashSet<Connectors>();
            //TaskComponentsActivityBlock = new HashSet<TaskComponents>();
            //TaskComponentsStepActivityBlock = new HashSet<TaskComponents>();
        }*/

        public int Id { get; set; }
        public string Caption { get; set; }
        public int ActivityBlockTypeId { get; set; }
        public int SwimLaneId { get; set; }
        public int? PhaseId { get; set; }
        public int? KnowledgeAssetId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int? BorderWidth { get; set; }
        public int? SequenceNumber { get; set; }
        public string AssetContentId { get; set; }
        public bool? ProtectedInd { get; set; }
        public bool? RequiredInd { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? FormatId { get; set; }

        public virtual ActivityBlockTypes ActivityBlockType { get; set; }
        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
        public virtual PhasesMap Phase { get; set; }
        public virtual SwimLanes SwimLane { get; set; }
        public virtual ICollection<ActivityStates> ActivityStates { get; set; }
        public virtual ICollection<Connectors> ConnectorsChildActivityBlock { get; set; }
        public virtual ICollection<Connectors> ConnectorsParentActivityBlock { get; set; }
        //public virtual ICollection<TaskComponents> TaskComponentsActivityBlock { get; set; }
        //public virtual ICollection<TaskComponents> TaskComponentsStepActivityBlock { get; set; }
    }
}
