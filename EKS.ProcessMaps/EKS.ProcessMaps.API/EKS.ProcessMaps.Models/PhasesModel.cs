namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model class of phases
    /// </summary>
    public class PhasesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Caption { get; set; }

        public int? SequenceNumber { get; set; }

        public int? Version { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string CreatedUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public int? ProcessMapId { get; set; }

        public string Size { get; set; }
        public string Location { get; set; }

    }
}
