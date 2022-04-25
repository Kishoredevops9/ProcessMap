namespace EKS.ProcessMaps.Entities
{
    using System;

    public class ActivityPage
    {
        /// ActivityPageId
        public long Id { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Title
        public string Title { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        /// SubDisciplineId
        public int? SubDisciplineId { get; set; }

        /// SubSubDisciplineId
        public int? SubSubDisciplineId { get; set; }

        /// SubSubSubDisciplineId
        public int? SubSubSubDisciplineId { get; set; }

        /// DisciplineCodeId
        public int? DisciplineCodeId { get; set; }

        /// AssetTypeId
        public int? AssetTypeId { get; set; }

        /// AssetStatusId
        public int? AssetStatusId { get; set; }

        /// ContentOwnerId
        public string ContentOwnerId { get; set; }

        /// ClassifierId
        public int? ClassifierId { get; set; }

        /// ConfidentialityId
        public int? ConfidentialityId { get; set; }

        /// RevisionTypeId
        public int? RevisionTypeId { get; set; }

        /// ProgramControlled
        public bool? ProgramControlled { get; set; }

        /// Outsourceable
        public bool? Outsourceable { get; set; }

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

        /// UsjurisdictionId
        public int? UsjurisdictionId { get; set; }

        /// UsclassificationId
        public int? UsclassificationId { get; set; }

        /// ClassificationRequestNumber
        public int? ClassificationRequestNumber { get; set; }

        /// ClassificationDate
        public DateTime? ClassificationDate { get; set; }

        /// Tpmdate
        public DateTime? TPMDate { get; set; }

        /// Keywords
        public string Keywords { get; set; }

        /// Author
        public string Author { get; set; }

        /// Confidentiality
        public bool? Confidentiality { get; set; }

        /// Purpose
        public string Purpose { get; set; }

        /// ProcessInstId
        public string ProcessInstId { get; set; }

        /// CustomId
        public string CustomId { get; set; }

        /// AssetStatus
        //public virtual AssetStatus AssetStatus { get; set; }

        ///// AssetType
        public virtual ContentType AssetType { get; set; }


        ///// ConfidentialityNavigation
        //public Confidentialities ConfidentialityNavigation { get; set; }

        ///// Discipline
        //public virtual Discipline Discipline { get; set; }

        ///// DisciplineCode
        //public virtual DisciplineCode DisciplineCode { get; set; }

        ///// RevisionTypes
        //public virtual RevisionTypes RevisionTypes { get; set; }

        ///// SubDiscipline
        //public virtual SubDiscipline SubDiscipline { get; set; }

        ///// SubSubDiscipline
        //public virtual SubSubDiscipline SubSubDiscipline { get; set; }

        ///// SubSubSubDiscipline
        //public virtual SubSubSubDiscipline SubSubSubDiscipline { get; set; }

        /// Usclassification
        //public virtual Usclassification Usclassification { get; set; }

        /// Usjurisdiction
        //public virtual Usjurisdiction Usjurisdiction { get; set; }
    }
}