namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entity of swimlanes.
    /// </summary>
    public class SwimLanes
    {
        /// Id
        public int Id { get; set; }

        /// ProcessMapId
        public int? ProcessMapId { get; set; }

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

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// ProtectedInd
        public bool ProtectedInd { get; set; }

        /// RequiredInd
        public bool RequiredInd { get; set; }

        /// Size
        public string Size { get; set; }

        /// Discipline
        public virtual Discipline Discipline { get; set; }

        /// ProcessMap
        public virtual ProcessMap ProcessMap { get; set; }

        /// ActivityBlocks
        public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
