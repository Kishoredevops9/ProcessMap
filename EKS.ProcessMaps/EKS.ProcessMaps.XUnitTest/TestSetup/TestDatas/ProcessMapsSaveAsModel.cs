using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ProcessMapsSaveAsModel> ProcessMapsSaveAsModels =>
            new List<ProcessMapsSaveAsModel>
            {
                new ProcessMapsSaveAsModel { Id = 1, CreatedUser = "user@test.com", }
            };
    }
}
