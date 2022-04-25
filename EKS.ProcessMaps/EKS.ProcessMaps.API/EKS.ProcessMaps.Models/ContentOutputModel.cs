namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// ContentOutputModel
    /// </summary>
    public class ContentOutputModel
    {
        /// ContentOwner
        public string ContentOwner { get; set; }

        /// Author
        public string Author { get; set; }

        /// CoAuthor
        public string CoAuthor { get; set; }

        ///ClassifierId
        public int? ClassifierId { get; set; }

        /// ExportAuthorityId
        public int? ExportAuthorityId { get; set; }
        
        ///OutSourcable
        public bool OutSourcable { get; set; }

        ///ProgramControlled
        public bool ProgramControlled { get; set; }

        /// ControllingProgramGroup
        public string ControllingProgramGroup { get; set; }
    }
}
