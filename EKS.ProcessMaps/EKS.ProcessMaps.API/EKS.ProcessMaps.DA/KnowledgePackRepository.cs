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
    public class KnowledgePackRepository : IKnowledgePackRepository
    {
        private readonly KnowledgeMapContext _context;
        
        public KnowledgePackRepository(KnowledgeMapContext context)
        {
            this._context = context;
        }

        public KnowledgePack FindKnowledgePackByContentId(string contentId)
        {
            return this._context.KnowledgePack
                .Include(x => x.Discipline)
                .Include(x => x.AssetType)
                .FirstOrDefault(x => x.ContentId == contentId);
        }
    }
}
