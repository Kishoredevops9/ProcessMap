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
    public class ProcessMapMetaControllerTest : ControllerTestBase
    {
        private ProcessMapMetaAppServiceBuilder _serviceBuilder = new ProcessMapMetaAppServiceBuilder();

        [Fact]
        public async Task CreateProcessMapMetaTest()
        {
            // Arrange
            var processMapMetaModel = TestData.ProcessMapMetaModels.FirstOrDefault();
            var privateAssetService = this._serviceBuilder.CreateProcessMapMetaAsync_Setup(processMapMetaModel).Build();
            var controller = new ProcessMapMetaController(privateAssetService, this._logManager);

            // Act
            var response = await controller.CreateProcessMapMeta(processMapMetaModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateProcessMapMetaTest()
        {
            // Arrange
            var processMapMetaModel = TestData.ProcessMapMetaModels.FirstOrDefault();
            var privateAssetService = this._serviceBuilder.UpdateProcessMapMetaAsync_Setup(processMapMetaModel).Build();
            var controller = new ProcessMapMetaController(privateAssetService, this._logManager);

            // Act
            var response = await controller.UpdateProcessMapMeta(processMapMetaModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteProcessMapMetaTest()
        {
            // Arrange
            var processMapMetaModel = TestData.ProcessMapMetaModels.FirstOrDefault();
            var appService = this._serviceBuilder.DeleteProcessMapMetaAsync_Setup(expectedResult: true).Build();
            var controller = new ProcessMapMetaController(appService, this._logManager);

            // Act
            var response = await controller.DeleteProcessMapMeta(processMapMetaModel.Id);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
