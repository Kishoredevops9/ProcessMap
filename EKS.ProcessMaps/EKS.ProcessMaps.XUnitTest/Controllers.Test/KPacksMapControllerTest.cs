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
    public class KPacksMapControllerTest : ControllerTestBase
    {
        private KPacksAppServiceBuilder _kPackMapBuilder = new KPacksAppServiceBuilder();
        private ContainerItemAppServiceBuilder _containerItemBuilder = new ContainerItemAppServiceBuilder();

        [Fact]
        public async Task CreateKPacksMapTest()
        {
            // Arrange
            var kPackMapModel = TestData.KpacksMapModels.FirstOrDefault();
            var kPackAppService = this._kPackMapBuilder.CreateKPacksMapAsync_Setup(kPackMapModel).Build();
            var containerItemAppService = this._containerItemBuilder.Build();
            var controller = new KPacksMapController(kPackAppService, containerItemAppService, this._logManager);

            // Act
            var response = await controller.CreateKPacksMap(kPackMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task AddKPacksMapsTest()
        {
            // Arrange
            var kPackMapModels = TestData.KpacksMapModels;
            var kPackAppService = this._kPackMapBuilder.AddKPacksMapAsync_Setup(kPackMapModels).Build();
            var containerItemAppService = this._containerItemBuilder.Build();
            var controller = new KPacksMapController(kPackAppService, containerItemAppService, this._logManager);

            // Act
            var response = await controller.AddKPacksMaps(kPackMapModels);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeletekpackmapTest()
        {
            // Arrange
            var kPackAppService = this._kPackMapBuilder.DeleteKPacksMapAsync_Setup(true).Build();
            var containerItemAppService = this._containerItemBuilder.Build();
            var controller = new KPacksMapController(kPackAppService, containerItemAppService, this._logManager);

            // Act
            var kpackMapId = 1;
            var response = await controller.Deletekpackmap(kpackMapId);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
