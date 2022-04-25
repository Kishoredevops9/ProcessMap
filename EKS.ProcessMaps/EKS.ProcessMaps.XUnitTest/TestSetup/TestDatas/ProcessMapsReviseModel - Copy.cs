using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<RevisionCheckingResult> RevisionCheckingResults =>
            new List<RevisionCheckingResult>
            {
                new RevisionCheckingResult
                {
                    IsAbleToRevise = true,
                    Message = string.Empty,
                },
            };
    }
}
