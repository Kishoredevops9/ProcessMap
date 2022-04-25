using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityBlocksModel> ActivityBlocksModels =>
            new List<ActivityBlocksModel>
            {
                new ActivityBlocksModel
                {
                    Id = 0,
                    SwimLaneId = 1,
                    ActivityTypeId  = 1,
                    SequenceNumber = 1,
                    Name = "Activity Block 2",
                }
            };
    }
}
