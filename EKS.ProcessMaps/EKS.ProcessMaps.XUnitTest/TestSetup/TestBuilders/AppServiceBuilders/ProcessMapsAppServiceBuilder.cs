using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class ProcessMapsAppServiceBuilder
    {
        private Moq.Mock<IProcessMapsAppService> _service = new Moq.Mock<IProcessMapsAppService>();

        public IProcessMapsAppService Build()
        {
            return this._service.Object;
        }

        public ProcessMapsAppServiceBuilder GetAllProcessMapsAsync_Setup(IEnumerable<ProcessMapModel> expectedResult)
        {
            this._service.Setup(x => x.GetAllProcessMapsAsync()).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder GetProcessMapByIdAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.GetProcessMapByIdAsync(It.IsAny<long>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder GetProcessMapByContentId_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.GetProcessMapByContentId(It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder GetProcessMapFlowViewByIdAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.GetProcessMapFlowViewByIdAsync(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder CreateProcessMapAsync_Setup(ProcessMapInputOutputModel expectedResult)
        {
            this._service.Setup(x => x.CreateProcessMapAsync(It.IsAny<ProcessMapModel>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder UpdateProcessMapAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.UpdateProcessMapAsync(It.IsAny<ProcessMapModel>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder UpdatePropertiesInProcessMapAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.UpdatePropertiesInProcessMapAsync(It.IsAny<ProcessMapModel>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder UpdateProcessMapStatusAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.UpdateProcessMapStatusAsync(It.IsAny<ProcessMapModel>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public ProcessMapsAppServiceBuilder UpdateProcessMapPurposeAsync_Setup(ProcessMapsPurposeModel expectedResult)
        {
            this._service.Setup(x => x.UpdateProcessMapPurposeAsync(It.IsAny<ProcessMapsPurposeModel>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
