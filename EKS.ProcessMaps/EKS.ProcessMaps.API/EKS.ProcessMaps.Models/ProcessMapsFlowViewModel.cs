namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// ProcessMapsFlowViewModel
    /// </summary>
    public class ProcessMapsFlowViewModel
    {
        public ProcessMapsFlowViewModel()
        {
            this.SwimLanes = new List<SwimLanesModel>();
            this.ActivityBlocks = new List<ActivityBlocksModel>();
            this.Phases = new List<PhasesModel>();
            this.ContentPhase = new List<int>();
            this.ContentExportCompliances = new List<ContentExportCompliancesModel>();
            this.ContentTag = new List<int>();
        }

        public int Id { get; set; }

        public string ContentId { get; set; }

        public string Title { get; set; }

        public int? DisciplineId { get; set; }

        public int? DisciplineCodeId { get; set; }

        public int? AssetTypeId { get; set; }

        public int? AssetStatusId { get; set; }

        public int? ApprovalRequirementId { get; set; }

        public int? ClassifierId { get; set; }

        public string ClockId { get; set; }

        public int? ConfidentialityId { get; set; }

        public int? RevisionTypeId { get; set; }

        public bool? ProgramControlled { get; set; }

        public bool? Outsourceable { get; set; }

        public int? Version { get; set; }

        public DateTime? EffectiveFrom { get; set; }

        public DateTime? EffectiveTo { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public string CreatedUser { get; set; }

        public DateTime? LastUpdateDateTime { get; set; }

        public string LastUpdateUser { get; set; }

        public int? UsjurisdictionId { get; set; }

        public int? UsclassificationId { get; set; }

        public int? ClassificationRequestNumber { get; set; }

        public DateTime? ClassificationDate { get; set; }

        public DateTime? Tpmdate { get; set; }

        public int? ExportAuthorityId { get; set; }

        public int? ControllingProgramId { get; set; }

        public string ContentOwnerId { get; set; }

        public string Keywords { get; set; }

        public string Author { get; set; }

        public bool? Confidentiality { get; set; }

        public string Purpose { get; set; }

        public string ProcessInstId { get; set; }

        public string CustomId { get; set; }

        public string DisciplineCode { get; set; }

        public bool PrivateInd { get; set; }

        public string SourceFileUrl { get; set; }

        public string ExportPdfurl { get; set; }

        public List<SwimLanesModel> SwimLanes { get; set; }

        public List<ActivityBlocksModel> ActivityBlocks { get; set; }

        public List<PhasesModel> Phases { get; set; }

        public List<int> ContentPhase { get; set; }

        public List<ContentExportCompliancesModel> ContentExportCompliances { get; set; }

        public List<int> ContentTag { get; set; }
    }
}
