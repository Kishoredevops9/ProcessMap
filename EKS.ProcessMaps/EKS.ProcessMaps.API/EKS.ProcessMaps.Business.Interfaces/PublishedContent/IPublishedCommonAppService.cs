using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IPublishedCommonAppService
    {
        string GetKnowledgeAssetStatus(string contentId, int version);
    }
}
