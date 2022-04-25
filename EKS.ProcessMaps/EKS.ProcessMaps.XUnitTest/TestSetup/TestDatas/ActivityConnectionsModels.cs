using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityConnectionsModel> ActivityConnectionsModels =>
            new List<ActivityConnectionsModel>
            {
                new ActivityConnectionsModel
                {
                    Id = 0,
                    ActivityBlockId = 2,
                    PreviousActivityBlockId = 1,
                },
            };
    }
}
