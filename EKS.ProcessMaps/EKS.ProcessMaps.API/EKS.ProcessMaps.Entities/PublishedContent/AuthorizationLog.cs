using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class AuthorizationLog
    {
        public int Id { get; set; }
        public string User { get; set; }
        public string ContentId { get; set; }
        public int Version { get; set; }
        public DateTime RequestDateTime { get; set; }
        public bool Outcome { get; set; }
        public int NationalityId { get; set; }
        public bool Citizenship { get; set; }
        public string Groups { get; set; }
        public int UsjurisdictionId { get; set; }
        public int UsclassificationId { get; set; }
        public bool Outsourcable { get; set; }
        public int? ConfidentialityId { get; set; }
        public bool ProgramControlled { get; set; }
        public string RemoteAddress { get; set; }
        public string ForwardedFor { get; set; }
    }
}
