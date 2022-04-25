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
    public class KnowledgeAssetCloneAppServiceBuilder
    {
        private Moq.Mock<IKnowledgeAssetCloneAppService> _service = new Moq.Mock<IKnowledgeAssetCloneAppService>();

        public IKnowledgeAssetCloneAppService Build()
        {
            return this._service.Object;
        }

        public KnowledgeAssetCloneAppServiceBuilder SaveAsStepFlowAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.SaveAsStepFlowAsync(It.IsAny<ProcessMapsSaveAsModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KnowledgeAssetCloneAppServiceBuilder SaveAsStepAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.SaveAsStepAsync(It.IsAny<ProcessMapsSaveAsModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KnowledgeAssetCloneAppServiceBuilder ReviseStepFlowAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.ReviseStepFlowAsync(It.IsAny<ProcessMapsReviseModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KnowledgeAssetCloneAppServiceBuilder ReviseStepAsync_Setup(ProcessMapModel expectedResult)
        {
            this._service.Setup(x => x.ReviseStepAsync(It.IsAny<ProcessMapsReviseModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }

        public KnowledgeAssetCloneAppServiceBuilder IsAbleToReviseAsync_Setup(RevisionCheckingResult expectedResult)
        {
            this._service.Setup(x => x.IsAbleToReviseAsync(It.IsAny<ProcessMapsReviseModel>())).Returns(Task.FromResult(expectedResult));
            return this;
        }
    }
}
