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

    /// <summary>
    /// Repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ProcessMapRepository : IProcessMapRepository
    {
        private readonly KnowledgeMapContext _context;
        
        public ProcessMapRepository(KnowledgeMapContext context)
        {
            this._context = context;
        }

        public ProcessMap GetProcessMapNoTrackingById(long processMapId)
        {
            return this._context.ProcessMaps
                .Include(x => x.SwimLanes).ThenInclude(s => s.Discipline)
                .Include(x => x.SwimLanes).ThenInclude(s => s.ActivityBlocks).ThenInclude(x => x.ActivityConnections)
                .Include(x => x.ActivityBlocks).ThenInclude(a => a.ActivityConnections)
                .Include(x => x.Phases)
                .Include(x => x.ProcessMapMeta)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == processMapId);
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
