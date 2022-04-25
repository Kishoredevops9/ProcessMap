namespace EKS.ProcessMaps.Business.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using global::EKS.ProcessMaps.Models;

    /// <summary>
    /// Interface of IPublicStepsAppService
    /// </summary>
    public interface IProcessMapCommonAppService
    {
        Task<string> GenerateContentId(int processMapId, int? assetTypeId, string createdUser, bool isPrivateAsset);

        Task<string> GetAssetStatusAsync(int assetStatusId);
    }
}
