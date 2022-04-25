namespace EKS.ProcessMaps.Entities
{
    using System;
    using System.Collections.Generic;

    public class ActivityConnections
    {
        public int Id { get; set; }
        public int? ActivityBlockId { get; set; }
        public int? PreviousActivityBlockId { get; set; }
        public string Color { get; set; }
        public string BackgroundColor { get; set; }
        public string BorderColor { get; set; }
        public string BorderStyle { get; set; }
        public int? BorderWidth { get; set; }
        public int? Version { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public string CaptionStart { get; set; }
        public string CaptionMiddle { get; set; }
        public string CaptionEnd { get; set; }

        public virtual ActivityBlocks ActivityBlock { get; set; }
    }
}
