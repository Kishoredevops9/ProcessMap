namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface - IProcessMapsAppService
    /// </summary>
    public interface IExportExcelAppService
    {
        string Hyperlink(string universalUrl, string contentType, string contentId, int version);
        byte[] CreateExcel(List<ProcessMapExcelModel> stepFlowModels, string heading, string header);
    }
}
