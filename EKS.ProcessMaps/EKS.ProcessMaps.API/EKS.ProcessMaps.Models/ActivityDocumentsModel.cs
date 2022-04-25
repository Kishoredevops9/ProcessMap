namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// Model class of activity documents.
    /// </summary>
    public class ActivityDocumentsModel
    {
        /// Id
        public int Id { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// ActivityBlockId
        public int? ActivityBlockId { get; set; }

        /// Uri
        public string Uri { get; set; }

        /// Type
        public string Type { get; set; }

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

        /// SubProcessMapId
        public int? SubProcessMapId { get; set; }

        /// ActivityPageId
        public long? ActivityPageId { get; set; }

        /// Label
        public string Label { get; set; }
    }
}
