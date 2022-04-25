using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class KPacksAppServiceBuilder
    {
        private Moq.Mock<IKPacksAppService> _service = new Moq.Mock<IKPacksAppService>();

        public IKPacksAppService Build()
        {
            return this._service.Object;
        }

        public KPacksAppServiceBuilder CreateKPacksMapAsync_Setup(KPackMapModel expectedResult)
        {
            this._service.Setup(x => x.CreateKPacksMapAsync(It.IsAny<KPackMapModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KPacksAppServiceBuilder AddKPacksMapAsync_Setup(IEnumerable<KPackMapModel> expectedResult)
        {
            this._service.Setup(x => x.AddKPacksMapAsync(It.IsAny<IEnumerable<KPackMapModel>>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KPacksAppServiceBuilder DeleteKPacksMapAsync_Setup(bool expectedResult)
        {
            this._service.Setup(x => x.DeleteKPacksMapAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
        
    }
}
