using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.RepositoryBuilders
{
    public class ActivityGroupsRepoBuilder
    {
        private Moq.Mock<IRepository<SwimLanes>> _repo = new Moq.Mock<IRepository<SwimLanes>>();

        public IRepository<SwimLanes> Build()
        {
            return this._repo.Object;
        }

        public ActivityGroupsRepoBuilder AddAsyn_Setup(SwimLanes expectedResult)
        {
            this._repo.Setup(x => x.AddAsyn(It.IsAny<SwimLanes>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ActivityGroupsRepoBuilder UpdateAsyn_Setup(int expectedResult)
        {
            this._repo.Setup(x => x.UpdateAsyn(It.IsAny<SwimLanes>(), It.IsAny<object>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ActivityGroupsRepoBuilder DeleteAsyn_Setup(int expectedResult)
        {
            this._repo.Setup(x => x.DeleteAsyn(It.IsAny<SwimLanes>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }
        public ActivityGroupsRepoBuilder SaveAsync_Setup()
        {
            this._repo.Setup(x => x.SaveAsync());
            return this;
        }



        public ActivityGroupsRepoBuilder FindBy_Setup(ICollection<SwimLanes> expectedResult)
        {
            this._repo.Setup(x => x.FindBy(It.IsAny<Expression<Func<SwimLanes, bool>>>()))
                .Returns(expectedResult.AsQueryable());
            return this;
        }

        public ActivityGroupsRepoBuilder Get_Setup(SwimLanes expectedResult)
        {
            this._repo.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(expectedResult);
            return this;
        }
    }
}
