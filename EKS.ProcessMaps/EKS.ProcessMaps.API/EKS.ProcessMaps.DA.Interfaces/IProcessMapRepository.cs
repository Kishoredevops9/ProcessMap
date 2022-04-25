namespace EKS.ProcessMaps.DA.Interfaces
{
    using System.Collections.Generic;
    using EKS.ProcessMaps.Entities;

    public interface IProcessMapRepository
    {
        ProcessMap GetProcessMapNoTrackingById(long processMapId);
    }
}
