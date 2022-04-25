namespace EKS.ProcessMaps.Business.Interfaces
{
    using EKS.ProcessMaps.Models; 
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// ICustomAppService
    /// </summary>
    public interface ICustomAppService
    {
        /// <summary>
        /// GetContentData
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        ContentOutputModel GetContentData(int Id, string contentType);
    }
}
