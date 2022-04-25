using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Models.PublishedContent
{
    public class ContainerItemsExportModel
    {
        public ContainerItemsExportModel()
        {
            Children = new List<ContainerItemsExportModel>();
        }

        public int Id { get; set; }
        public int ContainerKnowledgeAssetId { get; set; }
        public int? ParentContainerItemId { get; set; }
        public int Index { get; set; }
        public string AssetContentId { get; set; }
        public string Text { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public virtual List<ContainerItemsExportModel> Children { get; set; }
    }
}
