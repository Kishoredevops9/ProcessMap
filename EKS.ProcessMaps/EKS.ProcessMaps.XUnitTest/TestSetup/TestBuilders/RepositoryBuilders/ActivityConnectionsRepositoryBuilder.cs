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
    public class ActivityConnectionsRepositoryBuilder
    {
        private Moq.Mock<IRepository<ActivityConnections>> _repo = new Moq.Mock<IRepository<ActivityConnections>>();

        public IRepository<ActivityConnections> Build()
        {
            return this._repo.Object;
        }

        public ActivityConnectionsRepositoryBuilder AddAsyn_Setup(ActivityConnections expectedResult)
        {
            this._repo.Setup(x => x.AddAsyn(It.IsAny<ActivityConnections>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ActivityConnectionsRepositoryBuilder FindBy_Setup(ICollection<ActivityConnections> expectedResult)
        {
            this._repo.Setup(x => x.FindBy(It.IsAny<Expression<Func<ActivityConnections, bool>>>()))
                .Returns(expectedResult.AsQueryable());
            return this;
        }

        public ActivityConnectionsRepositoryBuilder DeleteAsyn_Setup(int expectedResult)
        {
            this._repo.Setup(x => x.DeleteAsyn(It.IsAny<ActivityConnections>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ActivityConnectionsRepositoryBuilder UpdateAsyn_Setup(int expectedResult)
        {
            this._repo.Setup(x => x.UpdateAsyn(It.IsAny<ActivityConnections>(), It.IsAny<object>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
