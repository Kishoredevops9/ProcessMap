using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<PublicStepsPurposeModel> PublicStepsPurposeModels =>
            new List<PublicStepsPurposeModel>
            {
                new PublicStepsPurposeModel
                {
                    Id = 1,
                    ContentId = "F-000001",
                    Purpose = "Purpose"
                }
            };
    }
}
