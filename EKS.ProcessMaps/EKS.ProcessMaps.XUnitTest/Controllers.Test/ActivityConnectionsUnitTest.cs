namespace EKS.ProcessMaps.XUnitTest
{
    using System.Threading.Tasks;
    using EKS.Common.Logging;
    using EKS.ProcessMaps.API.Controllers;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.Models;
    using Microsoft.AspNetCore.Mvc;
    using Xunit;

    /// <summary>
    /// Unit test cases for activity connections.
    /// </summary>
    public class ActivityConnectionsUnitTest
    {
        /// <summary>
        /// Unit test case for create activity connections.
        /// </summary>
        [Fact]
        public void CreateActivityConnections()
        {
            // Arrange
            Moq.Mock<IActivityConnectionsAppService> mockService = new Moq.Mock<IActivityConnectionsAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            ActivityConnectionsModel requestData = new ActivityConnectionsModel
            {
                ActivityBlockId = 171,
                PreviousActivityBlockId = 170,
                BorderWidth = 2,
            };

            ActivityConnectionsModel model = new ActivityConnectionsModel
            {
                Id = 519,
                ActivityBlockId = 171,
                PreviousActivityBlockId = 170,
                BorderWidth = 2,
            };

            mockService.Setup(service => service.CreateActivityConnectionsAsync(requestData))
                .Returns(Task.FromResult(model));

            ActivityConnectionsController controller = new ActivityConnectionsController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.CreateActivityConnections(requestData).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<ActivityConnectionsModel>((ActivityConnectionsModel)okResult.Value);
        }

        /// <summary>
        /// Unit test case for update activity connection.
        /// </summary>
        [Fact]
        public void UpdateActivityConnections()
        {
            // Arrange
            Moq.Mock<IActivityConnectionsAppService> mockService = new Moq.Mock<IActivityConnectionsAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            ActivityConnectionsModel requestData = new ActivityConnectionsModel
            {
                Id = 519,
                ActivityBlockId = 171,
                PreviousActivityBlockId = 170,
                BorderWidth = 2,
            };

            ActivityConnectionsModel model = new ActivityConnectionsModel
            {
                Id = 519,
                ActivityBlockId = 171,
                PreviousActivityBlockId = 170,
                BorderWidth = 1,
            };

            mockService.Setup(service => service.UpdateActivityConnectionsAsync(requestData))
                .Returns(Task.FromResult(model));

            ActivityConnectionsController controller = new ActivityConnectionsController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.UpdateActivityConnections(requestData).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<ActivityConnectionsModel>((ActivityConnectionsModel)okResult.Value);
        }

        /// <summary>
        /// Unit test case for delete activity connections.
        /// </summary>
        [Fact]
        public void DeleteActivityConnections()
        {
            // Arrange
            Moq.Mock<IActivityConnectionsAppService> mockService = new Moq.Mock<IActivityConnectionsAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            int id = 519;

            mockService.Setup(service => service.DeleteActivityConnectionsAsync(id))
                .Returns(Task.FromResult(true));

            ActivityConnectionsController controller = new ActivityConnectionsController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.DeleteActivityConnections(id).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
        }
    }
}
