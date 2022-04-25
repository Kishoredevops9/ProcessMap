using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IContainerItemAppService
    {
        Task<List<KPackMapExtendModel>> GetKpackMapByKnowledgeAssetId(string parentContentAssetId, int version);
    }
}
