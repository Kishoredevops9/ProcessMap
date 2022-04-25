using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<PhasesModel> PhasesModels =>
            new List<PhasesModel>
            {
                new PhasesModel 
                { 
                    Id = 1, 
                    ProcessMapId = 1, 
                    Name = "Phases model test 1", 
                    Caption = "Phases model test 1", 
                    SequenceNumber = 1
                }
            };
    }
}
