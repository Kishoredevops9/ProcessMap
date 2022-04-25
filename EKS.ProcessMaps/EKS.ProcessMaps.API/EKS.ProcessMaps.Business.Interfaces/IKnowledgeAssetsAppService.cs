namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    public interface IKnowledgeAssetsAppService
    {
        Task<KnowledgeAssetModel> CreateKnowledgeAssetAsync(KnowledgeAssetModel kPackModel);
    }
}