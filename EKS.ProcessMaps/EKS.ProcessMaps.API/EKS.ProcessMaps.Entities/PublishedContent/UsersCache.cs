using System;
using System.Collections.Generic;

namespace EKS.ProcessMaps.Entities.PublishedContent
{
    public class UsersCache
    {
        public long GlobalUid { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ClockId { get; set; }
        public string Aadid { get; set; }
        public string Email { get; set; }
        public string Nationality { get; set; }
        public string PwemploymentCode { get; set; }
        public string Groups { get; set; }
        public string BusinessUnitName { get; set; }
        public string DepartmentName { get; set; }
        public string MajorDepartmentName { get; set; }
        public bool Classifier { get; set; }
        public bool ExportControl { get; set; }
        public DateTime LastUpdateDateTime { get; set; }
    }
}
