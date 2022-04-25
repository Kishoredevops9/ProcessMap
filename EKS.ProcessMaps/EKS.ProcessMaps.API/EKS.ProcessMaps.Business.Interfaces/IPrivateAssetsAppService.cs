using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.Business.Interfaces
{
    public interface IPrivateAssetsAppService
    {
        Task<PrivateAssetsModel> CreatePrivateAssetsAsync(PrivateAssetsModel privateAssetsModel);

        Task<IEnumerable<PrivateAssetsModel>> GetAllPrivateAssetsAsync();

        Task<IEnumerable<PrivateAssetsModel>> GetPrivateAssetsByParentContentAssetIdAsync(int parentContentAssetId);

        Task<PrivateAssetsModel> UpdatePrivateAssetsAsync(PrivateAssetsModel privateAssetsModel);
    }
}
