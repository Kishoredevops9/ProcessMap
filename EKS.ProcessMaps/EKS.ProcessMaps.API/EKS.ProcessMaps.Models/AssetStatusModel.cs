namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class AssetStatusModel
    {
        // AssetStatusId
        public int AssetStatusId { get; set; }

        // Name
        public string Name { get; set; }

        // Description
        public string Description { get; set; }

        // Version
        public int? Version { get; set; }

        // EffectiveFrom
        public DateTime? EffectiveFrom { get; set; }

        // EffectiveTo
        public DateTime? EffectiveTo { get; set; }

        // CreatedDateTime
        public DateTime? CreatedDateTime { get; set; }

        // CreatedUser
        public string CreatedUser { get; set; }

        // LastUpdateDateTime
        public DateTime? LastUpdateDateTime { get; set; }

        // LastUpdateUser
        public string LastUpdateUser { get; set; }
    }
}
