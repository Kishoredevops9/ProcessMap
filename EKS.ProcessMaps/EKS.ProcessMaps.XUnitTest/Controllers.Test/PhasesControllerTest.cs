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
    public class PhasesControllerTest : ControllerTestBase
    {
        private PhasesAppServiceBuilder _phasesBuilder = new PhasesAppServiceBuilder();

        [Fact]
        public async Task GetPhasesByIdTest()
        {
            // Arrange
            var phaseModel = TestData.PhasesModels.FirstOrDefault();
            var phasesAppService = this._phasesBuilder.GetPhasesByIdAsync_Setup(phaseModel).Build();
            var controller = new PhasesController(phasesAppService, this._logManager);

            // Act
            var response = await controller.GetPhasesById(phaseModel.Id);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task GetPhasesByProcessMapIdTest()
        {
            // Arrange
            var phaseModels = TestData.PhasesModels;
            var phasesAppService = this._phasesBuilder.GetPhasesByProcessMapIdAsync_Setup(phaseModels).Build();
            var controller = new PhasesController(phasesAppService, this._logManager);

            // Act
            var processMapId = phaseModels.FirstOrDefault().ProcessMapId;
            var response = await controller.GetPhasesByProcessMapId(processMapId);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreatePhasesTest()
        {
            // Arrange
            var phaseModel = TestData.PhasesModels.FirstOrDefault();
            var phasesAppService = this._phasesBuilder.CreatePhasesAsync_Setup(phaseModel).Build();
            var controller = new PhasesController(phasesAppService, this._logManager);

            // Act
            var response = await controller.CreatePhases(phaseModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdatephasesTest()
        {
            // Arrange
            var phaseModel = TestData.PhasesModels.FirstOrDefault();
            var phasesAppService = this._phasesBuilder.Updatephases_Setup(phaseModel).Build();
            var controller = new PhasesController(phasesAppService, this._logManager);

            // Act
            var response = await controller.Updatephases(phaseModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task DeletephasesTest()
        {
            // Arrange
            var phaseModel = TestData.PhasesModels.FirstOrDefault();
            var phasesAppService = this._phasesBuilder.Deletephases_Setup(true).Build();
            var controller = new PhasesController(phasesAppService, this._logManager);

            // Act
            var response = await controller.Deletephases(phaseModel.Id);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
