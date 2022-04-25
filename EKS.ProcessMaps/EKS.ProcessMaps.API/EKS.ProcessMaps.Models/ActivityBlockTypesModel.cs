namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// Model class of activity block types
    /// </summary>
    public class ActivityBlockTypesModel
    {
        /// Id
        public int Id { get; set; }

        /// Name
        public string Name { get; set; }

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
