namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ControllingProgram
    /// </summary>
    public class ControllingProgram
    {
        /// ControllingProgramId
        //public int ControllingProgramId { get; set; }
        public int Id { get; set; }

        /// Name
        public string Name { get; set; }

        /// Description
        public string Description { get; set; }

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

        /// GroupName
        public string GroupName { get; set; }

        /// ActivityPage
       // public virtual ICollection<ProcessMap> ProcessMaps { get; set; }
        public virtual ICollection<AssetControllingPrograms> AssetControllingPrograms { get; set; }

        public virtual ICollection<ProcessMap> ProcessMap { get; set; }
    }
}
