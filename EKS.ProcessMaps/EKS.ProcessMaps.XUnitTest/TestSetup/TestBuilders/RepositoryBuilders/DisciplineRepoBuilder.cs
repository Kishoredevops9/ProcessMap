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
    public class DisciplineRepoBuilder
    {
        private Moq.Mock<IRepository<Discipline>> _repo = new Moq.Mock<IRepository<Discipline>>();

        public IRepository<Discipline> Build()
        {
            return this._repo.Object;
        }

        public DisciplineRepoBuilder GetAsyn_Setup(Discipline expectedResult)
        {
            this._repo.Setup(x => x.GetAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
