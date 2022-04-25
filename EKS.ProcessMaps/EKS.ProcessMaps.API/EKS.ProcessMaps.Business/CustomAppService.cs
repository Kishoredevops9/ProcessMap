namespace EKS.ProcessMaps.Business
{
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using EKSEnum = EKS.ProcessMaps.Helper.Enum;
    /// <summary>
    /// 
    /// </summary>
    public class CustomAppService : ICustomAppService
    {
        private readonly IConfiguration configuration;
        private readonly IRepository<ProcessMap> processMapsRepo;
        private readonly IRepository<ToDoTask> _todotaskRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>       
        /// <param name="processMapsRepo"></param>
        public CustomAppService(IConfiguration configuration, IRepository<ProcessMap> processMapsRepo, IRepository<ToDoTask> todotaskRepository)
        {
            this.configuration = configuration;
            this.processMapsRepo = processMapsRepo;
            _todotaskRepository = todotaskRepository;
        }

        /// <summary>
        /// GetContentData
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public ContentOutputModel GetContentData(int Id, string contentType)
        {
            var contentOutputModel = this.GetAuthoringContentOutputData(Id, contentType);
            this.AddCoAuthor(Id, contentType, contentOutputModel);
            return contentOutputModel;
        }

        /// <summary>
        /// GetAuthoringContentOutputData
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private ContentOutputModel GetAuthoringContentOutputData(int Id, string contentType)
        {

            ProcessMap resultData = this.processMapsRepo.GetAllIncluding(t => t.ControllingProgram).FirstOrDefault(t => t.Id == Id);

            var contentOutputModel = new ContentOutputModel();
            contentOutputModel.ContentOwner = resultData.ContentOwnerId;
            contentOutputModel.Author = resultData.Author;
            contentOutputModel.ExportAuthorityId = resultData.ExportAuthorityId;
            contentOutputModel.ClassifierId = resultData.ClassifierId;
            contentOutputModel.OutSourcable = Convert.ToBoolean(resultData.Outsourceable);
            contentOutputModel.ProgramControlled = Convert.ToBoolean(resultData.ProgramControlled);
            contentOutputModel.ControllingProgramGroup = resultData.ControllingProgram?.GroupName;
            return contentOutputModel;
        }

        /// <summary>
        /// AddCoAuthor
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="contentType"></param>
        /// <param name="contentOutputModel"></param>
        private void AddCoAuthor(int Id, string contentType, ContentOutputModel contentOutputModel)
        {
            int? contentTypeId = GetContentTypeId(contentType);
            var todoTaskData = this._todotaskRepository.FindAll(x => (x.ItemId == Convert.ToInt64(Id)) && (x.ContentTypeId == contentTypeId));
            
            foreach (var item in todoTaskData)
            {
                if (contentOutputModel.Author == string.Empty)
                {
                    contentOutputModel.Author = item.AssignedToUserID;
                }
                else
                {
                    contentOutputModel.Author += "," + item.AssignedToUserID;
                }
            }
        }

        /// <summary>
        /// GetContentTypeId
        /// </summary>
        /// <param name="contentType"></param>
        /// <returns></returns>
        private int? GetContentTypeId(string contentType)
        {
            int? ContentTypeId = 0;
            switch (contentType)
            {
                case "SF":
                    ContentTypeId = (int)EKSEnum.AssetTypes.SF;
                    break;
                case "SP":
                    ContentTypeId = (int)EKSEnum.AssetTypes.SP;
                    break;
            }
            return ContentTypeId;
        }
    }
}

