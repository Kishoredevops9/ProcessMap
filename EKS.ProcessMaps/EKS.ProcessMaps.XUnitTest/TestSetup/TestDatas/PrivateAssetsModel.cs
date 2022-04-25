using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas
{
    public static partial class TestData
    {
        public static List<PrivateAssetsModel> PrivateAssetsModels =>
            new List<PrivateAssetsModel>
            {
                new PrivateAssetsModel
                {
                    ContentAssetId = 3,
                     ParentContentAssetId = 1,
                     Version = 1
                }
            };
    }
}
