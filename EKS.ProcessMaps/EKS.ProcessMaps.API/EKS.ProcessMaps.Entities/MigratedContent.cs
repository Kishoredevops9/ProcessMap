namespace EKS.ProcessMaps.Entities
{
    using System;

    /// <summary>
    /// MigratedContent.
    /// </summary>
    public class MigratedContent
    {
        public int Id { get; set; }
        public long Contentid { get; set; }
        public string Contentno { get; set; }
        public string Title { get; set; }
        public string CurrentStatus { get; set; }
        public int Version { get; set; }
        public string ContentType { get; set; }
        public DateTime CreatedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public string SourceDocUrl { get; set; }
        public string USClassification { get; set; }
    }
}
