namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ActivityPageAllOutputModel to use as output model for all activity page.
    /// </summary>
    public class ActivityPageAllOutputModel
    {
        /// CriteriaGroupId
        public long Id { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Title
        public string Title { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// DisciplineCodeId
        public int? DisciplineCodeId { get; set; }

        /// AssetTypeId
        public int? AssetTypeId { get; set; }

        /// AssetTypeCode
        public string AssetTypeCode { get; set; }

        /// Purpose
        public string Purpose { get; set; }        

        /// ProcessInstId
        public string ProcessInstId { get; set; }

        /// DisciplineCode
        public string DisciplineCode { get; set; }

        /// DisciplineName
        public string DisciplineName { get; set; }

        /// Content
        public List<ActivityContainerModel> Content { get; set; }

    }
}
