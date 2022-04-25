using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class ProcessMapMetaAppServiceBuilder
    {
        private Moq.Mock<IProcessMapMetaAppService> _service = new Moq.Mock<IProcessMapMetaAppService>();

        public IProcessMapMetaAppService Build()
        {
            return this._service.Object;
        }

        public ProcessMapMetaAppServiceBuilder CreateProcessMapMetaAsync_Setup(ProcessMapMetaModel expectedResult)
        {
            this._service.Setup(x => x.CreateProcessMapMetaAsync(It.IsAny<ProcessMapMetaModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapMetaAppServiceBuilder UpdateProcessMapMetaAsync_Setup(ProcessMapMetaModel expectedResult)
        {
            this._service.Setup(x => x.UpdateProcessMapMetaAsync(It.IsAny<ProcessMapMetaModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapMetaAppServiceBuilder DeleteProcessMapMetaAsync_Setup(bool expectedResult)
        {
            this._service.Setup(x => x.DeleteProcessMapMetaAsync(It.IsAny<long>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
