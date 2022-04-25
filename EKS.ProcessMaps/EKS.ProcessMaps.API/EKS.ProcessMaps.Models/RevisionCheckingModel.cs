namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// RevisionCheckingResult
    /// </summary>
    public class RevisionCheckingResult
    {
        public bool IsAbleToRevise { get; set; }

        public string Message { get; set; }
    }
}
