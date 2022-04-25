using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class ContainerItem
    {
        public int TaskId { get; set; }
        public int TaskMapId { get; set; }
        public int ActivityBlockId { get; set; }
        public int KnowledgeAssetId { get; set; }
        public int? Expr1 { get; set; }
        public string Title { get; set; }
    }
}
