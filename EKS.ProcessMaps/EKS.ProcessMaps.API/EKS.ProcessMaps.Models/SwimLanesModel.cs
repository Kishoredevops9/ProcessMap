namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// Model class of swimlines
    /// </summary>
    public class SwimLanesModel
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

        /// DisciplineText
        public string DisciplineText { get; set; }

        /// ProtectedInd
        public bool ProtectedInd { get; set; }

        /// RequiredInd
        public bool RequiredInd { get; set; }

        /// Size
        public string Size { get; set; }
    }
}
