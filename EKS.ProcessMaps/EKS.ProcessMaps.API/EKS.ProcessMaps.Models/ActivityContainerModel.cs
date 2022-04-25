namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ActivityContainerModel to use as model for activity container
    /// </summary>
    public class ActivityContainerModel
    {
        /// ActivityContainerId
        public long ActivityContainerId { get; set; }

        /// ActivityPageId
        public long ActivityPageId { get; set; }

        /// ContentItemId
        public int ContentItemId { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Title
        public string Title { get; set; }

        /// ContentTypeId
        public int ContentTypeId { get; set; }

        /// ContentNo
        public string ContentNo { get; set; }

        /// US_JC
#pragma warning disable CA1707 // Identifiers should not contain underscores
        public string US_JC { get; set; }
#pragma warning restore CA1707 // Identifiers should not contain underscores

        /// Url
        public string Url { get; set; }

        /// Version
        public string Version { get; set; }

        /// ParentActivityContainerId
        public long? ParentActivityContainerId { get; set; }

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

        /// ActivityPageContentId
        public string ActivityPageContentId { get; set; }

        /// AssetContentId
        public string AssetContentId { get; set; }

        public string AssetStatus { get; set; }

        /// PurposeComponent
        public string PurposeComponent { get; set; }

        /// ChildList
        public List<ActivityContainerModel> ChildList { get; set; }

        /// Guidance
        public string Guidance { get; set; }

        // PropertiesLastUpdateDateTime
        public DateTime PropertiesLastUpdateDateTime { get; set; }

        /// AssetTypeCode
        public string AssetTypeCode { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

    }
}
