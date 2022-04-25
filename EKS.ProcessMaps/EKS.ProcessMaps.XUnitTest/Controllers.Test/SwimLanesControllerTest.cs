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
    public class SwimLanesControllerTest : ControllerTestBase
    {
        private SwimlaneAppServiceBuilder _serviceBuilder = new SwimlaneAppServiceBuilder();

        [Fact]
        public async Task CreateActivityGroupsTest()
        {
            // Arrange
            var swimLanesModel = TestData.SwimLanesModels.FirstOrDefault();
            var appService = this._serviceBuilder.CreateActivityGroupsAsync_Setup(swimLanesModel).Build();
            var controller = new SwimLanesController(appService, this._logManager);

            // Act
            var response = await controller.CreateActivityGroups(swimLanesModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateActivityGroupsTest()
        {
            // Arrange
            var swimLanesModel = TestData.SwimLanesModels.FirstOrDefault();
            var appService = this._serviceBuilder.UpdateActivityGroupsAsync_Setup(swimLanesModel).Build();
            var controller = new SwimLanesController(appService, this._logManager);

            // Act
            var response = await controller.UpdateActivityGroups(swimLanesModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateAllActivityGroupsTest()
        {
            // Arrange
            var swimLanesModels = TestData.SwimLanesModels;
            var appService = this._serviceBuilder.UpdateAllActivityGroupsAsync_Setup(swimLanesModels).Build();
            var controller = new SwimLanesController(appService, this._logManager);

            // Act
            var response = await controller.UpdateAllActivityGroups(swimLanesModels);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeleteActivityGroupsTest()
        {
            // Arrange
            var swimLanesModel = TestData.SwimLanesModels.FirstOrDefault();
            var appService = this._serviceBuilder.DeleteActivityGroupsAsync_Setup(expectedResult: true).Build();
            var controller = new SwimLanesController(appService, this._logManager);

            // Act
            var response = await controller.DeleteActivityGroups(swimLanesModel.Id);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateActivityGroupsSequenceTest()
        {
            // Arrange
            var swimLanesModels = TestData.SwimLanesModels;
            var sequenceUpdateModels = TestData.SequenceUpdateModels;
            var appService = this._serviceBuilder.UpdateActivityGroupsSequenceAsync_Setup(expectedResult: swimLanesModels).Build();
            var controller = new SwimLanesController(appService, this._logManager);

            // Act
            var response = await controller.UpdateActivityGroupsSequence(sequenceUpdateModels);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
