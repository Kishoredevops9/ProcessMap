using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent
{
    public interface IKnowledgeAssetExportAppService
    {
        Task<byte[]> ExportStepFlowToExcel(int knowledgeAssetId, string url);
        Task<byte[]> ExportStepToExcel(int knowledgeAssetId, string url);
    }
}
