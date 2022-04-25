namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// Model class of process map meta
    /// </summary>
    public class ProcessMapMetaModel
    {
        /// Id
        public int Id { get; set; }

        /// ProcessMapId
        public int? ProcessMapId { get; set; }

        /// Key
        public string Key { get; set; }

        /// Value
        public string Value { get; set; }

        /// Version
        public int? Version { get; set; }

        /// Createdon
        public DateTime? Createdon { get; set; }

        /// CreatedbyUserid
        public string CreatedbyUserid { get; set; }

        /// Modifiedon
        public DateTime? Modifiedon { get; set; }

        /// ModifiedbyUserid
        public string ModifiedbyUserid { get; set; }
    }
}
