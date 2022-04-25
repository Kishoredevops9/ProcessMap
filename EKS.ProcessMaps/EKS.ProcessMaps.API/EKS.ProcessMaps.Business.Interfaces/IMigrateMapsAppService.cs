namespace EKS.ProcessMaps.Business.Interfaces
{
    using System.Threading.Tasks;
    using EKS.ProcessMaps.Models;

    /// <summary>
    /// IMigrateMapsAppService
    /// </summary>
    public interface IMigrateMapsAppService
    {
        /// <summary>
        /// MigrateMapsAsync.
        /// </summary>
        /// <param name="contentAssetTypeModel"></param>
        /// <returns></returns>
        Task<ProcessMapInputOutputModel> MigrateMapsAsync(ContentAssetTypeModel contentAssetTypeModel);
    }
}
