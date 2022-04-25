using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityMetaModel> ActivityMetaModels =>
            new List<ActivityMetaModel>
            {
                new ActivityMetaModel
                {
                    Id = 1,
                    ActivityBlockId = 1,
                    Key = "Key",
                    Value = "Value",
                    Version = 1,
                    CreatedUser = "User 1",
                    LastUpdateUser = "User 1"
                }
            };
    }
}
