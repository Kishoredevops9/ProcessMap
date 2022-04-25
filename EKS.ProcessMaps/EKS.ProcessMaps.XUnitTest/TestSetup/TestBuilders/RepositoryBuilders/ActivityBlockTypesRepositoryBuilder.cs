using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Entities;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.RepositoryBuilders
{
    public class ActivityBlockTypesRepositoryBuilder
    {
        private Moq.Mock<IRepository<ActivityBlockTypes>> _repo = new Moq.Mock<IRepository<ActivityBlockTypes>>();

        public IRepository<ActivityBlockTypes> Build()
        {
            return this._repo.Object;
        }

        public ActivityBlockTypesRepositoryBuilder GetAllAsyn_Setup(ICollection<ActivityBlockTypes> expectedResult)
        {
            this._repo.Setup(x => x.GetAllAsyn()).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
