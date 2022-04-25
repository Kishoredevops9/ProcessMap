using EKS.ProcessMaps.Business;
using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Entities;
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
    public class ActivityGroupsAppServiceTest : ServiceTestBase
    {
        private ActivityGroupsRepoBuilder activityGroupsRepoBuilder = new ActivityGroupsRepoBuilder();
        private DisciplineRepoBuilder disciplineRepoBuilder = new DisciplineRepoBuilder();

        private IRepository<SwimLanes> activityGroupsRepo;
        private IRepository<Discipline> disciplineRepo;

        public ActivityGroupsAppServiceTest()
        {
            this.activityGroupsRepo = this.activityGroupsRepoBuilder.Build();
            this.disciplineRepo = this.disciplineRepoBuilder.Build();
        }

        [Fact]
        public async Task CreateActivityGroupsAsyncTest()
        {
            // Arrange
            var findExpectedSwimlanes = TestData.SwimLanes;
            var addExpectedSwimlane = TestData.SwimLanes.FirstOrDefault();
            this.activityGroupsRepo = this.activityGroupsRepoBuilder
                .FindBy_Setup(findExpectedSwimlanes)
                .AddAsyn_Setup(addExpectedSwimlane)
                .Build();

            var expectedDiscipline = TestData.Disciplines.FirstOrDefault();
            this.disciplineRepo = this.disciplineRepoBuilder.GetAsyn_Setup(expectedDiscipline).Build();

            var service = new ActivityGroupsAppService(this._mapper, this.activityGroupsRepo, this.disciplineRepo);

            // Act
            var model = TestData.SwimLanesModels.FirstOrDefault();
            var actual = await service.CreateActivityGroupsAsync(model).ConfigureAwait(false);

            // Assert
            Assert.Equal(actual.Id, addExpectedSwimlane.Id);

        }

        [Fact]
        public async Task UpdateActivityGroupsAsyncTest()
        {
            // Arrange
            var findExpectedSwimlanes = TestData.SwimLanes;
            var expectedUpdatedId = 1;
            this.activityGroupsRepo = this.activityGroupsRepoBuilder
                .FindBy_Setup(findExpectedSwimlanes)
                .UpdateAsyn_Setup(expectedUpdatedId)
                .Build();

            var service = new ActivityGroupsAppService(this._mapper, this.activityGroupsRepo, this.disciplineRepo);

            // Act
            var model = TestData.SwimLanesModels.FirstOrDefault();
            var actual = await service.UpdateActivityGroupsAsync(model).ConfigureAwait(false);

            // Assert
            Assert.Equal(actual.Id, expectedUpdatedId);

        }

        [Fact]
        public async Task DeleteActivityGroupsAsyncTest()
        {
            // Arrange
            var findExpectedSwimlanes = TestData.SwimLanes;
            var expectedDeletedId = 1;
            this.activityGroupsRepo = this.activityGroupsRepoBuilder
                .FindBy_Setup(findExpectedSwimlanes)
                .DeleteAsyn_Setup(expectedDeletedId)
                .Build();

            var service = new ActivityGroupsAppService(this._mapper, this.activityGroupsRepo, this.disciplineRepo);

            // Act
            var swimlaneId = 1;
            var actual = await service.DeleteActivityGroupsAsync(swimlaneId).ConfigureAwait(false);

            // Assert
            Assert.True(actual);

        }

        [Fact]
        public async Task UpdateAllActivityGroupsAsyncTest()
        {
            // Arrange
            var findExpectedSwimlanes = TestData.SwimLanes;
            var expectedUpdatedId = 1;
            this.activityGroupsRepo = this.activityGroupsRepoBuilder
                .FindBy_Setup(findExpectedSwimlanes)
                .UpdateAsyn_Setup(expectedUpdatedId)
                .Build();

            var service = new ActivityGroupsAppService(this._mapper, this.activityGroupsRepo, this.disciplineRepo);

            // Act
            var models = TestData.SwimLanesModels;
            var actual = await service.UpdateAllActivityGroupsAsync(models).ConfigureAwait(false);

            // Assert
            Assert.Equal(actual.Count, 1);
            Assert.Equal(actual.FirstOrDefault().Id, expectedUpdatedId);

        }

        [Fact]
        public async Task UpdateActivityGroupsSequenceAsyncTest()
        {
            // Arrange
            var getExpectedSwimlane = TestData.SwimLanes.FirstOrDefault();
            this.activityGroupsRepo = this.activityGroupsRepoBuilder
                .Get_Setup(getExpectedSwimlane)
                .SaveAsync_Setup()
                .Build();

            var service = new ActivityGroupsAppService(this._mapper, this.activityGroupsRepo, this.disciplineRepo);

            // Act
            var updateModels = TestData.SequenceUpdateModels;
            var actual = await service.UpdateActivityGroupsSequenceAsync(updateModels).ConfigureAwait(false);

            // Assert
            Assert.Equal(actual.FirstOrDefault().SequenceNumber, getExpectedSwimlane.SequenceNumber);
        }

    }
}
