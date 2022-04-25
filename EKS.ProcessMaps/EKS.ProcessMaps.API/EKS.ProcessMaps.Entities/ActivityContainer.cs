namespace EKS.ProcessMaps.Entities
{
    using System;

    /// <summary>
    /// ActivityContainer Entity
    /// </summary>
    public class ActivityContainer
    {
        /// ActivityContainerId
        public long ActivityContainerId { get; set; }

        /// ActivityPageId
        public long ActivityPageId { get; set; }

        /// ContentId
        public int ContentItemId { get; set; }

        /// Title
        public string Title { get; set; }

        /// ContentTypeId
        public int ContentTypeId { get; set; }

        /// ContentNo
        public string ContentNo { get; set; }

        /// US_JC
        public string US_JC { get; set; }

        /// Url
        public string Url { get; set; }

        /// Version
        public string Version { get; set; }

        /// ParentActivityContainerId
        public long ParentActivityContainerId { get; set; }

        /// OrderNo
        public int OrderNo { get; set; }

        /// CreatedOn
        public DateTime? CreatedOn { get; set; }

        /// CreatorClockId
        public string CreatorClockId { get; set; }

        /// ModifiedOn
        public DateTime? ModifiedOn { get; set; }

        /// ModifierClockId
        public string ModifierClockId { get; set; }

        /// Guidance
        public string Guidance { get; set; }

    }
}
