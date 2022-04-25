using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityConnections> ActivityConnections =>
            new List<ActivityConnections>
            {
                new ActivityConnections
                {
                    Id = 1,
                    ActivityBlockId = 2,
                    PreviousActivityBlockId = 1,
                },
            };
    }
}
