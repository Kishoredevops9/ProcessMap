namespace EKS.ProcessMaps.Models
{
    using System;

    /// <summary>
    /// ContentTypeModel is used as model for ContentType
    /// </summary>
    public class ContentTypeModel
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
    }
}
