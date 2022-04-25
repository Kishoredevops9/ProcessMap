namespace EKS.ProcessMaps.Entities
{
    using System;

    public class ContentInformation
    {
        public int Id { get; set; }
        public string ContentId { get; set; }
        public string ContentType { get; set; }
        public int? ContentItemId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
