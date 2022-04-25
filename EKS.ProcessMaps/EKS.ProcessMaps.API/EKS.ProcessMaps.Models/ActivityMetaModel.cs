namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// Model class of activity meta
    /// </summary>
    public class ActivityMetaModel
    {
        /// Id
        public int Id { get; set; }

        /// ActivityBlockId
        public int? ActivityBlockId { get; set; }

        /// Key
        public string Key { get; set; }

        /// Value
        public string Value { get; set; }

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
    }
}
