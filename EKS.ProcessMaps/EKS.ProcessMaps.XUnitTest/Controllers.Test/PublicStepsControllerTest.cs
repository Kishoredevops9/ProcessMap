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
    public class PublicStepsControllerTest : ControllerTestBase
    {
        private PublicStepsAppServiceBuilder _serviceBuilder = new PublicStepsAppServiceBuilder();

        [Fact]
        public async Task CreatePublicStepsTest()
        {
            // Arrange
            var processMapInputOutputModel = TestData.PublicStepsInputOutputModels.FirstOrDefault();
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var appService = this._serviceBuilder.CreateProcessMapMetaAsync_Setup(processMapInputOutputModel).Build();
            var controller = new PublicStepsController(this._logManager, appService);

            // Act
            var response = await controller.CreatePublicSteps(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdatePublicStepsPurposeTest()
        {
            // Arrange
            var purposeModel = TestData.PublicStepsPurposeModels.FirstOrDefault();
            var appService = this._serviceBuilder.UpdatePublicStepsPurposeAsync_Setup(purposeModel).Build();
            var controller = new PublicStepsController(this._logManager, appService);

            // Act
            var response = await controller.UpdatePublicStepsPurpose(purposeModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
