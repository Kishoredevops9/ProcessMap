namespace EKS.ProcessMaps.Models
{
    using System;

    public class ContentExportCompliancesModel
    {
        public int Id { get; set; }
        public int ContentId { get; set; }
        public int TypeId { get; set; }
        public int ExportComplianceId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
