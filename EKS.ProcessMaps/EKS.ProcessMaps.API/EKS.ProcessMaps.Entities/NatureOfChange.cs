namespace EKS.ProcessMaps.Entities
{
    using System;

    /// <summary>
    /// NatureOfChange
    /// </summary>
    public class NatureOfChange
    {
        /// NatureOfChangeId
        public int NatureOfChangeId { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// ContentItemId
        public int? ContentItemId { get; set; }

        /// AssetTypeId
        public int? AssetTypeId { get; set; }

        /// NocdateTime
        public DateTime? NocdateTime { get; set; }

        /// Definition
        public string Definition { get; set; }

        /// Version
        public int? Version { get; set; }

        /// EffectiveFrom
        public DateTime EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

    }
}
