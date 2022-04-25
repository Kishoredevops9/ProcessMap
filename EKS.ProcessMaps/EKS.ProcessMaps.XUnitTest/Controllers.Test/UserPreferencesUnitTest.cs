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
    /// Unit test cases for user preferences.
    /// </summary>
    public class UserPreferencesUnitTest
    {
        /// <summary>
        /// Unit test case for get user preferences by id.
        /// </summary>
        [Fact]
        public void GetUserPreferencesById()
        {
            // Arrange
            Moq.Mock<IUserPreferencesAppService> mockService = new Moq.Mock<IUserPreferencesAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            UserPreferencesModel model = new UserPreferencesModel
            {
                Id = 1,
                UserIdentifier = "pwesw1@pwesw2.onmicrosoft.com",
                Tiles = "[4, 2, 3, 1]",
            };

            string emailID = "pwesw1@pwesw2.onmicrosoft.com";

            mockService.Setup(service => service.GetUserPreferencesByIdAsync(emailID))
                .Returns(Task.FromResult(model));

            UserPreferencesController controller = new UserPreferencesController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.GetUserPreferencesById(emailID).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<UserPreferencesModel>((UserPreferencesModel)okResult.Value);
        }

        /// <summary>
        /// Unit test case for create UserPreferences.
        /// </summary>
        [Fact]
        public void CreateUserPreferences()
        {
            // Arrange
            Moq.Mock<IUserPreferencesAppService> mockService = new Moq.Mock<IUserPreferencesAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            UserPreferencesModel requestData = new UserPreferencesModel
            {
                UserIdentifier = "pwesw1@pwesw2.onmicrosoft.com",
                Tiles = "[4, 2, 3, 1]",
            };

            UserPreferencesModel model = new UserPreferencesModel
            {
                Id = 1,
                UserIdentifier = "pwesw1@pwesw2.onmicrosoft.com",
                Tiles = "[4, 2, 3, 1]",
            };

            mockService.Setup(service => service.CreateUserPreferencesAsync(requestData))
                .Returns(Task.FromResult(model));

            UserPreferencesController controller = new UserPreferencesController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.CreateUserPrefences(requestData).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<UserPreferencesModel>((UserPreferencesModel)okResult.Value);
        }

        /// <summary>
        /// Unit test case for update UserPreferences.
        /// </summary>
        [Fact]
        public void UpdateUserPreferences()
        {
            // Arrange
            Moq.Mock<IUserPreferencesAppService> mockService = new Moq.Mock<IUserPreferencesAppService>();
            Moq.Mock<ILogManager> mockLogManagerService = new Moq.Mock<ILogManager>();

            UserPreferencesModel requestData = new UserPreferencesModel
            {
                Id = 1,
                UserIdentifier = "pwesw1@pwesw2.onmicrosoft.com",
                Tiles = "[4, 3, 2, 1]",
            };

            UserPreferencesModel model = new UserPreferencesModel
            {
                Id = 1,
                UserIdentifier = "pwesw1@pwesw2.onmicrosoft.com",
                Tiles = "[4, 3, 2, 1]",
            };

            mockService.Setup(service => service.UpdateUserPreferencesAsync(requestData))
                .Returns(Task.FromResult(model));

            UserPreferencesController controller = new UserPreferencesController(mockService.Object, mockLogManagerService.Object);

            // Act
            var response = controller.UpdateUserPreferences(requestData).Result;
            var okResult = response as OkObjectResult;

            // Assert
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);

            Assert.IsType<UserPreferencesModel>((UserPreferencesModel)okResult.Value);
        }
    }
}
