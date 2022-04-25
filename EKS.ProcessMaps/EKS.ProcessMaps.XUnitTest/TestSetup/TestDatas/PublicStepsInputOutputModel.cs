using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<PublicStepsInputOutputModel> PublicStepsInputOutputModels =>
            new List<PublicStepsInputOutputModel>
            {
                new PublicStepsInputOutputModel
                {
                    Id = 1,
                    ContentId = "F-000001",
                    Title = "SF Test 1",
                    DisciplineId = 1,
                    AssetTypeId = 13,
                    AssetStatusId = 1,
                    Version = 1,
                    PrivateInd = false
                }
            };
    }
}
