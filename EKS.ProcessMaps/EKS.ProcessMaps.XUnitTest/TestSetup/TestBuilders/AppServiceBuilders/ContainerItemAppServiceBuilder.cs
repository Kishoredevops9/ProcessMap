using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBuilders.AppServiceBuilders
{
    public class ContainerItemAppServiceBuilder
    {
        private Moq.Mock<IContainerItemAppService> _service = new Moq.Mock<IContainerItemAppService>();

        public IContainerItemAppService Build()
        {
            return this._service.Object;
        }
    }
}
