namespace EKS.ProcessMaps.Entities
{
    using System;

    /// <summary>
    /// ContentPhases
    /// </summary>
    public class ContentPhases
    {
        /// ContentPhaseId
        public int ContentPhaseId { get; set; }

        /// ContentId
        public int ContentId { get; set; }

        /// TypeId
        public int? TypeId { get; set; }

        /// PhaseId
        public int PhaseId { get; set; }

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

        /// Phase
        public virtual Phase Phase { get; set; }
    }
}
