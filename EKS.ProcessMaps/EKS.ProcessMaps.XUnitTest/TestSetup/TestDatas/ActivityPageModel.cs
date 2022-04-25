using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityPageModel> ActivityPageModels =>
            new List<ActivityPageModel>
            {
                new ActivityPageModel
                {
                    Id = 1,
                    ContentId = "X-000001",
                    Title = "Title",
                    AssetTypeId = 8,
                    Version = 1,
                    CreatedUser = "User 1",
                    LastUpdateUser = "User 1"
                }
            };
    }
}
