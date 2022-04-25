namespace EKS.ProcessMaps.Models
{
    using EKS.ProcessMaps.API.Helper;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Filters;
    using System;
    using System.Collections.Generic;

    public class AuthRequestQueryModel
    {
        public string Status { get; set; }
        public string ContentType { get; private set; }
        public int Id { get; private set; }
        public string ContentId { get; private set; }
        public string UserName { get; private set; }
        public bool HasValue { get; set; }
        public AuthRequestQueryModel(HttpRequest request)
        {
            var dictionaryData = new Dictionary<string, string>();
            if (request.QueryString.HasValue)
            {
                var keys = request.Query.Keys;

                foreach (var item in keys)
                {
                    Microsoft.Extensions.Primitives.StringValues valueData;
                    request.Query.TryGetValue(item, out valueData);
                    if (item == "version" && valueData == "null")
                    {
                        dictionaryData.Add(item, "1");
                    }
                    else
                    {
                        dictionaryData.Add(item, valueData);
                    }

                }

                Initialize(dictionaryData);
            }
        }

        private void Initialize(Dictionary<string, string> dictionaryData)
        {
            HasValue = dictionaryData.Count > 0;
            var uiStatus = GetKey(dictionaryData, "status");
            ContentType = GetKey(dictionaryData, "contentType");
            string IdData = GetKey(dictionaryData, "id");
            if (!string.IsNullOrEmpty(IdData))
            {
                int id;
                int.TryParse(IdData, out id);
                Id = id;
            }
            //if (string.IsNullOrEmpty(uiStatus))
            //{
            //    uiStatus = "published";
            //}
            ContentId = GetKey(dictionaryData, "contentId");
            ContentId = ContentId == "undefined" ? null : ContentId;
            UserName = GetKey(dictionaryData, "currentUserEmail");
            Status = StringHelper.GetApiStatus(uiStatus);
        }
        private string GetKey(IReadOnlyDictionary<string, string> dictValues, string keyValue)
        {
            return dictValues.ContainsKey(keyValue) ? dictValues[keyValue] : "";
        }
    }
}
