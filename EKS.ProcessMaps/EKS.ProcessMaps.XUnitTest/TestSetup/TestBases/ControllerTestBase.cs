using AutoMapper;
using EKS.Common.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBases
{
    public class ControllerTestBase
    {
        protected ILogManager _logManager;

        public ControllerTestBase()
        {
            SetupLogManager();
        }

        private void SetupLogManager()
        {
            var mockLogManager = new Moq.Mock<ILogManager>();

            mockLogManager.Setup(x => x.GetTrackingProperties(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Dictionary<string, string>());

            mockLogManager.Setup(x => x.Error(It.IsAny<string>(), It.IsAny<Exception>(), It.IsAny<Dictionary<string, string>>(), It.IsAny<string>()));

            _logManager = mockLogManager.Object;
        }
    }
}
