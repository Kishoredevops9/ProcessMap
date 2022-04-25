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
    public class ActivityBlocksControllerTest : ControllerTestBase
    {
        private ActivitiesAppServiceBuilder _activityBlockBuilder = new ActivitiesAppServiceBuilder();

        [Fact]
        public async Task CreateKPacksMapTest()
        {
            // Arrange
            var activityBlockModel = TestData.ActivityBlocksModels.FirstOrDefault();
            var appService = this._activityBlockBuilder.CreateActivitiesAsync_Setup(activityBlockModel).Build();
            var controller = new ActivityBlocksController(appService, this._logManager);

            // Act
            var response = await controller.CreateActivities(activityBlockModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
