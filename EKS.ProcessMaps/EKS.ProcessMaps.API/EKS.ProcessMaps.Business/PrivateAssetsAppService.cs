namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using AutoMapper;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    public class PrivateAssetsAppService : IPrivateAssetsAppService
    {
        private readonly IRepository<PrivateAssets> _privateAssetsRepo;
        private readonly IMapper _mapper;

        public PrivateAssetsAppService(IRepository<PrivateAssets> privateAssetsRepo, IMapper mapper)
        {
            this._mapper = mapper;
            this._privateAssetsRepo = privateAssetsRepo;
        }

        public async Task<PrivateAssetsModel> CreatePrivateAssetsAsync(PrivateAssetsModel privateAssetsModel)
        {
            var entity = this._mapper.Map<PrivateAssets>(privateAssetsModel);
            PrivateAssets resultData = await this._privateAssetsRepo.AddAsyn(entity).ConfigureAwait(false);
            return this._mapper.Map<PrivateAssetsModel>(resultData);
        }

        public async Task<IEnumerable<PrivateAssetsModel>> GetAllPrivateAssetsAsync()
        {
            ICollection<PrivateAssets> resultData = await this._privateAssetsRepo.GetAllAsyn().ConfigureAwait(false);
            return this._mapper.Map<IEnumerable<PrivateAssetsModel>>(resultData);
        }

        public async Task<IEnumerable<PrivateAssetsModel>> GetPrivateAssetsByParentContentAssetIdAsync(int parentContentAssetId)
        {
            ICollection<PrivateAssets> resultData = await this._privateAssetsRepo.FindByAsyn(x => x.ParentContentAssetId == parentContentAssetId).ConfigureAwait(false);
            return this._mapper.Map<IEnumerable<PrivateAssetsModel>>(resultData);
        }

        public async Task<PrivateAssetsModel> UpdatePrivateAssetsAsync(PrivateAssetsModel privateAssetsModel)
        {
            int resultData = await this._privateAssetsRepo
                .UpdateAsyn(this._mapper.Map<PrivateAssets>(privateAssetsModel), privateAssetsModel.ContentAssetId)
                .ConfigureAwait(false);

            PrivateAssets result = await this._privateAssetsRepo
                .FindAsync(x => x.ContentAssetId == privateAssetsModel.ContentAssetId)
                .ConfigureAwait(false);

            return this._mapper.Map<PrivateAssetsModel>(result);
        }
    }
}
