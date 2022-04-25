namespace EKS.ProcessMaps.Models
{
    public class AuthRequestHeaderModel
    {
        public string EksGroup { get; set; }
        public string PwEmploymentCode { get; set; }
        public string Nationality { get; set; }
        public string HeaderEmail { get; set; }
        public string GlobalUId { get; set; }
        public bool HasValue { get; set; }
        public AuthRequestHeaderModel(string headerData)
        {
            if (!string.IsNullOrEmpty(headerData))
            {
                string[] parameter = headerData.Split(':');
                EksGroup = parameter[0];
                PwEmploymentCode = parameter[1];
                Nationality = parameter[2];
                HeaderEmail = parameter[3];
                GlobalUId = parameter[4];
                HasValue = true;
            }
        }
    }
}
