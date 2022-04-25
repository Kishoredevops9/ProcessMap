using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class SwimlaneAppServiceBuilder
    {
        private Moq.Mock<IActivityGroupsAppService> _service = new Moq.Mock<IActivityGroupsAppService>();

        public IActivityGroupsAppService Build()
        {
            return this._service.Object;
        }

        public SwimlaneAppServiceBuilder CreateActivityGroupsAsync_Setup(SwimLanesModel expectedResult)
        {
            this._service.Setup(x => x.CreateActivityGroupsAsync(It.IsAny<SwimLanesModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public SwimlaneAppServiceBuilder UpdateActivityGroupsAsync_Setup(SwimLanesModel expectedResult)
        {
            this._service.Setup(x => x.UpdateActivityGroupsAsync(It.IsAny<SwimLanesModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public SwimlaneAppServiceBuilder UpdateAllActivityGroupsAsync_Setup(List<SwimLanesModel> expectedResult)
        {
            this._service.Setup(x => x.UpdateAllActivityGroupsAsync(It.IsAny<List<SwimLanesModel>>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public SwimlaneAppServiceBuilder DeleteActivityGroupsAsync_Setup(bool expectedResult)
        {
            this._service.Setup(x => x.DeleteActivityGroupsAsync(It.IsAny<long>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public SwimlaneAppServiceBuilder UpdateActivityGroupsSequenceAsync_Setup(IEnumerable<SwimLanesModel> expectedResult)
        {
            this._service.Setup(x => x.UpdateActivityGroupsSequenceAsync(It.IsAny<IEnumerable<SequenceUpdateModel>>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
