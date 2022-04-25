namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entity of activity blocks.
    /// </summary>
    public class ActivityBlocks
    {
        /// Id
        public int Id { get; set; }

        /// SwimLaneId
        public int? SwimLaneId { get; set; }

        /// ActivityTypeId
        public int? ActivityTypeId { get; set; }

        /// Name
        public string Name { get; set; }

        /// SequenceNumber
        public int? SequenceNumber { get; set; }

        /// Color
        public string Color { get; set; }

        /// BorderColor
        public string BorderColor { get; set; }

        /// BorderStyle
        public string BorderStyle { get; set; }

        /// BorderWidth
        public int? BorderWidth { get; set; }

        /// Version
        public int? Version { get; set; }

        /// EffectiveFrom
        public DateTime? EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime? CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime? LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

        /// LocationX
        public int LocationX { get; set; }

        /// LocationY
        public int LocationY { get; set; }

        /// AssetContentId
        public string AssetContentId { get; set; }

        /// ProtectedInd
        public bool ProtectedInd { get; set; }

        /// RequiredInd
        public bool RequiredInd { get; set; }

        /// ProcessMapId
        public int? ProcessMapId { get; set; }

        /// PhaseId
        public int? PhaseId { get; set; }

        /// Length
        public int? Length { get; set; }

        /// Width
        public int? Width { get; set; }

        /// ActivityType
        public virtual ActivityBlockTypes ActivityType { get; set; }

        /// ProcessMap
        public virtual ProcessMap ProcessMap { get; set; }

        /// SwimLane
        public virtual SwimLanes SwimLane { get; set; }

        /// Phase
        public virtual Phases Phase { get; set; }

        /// ActivityConnections
        public virtual ICollection<ActivityConnections> ActivityConnections { get; set; }
    }
}
