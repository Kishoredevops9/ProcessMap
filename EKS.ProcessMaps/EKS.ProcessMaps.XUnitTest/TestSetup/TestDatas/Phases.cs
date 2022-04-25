using EKS.ProcessMaps.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<Phases> Phases =>
            new List<Phases>
            {
                new Phases { Id = 1, ProcessMapId = 1, Name = "Phases test 1", Caption = "Phases Test 1", SequenceNumber = 1 }
            };
    }
}
