namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ContentType
    /// </summary>
    public class ContentType
    {
        /// ContentTypeId
        public int ContentTypeId { get; set; }

        /// Name
        public string Name { get; set; }

        /// Code
        public string Code { get; set; }

        /// Status
        public string Status { get; set; }

        /// CreatedOn
        public DateTime? CreatedOn { get; set; }

        /// CreatorClockID
        public string CreatorClockID { get; set; }

        /// ModifiedOn
        public DateTime? ModifiedOn { get; set; }

        /// ModifierClockID
        public string ModifierClockID { get; set; }

        /// ActivityPage
        public virtual ICollection<ActivityPage> ActivityPage { get; set; }

        ///// ActivityContainer
        //public virtual ICollection<ActivityContainer> ActivityContainer { get; set; }

        ///// CriteriaGroup        
        //public virtual ICollection<CriteriaGroup> CriteriaGroup { get; set; }

        public virtual ICollection<KnowledgePack> KnowledgePack { get; set; }
    }
}
