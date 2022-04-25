namespace EKS.ProcessMaps.Entities
{
    using System;

    /// <summary>
    /// MasterDisciplineCode
    /// </summary>
    public class MasterDisciplineCode
    {
        /// Id
        public int Id { get; set; }

        /// Code
        public string Code { get; set; }

        /// Status
        public string Status { get; set; }

        /// CreatedOn
        public DateTime? CreatedOn { get; set; }

        /// CreatorClockId
        public string CreatorClockId { get; set; }

        /// ModifiedOn
        public DateTime? ModifiedOn { get; set; }

        /// ModifierClockId
        public string ModifierClockId { get; set; }
    }
}
