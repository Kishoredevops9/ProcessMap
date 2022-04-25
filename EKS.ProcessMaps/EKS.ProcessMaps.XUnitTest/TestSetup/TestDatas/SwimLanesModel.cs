using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<SwimLanesModel> SwimLanesModels =>
            new List<SwimLanesModel>
            {
                new SwimLanesModel { Id = 1, ProcessMapId = 1, SequenceNumber = 1 }
            };
    }
}
