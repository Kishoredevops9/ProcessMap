namespace EKS.ProcessMaps.Entities
{
    using System;

    public class PrivateAssets
    {
        public int ContentAssetId { get; set; }

        public int? ParentContentAssetId { get; set; }

        public int? ParentTaskId { get; set; }

        public int Version { get; set; }

        public DateTime EffectiveFrom { get; set; }

        public DateTime EffectiveTo { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public string CreatedUser { get; set; }

        public DateTime LastUpdateDateTime { get; set; }

        public string LastUpdateUser { get; set; }
    }
}
