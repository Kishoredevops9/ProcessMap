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
    public class KnowledgeAssetAppServiceBuilder
    {
        private Moq.Mock<IKnowledgeAssetAppService> _service = new Moq.Mock<IKnowledgeAssetAppService>();

        public IKnowledgeAssetAppService Build()
        {
            return this._service.Object;
        }

        public KnowledgeAssetAppServiceBuilder GetProcessMapByIdOrContentId_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.GetProcessMapByIdOrContentId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KnowledgeAssetAppServiceBuilder GetProcessMapFlowViewByIdOrContentId_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.GetProcessMapFlowViewByIdOrContentId(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
                .Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
