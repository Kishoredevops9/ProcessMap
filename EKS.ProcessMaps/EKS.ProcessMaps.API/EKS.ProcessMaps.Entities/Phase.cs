namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Phase
    /// </summary>
    public class Phase
    {
        /// PhaseId
        public int PhaseId { get; set; }

        /// Phase1
        public string Phase1 { get; set; }

        /// Version
        public int Version { get; set; }

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

        /// ContentPhases
        public virtual ICollection<ContentPhases> ContentPhases { get; set; }

    }
}
