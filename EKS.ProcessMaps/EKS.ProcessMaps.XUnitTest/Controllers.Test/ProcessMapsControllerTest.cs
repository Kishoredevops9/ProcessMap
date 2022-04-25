using EKS.Common.Logging;
using EKS.ProcessMaps.API.Controllers;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
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
    public class ProcessMapsControllerTest : ControllerTestBase
    {
        private ProcessMapsAppServiceBuilder _procesMapServiceBuilder = new ProcessMapsAppServiceBuilder();
        private KnowledgeAssetAppServiceBuilder _knowledgeAssetServiceBuilder = new KnowledgeAssetAppServiceBuilder();
        private KnowledgeAssetExportAppServiceBuilder _exportServiceBuilder = new KnowledgeAssetExportAppServiceBuilder();
        private KnowledgeAssetCloneAppServiceBuilder _cloneServiceBuilder = new KnowledgeAssetCloneAppServiceBuilder();

        private IProcessMapsAppService _processMapService => _procesMapServiceBuilder.Build();
        private IKnowledgeAssetAppService _kaService => _knowledgeAssetServiceBuilder.Build();
        private IKnowledgeAssetExportAppService _exportService => _exportServiceBuilder.Build();
        private IKnowledgeAssetCloneAppService _cloneService => _cloneServiceBuilder.Build();

        [Fact]
        public async Task GetAllProcessMapsTest()
        {
            // Arrange
            var processMapModels = TestData.ProcessMapModels;
            var processMapService = this._procesMapServiceBuilder.GetAllProcessMapsAsync_Setup(processMapModels).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.GetAllProcessMaps();
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("published")]
        public async Task GetProcessMapsById_Published_Test(string status)
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var kaService = this._knowledgeAssetServiceBuilder.GetProcessMapByIdOrContentId_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(this._processMapService, kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.GetProcessMapsById(processMapModel.Id, "SF", status, processMapModel.ContentId, processMapModel.Version.Value, "email@email.com");
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("draft")]
        public async Task GetProcessMapsById_Draft_Test(string status)
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder
                .GetProcessMapByIdAsync_Setup(processMapModel)
                .GetProcessMapByContentId_Setup(processMapModel)
                .Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.GetProcessMapsById(processMapModel.Id, "SF", status, processMapModel.ContentId, processMapModel.Version.Value, "email@email.com");
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("published")]
        public async Task GetProcessMapsByContentId_Published_Test(string status)
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var kaService = this._knowledgeAssetServiceBuilder.GetProcessMapByIdOrContentId_Setup(processMapModel).Build();
            var exportService = this._exportServiceBuilder.Build();
            var cloneService = this._cloneServiceBuilder.Build();
            var controller = new ProcessMapsController(this._processMapService, kaService, exportService, cloneService, this._logManager);

            // Act
            var response = await controller.GetProcessMapsById(0, "SF", status, processMapModel.ContentId, processMapModel.Version.Value, "email@email.com");
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("draft")]
        public async Task GetProcessMapsByContentId_Draft_Test(string status)
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder
                .GetProcessMapByIdAsync_Setup(processMapModel)
                .GetProcessMapByContentId_Setup(processMapModel)
                .Build();
            var kaService = this._knowledgeAssetServiceBuilder.GetProcessMapByIdOrContentId_Setup(processMapModel).Build();
            var exportService = this._exportServiceBuilder.Build();
            var cloneService = this._cloneServiceBuilder.Build();
            var controller = new ProcessMapsController(processMapService, kaService, exportService, cloneService, this._logManager);

            // Act
            var response = await controller.GetProcessMapsById(0, "SF", status, processMapModel.ContentId, processMapModel.Version.Value, "email@email.com");
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Theory]
        [InlineData("published")]
        [InlineData("draft")]
        public async Task GetProcessMapsFlowViewByIdTest(string status)
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.GetProcessMapFlowViewByIdAsync_Setup(processMapModel).Build();
            var kaService = this._knowledgeAssetServiceBuilder.GetProcessMapFlowViewByIdOrContentId_Setup(processMapModel).Build();
            var exportService = this._exportServiceBuilder.Build();
            var cloneService = this._cloneServiceBuilder.Build();
            var controller = new ProcessMapsController(processMapService, kaService, exportService, cloneService, this._logManager);

            // Act
            var response = await controller.GetProcessMapsFlowViewById(processMapModel.Id, status, processMapModel.ContentId, processMapModel.Version.Value);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task CreateProcessMapsTest()
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapInOutModel = TestData.ProcessMapInputOutputModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.CreateProcessMapAsync_Setup(processMapInOutModel).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.CreateProcessMaps(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SaveAsStepFlowTest()
        {
            // Arrange
            var processMapSaveAsModel = TestData.ProcessMapsSaveAsModels.FirstOrDefault();
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var cloneService = this._cloneServiceBuilder.SaveAsStepFlowAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(this._processMapService, this._kaService, this._exportService, cloneService, this._logManager);

            // Act
            var response = await controller.SaveAsStepFlow(processMapSaveAsModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task SaveAsStepTest()
        {
            // Arrange
            var processMapSaveAsModel = TestData.ProcessMapsSaveAsModels.FirstOrDefault();
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var cloneService = this._cloneServiceBuilder.SaveAsStepAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(this._processMapService, this._kaService, this._exportService, cloneService, this._logManager);

            // Act
            var response = await controller.SaveAsStep(processMapSaveAsModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ReviseStepFlowTest()
        {
            // Arrange
            var expectedReviseCheckingResult = TestData.RevisionCheckingResults.FirstOrDefault();
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var cloneService = this._cloneServiceBuilder
                .ReviseStepFlowAsync_Setup(processMapModel)
                .IsAbleToReviseAsync_Setup(expectedReviseCheckingResult)
                .Build();
            var controller = new ProcessMapsController(this._processMapService, this._kaService, this._exportService, cloneService, this._logManager);

            // Act
            var processMapReviseModel = TestData.ProcessMapsReviseModels.FirstOrDefault();
            var response = await controller.ReviseStepFlow(processMapReviseModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task ReviseStepTest()
        {
            // Arrange
            var expectedReviseCheckingResult = TestData.RevisionCheckingResults.FirstOrDefault();
            var expectedReviseModel = TestData.ProcessMapModels.FirstOrDefault();
            var cloneService = this._cloneServiceBuilder
                .ReviseStepAsync_Setup(expectedReviseModel)
                .IsAbleToReviseAsync_Setup(expectedReviseCheckingResult)
                .Build();
            var controller = new ProcessMapsController(this._processMapService, this._kaService, this._exportService, cloneService, this._logManager);

            // Act
            var processMapReviseModel = TestData.ProcessMapsReviseModels.FirstOrDefault();
            var response = await controller.ReviseStep(processMapReviseModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateProcessMapsTest()
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.UpdateProcessMapAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.UpdateProcessMaps(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdatePropertiesInProcessMapTest()
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.UpdatePropertiesInProcessMapAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.UpdatePropertiesInProcessMap(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateProcessMapStatusTest()
        {
            // Arrange
            var processMapModel = TestData.ProcessMapModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.UpdateProcessMapStatusAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.UpdateProcessMapStatus(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task UpdateProcessMapsPurposeTest()
        {
            // Arrange
            var processMapModel = TestData.ProcessMapsPurposeModels.FirstOrDefault();
            var processMapService = this._procesMapServiceBuilder.UpdateProcessMapPurposeAsync_Setup(processMapModel).Build();
            var controller = new ProcessMapsController(processMapService, this._kaService, this._exportService, this._cloneService, this._logManager);

            // Act
            var response = await controller.UpdateProcessMapsPurpose(processMapModel);
            var result = response as OkObjectResult;

            // Assert
            Assert.Equal(200, result.StatusCode);
        }
    }
}
