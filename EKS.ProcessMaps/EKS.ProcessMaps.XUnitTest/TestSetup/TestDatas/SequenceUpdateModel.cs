using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<SequenceUpdateModel> SequenceUpdateModels =>
            new List<SequenceUpdateModel>
            {
                new SequenceUpdateModel { Id = 1, SequenceNumber = 1 }
            };
    }
}
