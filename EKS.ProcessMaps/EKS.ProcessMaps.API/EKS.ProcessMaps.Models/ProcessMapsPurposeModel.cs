namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model class of process map
    /// </summary>
    public class ProcessMapsPurposeModel
    {
        public int StepFlowId { get; set; }

        public string ContentId { get; set; }

        public string Purpose { get; set; }
    }
}
