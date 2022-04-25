namespace EKS.ProcessMaps.Models.PublishedContent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class ActivityBlocksModel
    {
        public int Id { get; set; }
        public string Caption { get; set; }
        public int ActivityBlockTypeId { get; set; }
        public int SwimLaneId { get; set; }
        public int? PhaseId { get; set; }
        public int? KnowledgeAssetId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int? BorderWidth { get; set; }
        public int? SequenceNumber { get; set; }
        public string AssetContentId { get; set; }
        public bool? ProtectedInd { get; set; }
        public bool? RequiredInd { get; set; }
        public int LocationX { get; set; }
        public int LocationY { get; set; }
        public int? Length { get; set; }
        public int? Width { get; set; }
        public int? FormatId { get; set; }
    }
}
