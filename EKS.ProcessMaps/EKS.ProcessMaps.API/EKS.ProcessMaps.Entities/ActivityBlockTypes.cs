namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Entities of activity block types.
    /// </summary>
    public class ActivityBlockTypes
    { /// Id
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

        /// ActivityBlocks
        public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
