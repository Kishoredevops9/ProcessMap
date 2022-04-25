using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;

namespace EKS.ProcessMaps.API.Helper
{
    public static class StringHelper
    {
        /// <summary>
        /// BuildDisciplineText
        /// </summary>
        /// <param name="dis"></param>
        /// <returns></returns>
        public static string BuildDisciplineText(PE.Disciplines dis)
        {
            var disciplines = new List<string> { dis.Discipline1 };
            if (!string.IsNullOrEmpty(dis.Discipline2))
            {
                disciplines.Add(dis.Discipline2);
            }

            if (!string.IsNullOrEmpty(dis.Discipline3))
            {
                disciplines.Add(dis.Discipline3);
            }

            if (!string.IsNullOrEmpty(dis.Discipline4))
            {
                disciplines.Add(dis.Discipline4);
            }

            var disciplineText = string.Join(" > ", disciplines);
            return disciplineText;
        }

        public static string GetApiStatus(string uiStatus)
        {
            if (string.IsNullOrEmpty(uiStatus))
            {
                return "draft";
            }

            if (Constants.AllStatusPublished.Contains(uiStatus.ToLower()))
            {
                return "published";
            }
            else
            {
                return "draft";
            }
        }
    }
}
