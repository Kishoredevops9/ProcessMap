namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// RefDiscipline
    /// </summary>
    public class Discipline
    {
        /// DisciplineId
        public int DisciplineId { get; set; }

        /// Discipline1
        public string Discipline1 { get; set; }

        /// Discipline2
        public string Discipline2 { get; set; }

        /// Discipline3
        public string Discipline3 { get; set; }

        /// Discipline4
        public string Discipline4 { get; set; }

        /// DisciplineCode
        public string DisciplineCode { get; set; }

        /// Version
        public int Version { get; set; }

        /// EffectiveFrom
        public DateTime EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

        /// <summary>
        /// ActivityPage
        /// </summary>
        public virtual ICollection<ActivityPage> ActivityPage { get; set; }
      

        /// <summary>
        /// KnowledgePack
        /// </summary>
        public virtual ICollection<ProcessMap> ProcessMap { get; set; }

        /// <summary>
        /// SwimLanes
        /// </summary>
        public virtual ICollection<SwimLanes> SwimLanes { get; set; }

        public virtual ICollection<KnowledgePack> KnowledgePack { get; set; }
    }
}
