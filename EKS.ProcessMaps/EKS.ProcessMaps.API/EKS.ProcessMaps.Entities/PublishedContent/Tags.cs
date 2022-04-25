using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class Tags
    {
        public Tags()
        {
            AssetTags = new HashSet<AssetTags>();
            StatementTags = new HashSet<StatementTags>();
            //TaskTags = new HashSet<TaskTags>();
            //TasksEngineModelTag = new HashSet<Tasks>();
            //TasksInitialEngineModelTag = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentTagsId { get; set; }
        public int Version { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime? EffectiveTo { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public string CreatedUser { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
        public string LastUpdateUser { get; set; }

        public virtual ICollection<AssetTags> AssetTags { get; set; }
        public virtual ICollection<StatementTags> StatementTags { get; set; }
        //public virtual ICollection<TaskTags> TaskTags { get; set; }
        //public virtual ICollection<Tasks> TasksEngineModelTag { get; set; }
        //public virtual ICollection<Tasks> TasksInitialEngineModelTag { get; set; }
    }
}
