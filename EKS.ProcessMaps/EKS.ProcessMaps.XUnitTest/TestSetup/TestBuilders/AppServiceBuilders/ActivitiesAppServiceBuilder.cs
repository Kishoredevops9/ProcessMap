using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class ActivitiesAppServiceBuilder
    {
        private Moq.Mock<IActivitiesAppService> _service = new Moq.Mock<IActivitiesAppService>();

        public IActivitiesAppService Build()
        {
            return this._service.Object;
        }

        public ActivitiesAppServiceBuilder CreateActivitiesAsync_Setup(ActivityBlocksModel expectedResult)
        {
            this._service.Setup(x => x.CreateActivitiesAsync(It.IsAny<ActivityBlocksModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
