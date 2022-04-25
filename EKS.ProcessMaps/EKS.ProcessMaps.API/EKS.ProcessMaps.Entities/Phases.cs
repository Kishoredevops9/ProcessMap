using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.Entities
{
    public class Phases
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

        public virtual ProcessMap ProcessMap { get; set; }
        public virtual ICollection<ActivityBlocks> ActivityBlocks { get; set; }
    }
}
