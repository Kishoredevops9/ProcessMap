using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AssetStatements
    {
        public AssetStatements()
        {
            StatementPhases = new HashSet<StatementPhases>();
            StatementTags = new HashSet<StatementTags>();
            //TaskComponents = new HashSet<TaskComponents>();
        }

        public int Id { get; set; }
        public int KnowledgeAssetId { get; set; }
        public string AssetStatementTypeCode { get; set; }
        public int SequenceNumber { get; set; }
        public string Statement { get; set; }
        public string Rationale { get; set; }
        public string Link { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual KnowledgeAssets KnowledgeAsset { get; set; }
        public virtual ICollection<StatementPhases> StatementPhases { get; set; }
        public virtual ICollection<StatementTags> StatementTags { get; set; }
        //public virtual ICollection<TaskComponents> TaskComponents { get; set; }
    }
}
