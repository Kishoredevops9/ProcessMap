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
    public class ActivityBlockTypesAppServiceTest : ServiceTestBase
    {
        private ActivityBlockTypesRepositoryBuilder _repoBuilder = new ActivityBlockTypesRepositoryBuilder();

        [Fact]
        public async Task GetAllActivityBlockTypesAsyncTest()
        {
            // Arrange
            var actBlockTypes = TestData.ActivityBlockTypes;
            var repo = this._repoBuilder.GetAllAsyn_Setup(actBlockTypes).Build();
            var service = new ActivityBlockTypesAppService(repo, this._mapper);

            // Act
            var result = await service.GetAllActivityBlockTypesAsync();

            // Assert
            Assert.Equal(actBlockTypes.First()?.Id, result.First()?.Id);

        }
    }
}
