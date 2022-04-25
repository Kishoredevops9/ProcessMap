using AutoMapper;
using EKS.Common.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EKS.ProcessMaps.XUnitTest.TestSetup.TestBases
{
    public class ServiceTestBase
    {
        protected IMapper _mapper;

        public ServiceTestBase()
        {
            this.CreateAutoMaper();
        }

        private void CreateAutoMaper()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });

            this._mapper = mockMapper.CreateMapper();
        }
    }
}
