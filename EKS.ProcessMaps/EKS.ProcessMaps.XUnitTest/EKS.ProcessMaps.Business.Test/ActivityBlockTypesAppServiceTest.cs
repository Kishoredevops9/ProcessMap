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

namespace EKS.ProcessMaps.XUnitTest.EKS.ProcessMaps.Business.Test
{
    public class ActivityBlockTypesAppServiceTest : ServiceTestBase
    {
        private ActivityBlockTypesRepositoryBuilder _activityBlockTypesRepoBuilder = new ActivityBlockTypesRepositoryBuilder();

        [Fact]
        public async Task GetAllActivityBlockTypesAsyncTest()
        {
            // Arrange
            var activityBlockTypes = TestData.ActivityBlockTypes;
            var repo = this._activityBlockTypesRepoBuilder.GetAllAsyn_Setup(activityBlockTypes).Build();
            var service = new ActivityBlockTypesAppService(repo, this._mapper);

            // Act
            var result = await service.GetAllActivityBlockTypesAsync().ConfigureAwait(false);

            // Assert
            Assert.Equal(activityBlockTypes.First()?.Id, result.First()?.Id);
        }
    }
}
