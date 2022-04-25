using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.Entities
{
    public class ToDoTask
    {
        /// ToDoId
        public long ToDoId { get; set; }

        /// ContentTypeId
        public int? ContentTypeId { get; set; }

        /// ContentId
        public string ContentId { get; set; }

        /// Name
        public string Name { get; set; }

        /// Comments
        public string Comments { get; set; }

        /// IsDone
        public bool IsDone { get; set; }

        /// Url
        public string Url { get; set; }

        /// ContentStatus
        public string ContentStatus { get; set; }

        /// ActionId
        public int ActionId { get; set; }

        /// DueDate
        public DateTime DueDate { get; set; }

        /// RequestedByUserId
        public string RequestedByUserId { get; set; }

        /// TaskRequestedDate
        public DateTime TaskRequestedDate { get; set; }

        /// AssignedToUserID
        public string AssignedToUserID { get; set; }

        /// TaskCompletedDate
        public DateTime TaskCompletedDate { get; set; }

        /// UserPermission
        public string UserPermission { get; set; }

        /// AssignedToUserName
        public string AssignedToUserName { get; set; }

        /// ItemId
        public long? ItemId { get; set; }

        /// Version
        public int? Version { get; set; }
    }
}
