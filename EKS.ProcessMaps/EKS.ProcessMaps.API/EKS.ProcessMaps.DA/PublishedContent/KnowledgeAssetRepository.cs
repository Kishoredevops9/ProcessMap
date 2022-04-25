namespace EKS.ProcessMaps.DA
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using Microsoft.EntityFrameworkCore;
    using PE = EKS.ProcessMaps.Entities.PublishedContent;
    using EksEnum = EKS.ProcessMaps.Helper.Enum;

    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class KnowledgeAssetRepository : IKnowledgeAssetRepository
    {
        private readonly PublishContentContext _context;
        
        /// <summary>
        /// Contructor
        /// </summary>
        /// <param name="context"></param>
        public KnowledgeAssetRepository(PublishContentContext context)
        {
            this._context = context;
        }

        public async Task<PE.KnowledgeAssets> GetKnowledgeAssetsByIdNoTrackingAsync(int id)
        {
            var ka = await this._context.KnowledgeAssets.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id).ConfigureAwait(false);

            return ka;
        }

        /// <summary>
        /// GetKnowledgeAssetsNoTracking
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<PE.KnowledgeAssets> GetKnowledgeAssets_NoStepAsyn(int id, string contentId, int version)
        {
            var ka = await this._context.KnowledgeAssets
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync(x => (id != 0 && x.Id == id) || (id == 0 && x.ContentId == contentId
                    && ((version > 0 && x.Version == version) || (version == 0))));

            await this._context.Entry(ka).Collection(x => x.AssetUsers).Query().Include(x => x.AssetUserRole).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetControllingPrograms).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.PhasesMap).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetPhases).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetExportCompliances).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetKeywords).Query().Include(x => x.Keyword).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetTags).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetParts).Query().Include(x => x.AssetPartType).LoadAsync(); // NOC | Purpose
            await this._context.Entry(ka).Collection(x => x.SwimLanes).Query().Include(x => x.Discipline).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.ActivityBlocks).Query().Include(x => x.ConnectorsChildActivityBlock).LoadAsync();

            return ka;
        }

        /// <summary>
        /// GetKnowledgeAssets_ForCloneAsyn
        /// </summary>
        /// <param name="knowledgeAssetId"></param>
        /// <returns></returns>
        public async Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForCloneAsyn(int knowledgeAssetId)
        {
            var ka = await this._context.KnowledgeAssets.FirstOrDefaultAsync(x => x.Id == knowledgeAssetId);
            await this._context.Entry(ka).Collection(x => x.AssetUsers).Query().Include(x => x.AssetUserRole).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetControllingPrograms).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.PhasesMap).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetPhases).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetKeywords).Query().Include(x => x.Keyword).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetTags).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetParts).Query().Include(x => x.AssetPartType).LoadAsync(); // NOC | Purpose
            await this._context.Entry(ka).Collection(x => x.SwimLanes).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.ContainerItems).LoadAsync();
            //await this._context.Entry(ka).Collection(x => x.PrivateAssetsParentAsset).LoadAsync();

            return ka;
        }

        /// <summary>
        /// GetKnowledgeAssets_ForStepFlowAsyn
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForStepFlowAsyn(int id, string contentId, int version)
        {
            var ka = await this._context.KnowledgeAssets
                .OrderByDescending(x => x.Version)
                .FirstOrDefaultAsync(x => (id != 0 && x.Id == id) || (id == 0 && x.ContentId == contentId 
                    && ((version > 0 && x.Version == version) || (version == 0)))).ConfigureAwait(false);

            await this._context.Entry(ka).Collection(x => x.AssetUsers).Query().Include(x => x.AssetUserRole).LoadAsync().ConfigureAwait(false);
            await this._context.Entry(ka).Collection(x => x.AssetControllingPrograms).LoadAsync().ConfigureAwait(false);
            await this._context.Entry(ka).Collection(x => x.AssetExportCompliances).LoadAsync().ConfigureAwait(false);
            await this._context.Entry(ka).Collection(x => x.AssetKeywords).Query().Include(x => x.Keyword).LoadAsync().ConfigureAwait(false);
            await this._context.Entry(ka).Collection(x => x.AssetParts).Query().Include(x => x.AssetPartType).LoadAsync().ConfigureAwait(false); // NOC | Purpose
            await this._context.Entry(ka).Collection(x => x.SwimLanes).Query().Include(x => x.ActivityBlocks).LoadAsync().ConfigureAwait(false);
            await this._context.Entry(ka).Reference(x => x.AssetStatus).LoadAsync().ConfigureAwait(false);

            return ka;
        }

        /// <summary>
        /// GetKnowledgeAssets_ForStepAsyn
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public PE.KnowledgeAssets GetKnowledgeAssets_ForStepAsyn(int id, string contentId, int version)
        {
            var ka = this._context.KnowledgeAssets
                .OrderByDescending(x => x.Version)
                .FirstOrDefault(x => (id != 0 && x.Id == id) || (id == 0 && x.ContentId == contentId
                    && ((version > 0 && x.Version == version) || (version == 0))));

            this._context.Entry(ka).Collection(x => x.SwimLanes).Query()?.Include(x => x.ActivityBlocks)?.Load();
            this._context.Entry(ka).Reference(x => x.AssetStatus).Load();

            return ka;
        }

        /// <summary>
        /// GetKnowledgeAssets_ForClone_StepAsyn
        /// </summary>
        /// <param name="knowledgeAssetStepId"></param>
        /// <returns></returns>
        public async Task<PE.KnowledgeAssets> GetKnowledgeAssets_ForClone_StepAsyn(int knowledgeAssetStepId)
        {
            var ka = await this._context.KnowledgeAssets.FirstOrDefaultAsync(x => x.Id == knowledgeAssetStepId);
            
            await this._context.Entry(ka).Collection(x => x.PhasesMap).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetPhases).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetTags).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.AssetParts).Query().Include(x => x.AssetPartType).LoadAsync(); // NOC | Purpose
            await this._context.Entry(ka).Collection(x => x.SwimLanes).LoadAsync();
            await this._context.Entry(ka).Collection(x => x.ContainerItems).LoadAsync();

            return ka;
        }

        private bool disposed = false;

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this._context.Dispose();
                }

                this.disposed = true;
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);

            GC.SuppressFinalize(this);
        }
    }
}
