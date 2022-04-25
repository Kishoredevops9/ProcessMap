using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ContainerItems
    {
        public ContainerItems()
        {
            //ContainerItemsRelationsFromContainerItem = new HashSet<ContainerItemsRelations>();
            //ContainerItemsRelationsToContainerItem = new HashSet<ContainerItemsRelations>();
            ExcludedContainerItems = new HashSet<ExcludedContainerItems>();
            InverseParentContainerItem = new HashSet<ContainerItems>();
            //TaskComponents = new HashSet<TaskComponents>();
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

        public virtual KnowledgeAssets ContainerKnowledgeAsset { get; set; }
        public virtual ContainerItems ParentContainerItem { get; set; }
        //public virtual ICollection<ContainerItemsRelations> ContainerItemsRelationsFromContainerItem { get; set; }
        //public virtual ICollection<ContainerItemsRelations> ContainerItemsRelationsToContainerItem { get; set; }
        public virtual ICollection<ExcludedContainerItems> ExcludedContainerItems { get; set; }
        public virtual ICollection<ContainerItems> InverseParentContainerItem { get; set; }
        //public virtual ICollection<TaskComponents> TaskComponents { get; set; }
    }
}
