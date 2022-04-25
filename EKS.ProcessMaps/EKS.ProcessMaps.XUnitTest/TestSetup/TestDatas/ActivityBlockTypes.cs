using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<ActivityBlockTypes> ActivityBlockTypes =>
            new List<ActivityBlockTypes>
            {
                new ActivityBlockTypes
                {
                    Id = 1,
                    Name = "Activity Block type 1",
                },
            };
    }
}
