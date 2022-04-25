using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class PrivateAssetsAppServiceBuilder
    {
        private Moq.Mock<IPrivateAssetsAppService> _service = new Moq.Mock<IPrivateAssetsAppService>();

        public IPrivateAssetsAppService Build()
        {
            return this._service.Object;
        }

        public PrivateAssetsAppServiceBuilder GetAllPrivateAssetsAsync_Setup(IEnumerable<PrivateAssetsModel> expectedResult)
        {
            this._service.Setup(x => x.GetAllPrivateAssetsAsync()).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PrivateAssetsAppServiceBuilder GetPrivateAssetsByParentContentAssetIdAsync_Setup(IEnumerable<PrivateAssetsModel> expectedResult)
        {
            this._service.Setup(x => x.GetPrivateAssetsByParentContentAssetIdAsync(It.IsAny<int>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PrivateAssetsAppServiceBuilder CreatePrivateAssetsAsync_Setup(PrivateAssetsModel expectedResult)
        {
            this._service.Setup(x => x.CreatePrivateAssetsAsync(It.IsAny<PrivateAssetsModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public PrivateAssetsAppServiceBuilder UpdatePrivateAssetsAsync_Setup(PrivateAssetsModel expectedResult)
        {
            this._service.Setup(x => x.UpdatePrivateAssetsAsync(It.IsAny<PrivateAssetsModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
