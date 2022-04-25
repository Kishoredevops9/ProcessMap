using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AssetUserRoles
    {
        public AssetUserRoles()
        {
            AssetUsers = new HashSet<AssetUsers>();
        }

        public int Id { get; set; }
        public string AssetUserRoleCode { get; set; }
        public string Description { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<AssetUsers> AssetUsers { get; set; }
    }
}
