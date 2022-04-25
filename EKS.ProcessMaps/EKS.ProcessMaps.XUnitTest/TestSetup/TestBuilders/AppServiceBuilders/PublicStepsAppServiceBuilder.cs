using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class PublicStepsAppServiceBuilder
    {
        private Moq.Mock<IPublicStepsAppService> _service = new Moq.Mock<IPublicStepsAppService>();

        public IPublicStepsAppService Build()
        {
            return this._service.Object;
        }

        public PublicStepsAppServiceBuilder CreateProcessMapMetaAsync_Setup(PublicStepsInputOutputModel expectedResult)
        {
            this._service.Setup(x => x.CreatePublicStepsAsync(It.IsAny<ProcessMapModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PublicStepsAppServiceBuilder UpdatePublicStepsPurposeAsync_Setup(PublicStepsPurposeModel expectedResult)
        {
            this._service.Setup(x => x.UpdatePublicStepsPurposeAsync(It.IsAny<PublicStepsPurposeModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
