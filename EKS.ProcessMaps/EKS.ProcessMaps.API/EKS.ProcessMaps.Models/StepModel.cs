namespace EKS.ProcessMaps.Models
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Model class for Steps
    /// </summary>
    public class StepModel
    {
        public StepModel()
        {
            StepSwimLanes = new List<StepSwimlane>();
            ContentPhase = new List<int>();
            ContentTag = new List<int>();
            ContentExportCompliances = new List<ContentExportCompliancesModel>();
            ActivityConnections = new List<ActivityConnectionsModel>();
        }

        /// Id
        public int StepId { get; set; }

        /// ContentId
        public string StepContentId { get; set; }

        /// Title
        public string StepTitle { get; set; }

        /// BaseType
        public string BaseType { get; set; }

        /// StepActivityBlockId
        public int? StepActivityBlockId { get; set; }

        public int? SequenceNumber { get; set; }

        public string AssetStatus { get; set; }
        /// SwimLanes
        public List<StepSwimlane> StepSwimLanes { get; set; }

        /// ContentPhase
        public List<int> ContentPhase { get; set; }

        /// ContentExportCompliances
        public List<ContentExportCompliancesModel> ContentExportCompliances { get; set; }

        /// ContentTag
        public List<int> ContentTag { get; set; }

        /// ActivityConnections
        public List<ActivityConnectionsModel> ActivityConnections { get; set; }
    }

    public class StepSwimlane
    {
        public StepSwimlane()
        {
            ActivityBlocks = new List<ActivityBlocksModel>();
        }
        /// Id
        public int SwimLaneId { get; set; }

        /// StepFlowId
        public int? StepId { get; set; }

        /// StepFlowId
        public string SwimLaneTitle { get; set; }

        /// BaseType
        public string BaseType { get; set; }

        /// DisciplineId
        public int? DisciplineId { get; set; }

        public string DisciplineText { get; set; }

        /// Color
        public string Color { get; set; }

        /// BorderColor
        public string BorderColor { get; set; }

        /// BorderStyle
        public string BorderStyle { get; set; }

        /// BorderWidth
        public int? BorderWidth { get; set; }

        public int? SequenceNumber { get; set; }

        /// ActivityBlocks
        public List<ActivityBlocksModel> ActivityBlocks { get; set; }
    }
}
