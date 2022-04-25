using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class StatementPhases
    {
        public int Id { get; set; }
        public int AssetStatementId { get; set; }
        public int PhaseId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual AssetStatements AssetStatement { get; set; }
        public virtual PhasesRef Phase { get; set; }
    }
}
