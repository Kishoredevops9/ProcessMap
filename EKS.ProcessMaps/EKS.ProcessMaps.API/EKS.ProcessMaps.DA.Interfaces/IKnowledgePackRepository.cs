namespace EKS.ProcessMaps.DA.Interfaces
{
    using System.Collections.Generic;
    using EKS.ProcessMaps.Entities;

    public interface IKnowledgePackRepository
    {
        KnowledgePack FindKnowledgePackByContentId(string contentId);
    }
}
