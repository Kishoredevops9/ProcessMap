using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class Connectors
    {
        public int Id { get; set; }
        public int ChildActivityBlockId { get; set; }
        public int ParentActivityBlockId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int? BorderWidth { get; set; }
        public bool? ProtectedInd { get; set; }
        public bool? ExcludedInd { get; set; }
        public string CaptionStart { get; set; }
        public string CaptionMiddle { get; set; }
        public string CaptionEnd { get; set; }
        public int? FromEnd { get; set; }
        public int? ToEnd { get; set; }
        public int? FormatId { get; set; }

        public virtual ActivityBlocks ChildActivityBlock { get; set; }
        public virtual ActivityBlocks ParentActivityBlock { get; set; }
    }
}
