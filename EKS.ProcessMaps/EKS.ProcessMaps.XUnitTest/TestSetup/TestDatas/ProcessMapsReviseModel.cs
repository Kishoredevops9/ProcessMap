using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ProcessMapsReviseModel> ProcessMapsReviseModels =>
            new List<ProcessMapsReviseModel>
            {
                new ProcessMapsReviseModel 
                {
                    Id = 1,
                    CreatedUser = "user@test.com",
                },
            };
    }
}
