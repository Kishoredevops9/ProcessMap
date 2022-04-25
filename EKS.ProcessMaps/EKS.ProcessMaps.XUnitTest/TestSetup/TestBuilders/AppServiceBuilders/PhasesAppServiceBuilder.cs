using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class PhasesAppServiceBuilder
    {
        private Moq.Mock<IPhasesAppService> _service = new Moq.Mock<IPhasesAppService>();

        public IPhasesAppService Build()
        {
            return this._service.Object;
        }

        public PhasesAppServiceBuilder GetPhasesByIdAsync_Setup(PhasesModel expectedResult)
        {
            this._service.Setup(x => x.GetPhasesByIdAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PhasesAppServiceBuilder GetPhasesByProcessMapIdAsync_Setup(IEnumerable<PhasesModel> expectedResult)
        {
            this._service.Setup(x => x.GetPhasesByProcessMapIdAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PhasesAppServiceBuilder CreatePhasesAsync_Setup(PhasesModel expectedResult)
        {
            this._service.Setup(x => x.CreatePhasesAsync(It.IsAny<PhasesModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PhasesAppServiceBuilder Updatephases_Setup(PhasesModel expectedResult)
        {
            this._service.Setup(x => x.UpdatePhasesAsync(It.IsAny<PhasesModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PhasesAppServiceBuilder Deletephases_Setup(bool expectedResult)
        {
            this._service.Setup(x => x.DeletePhasesAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
        

    }
}
