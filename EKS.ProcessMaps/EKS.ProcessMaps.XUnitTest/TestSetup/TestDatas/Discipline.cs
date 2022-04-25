using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<Discipline> Disciplines =>
            new List<Discipline>
            {
                new Discipline { DisciplineId = 1, DisciplineCode = "MD",
                    Discipline1 = "D1", Discipline2 = "D2", Discipline3 = "D3", Discipline4 = "D4", },
            };
    }
}
