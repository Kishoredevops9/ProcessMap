using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.API
{
    public static class Constants
    {
        public const string PhaseSize = "500 0";
        public const string AllStatusDraft = "draft,submitted for approval,approved,waiting for jc,rejected,waiting for jc,classified,jc rejected,cancelled,exception";
        public const string AllStatusPublished = "current,published,archived,obsolete,hidden,wip,legacy,legacy/obsolete";
    }
}
