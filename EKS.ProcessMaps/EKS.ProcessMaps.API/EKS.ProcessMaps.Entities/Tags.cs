namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Tags
    /// </summary>
    public class Tags
    {
        /// TagsId
        public int? TagsId { get; set; }

        /// Name
        public string Name { get; set; }

        /// ParentTagsId
        public int? ParentTagsId { get; set; }

        /// Version
        public int? Version { get; set; }

        /// EffectiveFrom
        public DateTime? EffectiveFrom { get; set; }

        /// EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        /// CreatedDateTime
        public DateTime? CreatedDateTime { get; set; }

        /// CreatedUser
        public string CreatedUser { get; set; }

        /// LastUpdateDateTime
        public DateTime? LastUpdateDateTime { get; set; }

        /// LastUpdateUser
        public string LastUpdateUser { get; set; }

        /// ContentTag
        public virtual ICollection<ContentTags> ContentTags { get; set; }
    }
}
