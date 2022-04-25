namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    public interface IKPacksAppService
    {
        Task<KPackMapModel> CreateKPacksMapAsync(KPackMapModel kPackModel);

        Task<IEnumerable<KPackMapModel>> AddKPacksMapAsync(IEnumerable<KPackMapModel> kPackMapModels);

        Task<List<KPackMapExtendModel>> GetKPacksMapByParentContentAssetIdAsync(string parentContentAssetId, int? version);
        Task<bool> DeleteKPacksMapAsync(long id);
    }
}