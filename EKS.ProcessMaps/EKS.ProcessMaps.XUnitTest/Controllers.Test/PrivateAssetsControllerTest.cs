using EKS.Common.Logging;
using EKS.ProcessMaps.API.Controllers;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestBases;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EKS.ProcessMaps.XUnitTest.Controllers.Test
{
    public class PrivateAssetsControllerTest : ControllerTestBase
    {
        private PrivateAssetsAppServiceBuilder _serviceBuilder = new PrivateAssetsAppServiceBuilder();

        [Fact]
        public async Task GetAllPrivateAssetsTest()
        {
            // Arrange
            var privateAssetModel = TestData.PrivateAssetsModels;
            var appService = this._serviceBuilder.GetAllPrivateAssetsAsync_Setup(privateAssetModel).Build();
            var controller = new PrivateAssetsController(appService, this._logManager);

            // Act
            var response = await controller.GetAllPrivateAssets();
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetPrivateAssetsByParentContentAssetIdTest()
        {
            // Arrange
            var privateAssetModels = TestData.PrivateAssetsModels;
            var appService = this._serviceBuilder.GetPrivateAssetsByParentContentAssetIdAsync_Setup(privateAssetModels).Build();
            var controller = new PrivateAssetsController(appService, this._logManager);

            // Act
            var response = await controller.GetPrivateAssetsByParentContentAssetId(privateAssetModels.FirstOrDefault().ParentContentAssetId.Value);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreatePrivateAssetsTest()
        {
            // Arrange
            var privateAssetModel = TestData.PrivateAssetsModels.FirstOrDefault();
            var appService = this._serviceBuilder.CreatePrivateAssetsAsync_Setup(privateAssetModel).Build();
            var controller = new PrivateAssetsController(appService, this._logManager);

            // Act
            var response = await controller.CreatePrivateAssets(privateAssetModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdatePrivateAssetsTest()
        {
            // Arrange
            var privateAssetModel = TestData.PrivateAssetsModels.FirstOrDefault();
            var appService = this._serviceBuilder.UpdatePrivateAssetsAsync_Setup(privateAssetModel).Build();
            var controller = new PrivateAssetsController(appService, this._logManager);

            // Act
            var response = await controller.UpdatePrivateAssets(privateAssetModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
