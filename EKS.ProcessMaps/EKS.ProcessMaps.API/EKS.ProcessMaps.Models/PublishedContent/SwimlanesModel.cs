namespace EKS.ProcessMaps.Models.PublishedContent
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class SwimlanesModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Caption { get; set; }
        public int SequenceNumber { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public int KnowledgeAssetId { get; set; }
        public string Color { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int? BorderWidth { get; set; }
        public bool? ProtectedInd { get; set; }
        public int? DisciplineId { get; set; }
        public bool RequiredInd { get; set; }
        public string Size { get; set; }
    }
}
