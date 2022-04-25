namespace EKS.ProcessMaps.Entities
{
    using System;
    public class Classifiers
    {
        public int ClassifiersId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Version { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string CreatedUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public string BusinessUnit { get; set; }
        public string FunctionalArea { get; set; }
        public string ClockId { get; set; }
    }
}
