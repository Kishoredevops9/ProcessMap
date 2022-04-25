namespace EKS.ProcessMaps.XUnitTest
{
    using System.Linq;
    using System.Threading.Tasks;
    using EKS.Common.Logging;
    using EKS.ProcessMaps.API.Controllers;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.Models;
    using EKS.ProcessMaps.XUnitTest.TestSetup.TestBases;
    using EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders;
    using EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    public class ActivityPagesControllerTest : ControllerTestBase
    {
        private ActivityPagesAppServiceBuilder _serviceBuilder = new ActivityPagesAppServiceBuilder();

        [Fact]
        public async Task CreateActivityMetaTest()
        {
            // Arrange
            var activityPageModel = TestData.ActivityPageModels;
            var appService = this._serviceBuilder.GetAllActivityPagesAsync_Setup(activityPageModel).Build();
            var controller = new ActivityPagesController(appService, this._logManager);

            // Act
            var response = await controller.GetAllActivityPages();
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
