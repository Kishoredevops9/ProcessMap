using EKS.ProcessMaps.Business;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestBases;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.RepositoryBuilders;
using EKS.ProcessMaps.XUnitTest.TestSetup.TestDatas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EKS.ProcessMaps.XUnitTest.AppService.Test
{
    public class ActivityConnectionsAppServiceTest : ServiceTestBase
    {
        private ActivityConnectionsRepositoryBuilder _repoBuilder = new ActivityConnectionsRepositoryBuilder();

        [Fact]
        public async Task CreateActivityConnectionsAsyncTest()
        {
            // Arrange
            var expected = TestData.ActivityConnections.FirstOrDefault();
            var repo = this._repoBuilder.AddAsyn_Setup(expected).Build();
            var service = new ActivityConnectionsAppService(repo, this._mapper);

            // Act
            var model = TestData.ActivityConnectionsModels.FirstOrDefault();
            var actual = await service.CreateActivityConnectionsAsync(model);

            // Assert
            Assert.Equal(actual.Id, expected.Id);

        }

        [Fact]
        public async Task DeleteActivityConnectionsAsyncTest()
        {
            // Arrange
            var connections = TestData.ActivityConnections;
            var repo = this._repoBuilder
                .FindBy_Setup(connections)
                .DeleteAsyn_Setup(1)
                .Build();
            var service = new ActivityConnectionsAppService(repo, this._mapper);

            // Act
            var id = 1;
            var result = await service.DeleteActivityConnectionsAsync(id);

            // Assert
            Assert.Equal(result, true);

        }

        [Fact]
        public async Task UpdateActivityConnectionsAsyncTest()
        {
            // Arrange
            var findResult = TestData.ActivityConnections;
            var updateResult = 1;
            var repo = this._repoBuilder
                .UpdateAsyn_Setup(updateResult)
                .FindBy_Setup(findResult)
                .Build();
            var service = new ActivityConnectionsAppService(repo, this._mapper);

            // Act
            var model = TestData.ActivityConnectionsModels.FirstOrDefault();
            var result = await service.UpdateActivityConnectionsAsync(model);

            // Assert
            Assert.Equal(result.Id, findResult.FirstOrDefault()?.Id);

        }
    }
}
