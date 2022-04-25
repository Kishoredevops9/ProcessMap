using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities
{
    public class ProcessMapMeta
    {
        public int Id { get; set; }
        public int? ProcessMapId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int? Version { get; set; }
        public DateTime? Createdon { get; set; }
        public string CreatedbyUserid { get; set; }
        public DateTime? Modifiedon { get; set; }
        public string ModifiedbyUserid { get; set; }

        public virtual ProcessMap ProcessMap { get; set; }
    }
}
