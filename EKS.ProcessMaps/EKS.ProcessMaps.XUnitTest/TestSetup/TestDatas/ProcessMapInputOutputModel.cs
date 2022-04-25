using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ProcessMapInputOutputModel> ProcessMapInputOutputModels =>
            new List<ProcessMapInputOutputModel>
            {
                new ProcessMapInputOutputModel
                {
                    StepFlowId = 1,
                    ContentId = "F-000003",
                    Title = "SF Test 3",
                    DisciplineId = 1,
                    AssetTypeId = 13,
                    AssetStatusId = 1,
                    Version = 1,
                    PrivateInd = false
                }
            };
    }
}
