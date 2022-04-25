using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class ActivityPagesAppServiceBuilder
    {
        private Moq.Mock<IActivityPagesAppService> _service = new Moq.Mock<IActivityPagesAppService>();

        public IActivityPagesAppService Build()
        {
            return this._service.Object;
        }

        public ActivityPagesAppServiceBuilder GetAllActivityPagesAsync_Setup(IEnumerable<ActivityPageModel> expectedResult)
        {
            this._service.Setup(x => x.GetAllActivityPagesAsync()).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
