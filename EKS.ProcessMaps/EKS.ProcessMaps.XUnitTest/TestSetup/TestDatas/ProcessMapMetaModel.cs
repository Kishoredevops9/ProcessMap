using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ProcessMapMetaModel> ProcessMapMetaModels =>
            new List<ProcessMapMetaModel>
            {
                new ProcessMapMetaModel { Id = 1, ProcessMapId = 1, Key = "Key Model 1", Value = "Value Model 1"}
            };
    }
}
