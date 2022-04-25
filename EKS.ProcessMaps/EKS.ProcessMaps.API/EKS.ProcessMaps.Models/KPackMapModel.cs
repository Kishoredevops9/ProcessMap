namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;
    public class KPackMapModel
    {
        public int KPacksMapId { get; set; }
        public string ParentContentAssetId { get; set; }
        public string ContentAssetId { get; set; }
        public int ParentContentTypeId { get; set; }
        public int? Version { get; set; }
        public DateTime? EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
    }
}
