using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ProcessMapsPurposeModel> ProcessMapsPurposeModels =>
            new List<ProcessMapsPurposeModel>
            {
                new ProcessMapsPurposeModel
                {
                    StepFlowId = 1,
                    ContentId = "F-000003",
                    Purpose = "Purpose"
                }
            };
    }
}
