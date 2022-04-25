namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Net.Http;
    using System.Threading.Tasks;
    using AutoMapper;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using EKS.Common.Logging;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.DA.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using EksEnum = EKS.ProcessMaps.Helper.Enum;
    using Microsoft.Extensions.Configuration;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;

    /// <summary>
    /// ProcessMapsAppService
    /// </summary>
    public partial class ProcessMapsAppService : IProcessMapsAppService
    {
        private readonly ILogManager _logManager;
        private readonly IProcessMapRepository _processMapsExtendRepo;
        private readonly IRepository<ProcessMap> _processMapsRepo;
        private readonly IMapper _mapper;
        private readonly IRepository<ActivityBlocks> _activitiesRepo;
        private readonly IRepository<SwimLanes> _activityGroupsRepo;
        private readonly IRepository<ActivityConnections> _activityConnectionsRepo;
        private readonly IRepository<Phases> _phasesRepo;
        private readonly IRepository<ContentPhases> _contentPhasesRepo;
        private readonly IRepository<ContentExportCompliances> _contentExportCompliancesRepo;
        private readonly IRepository<ContentTags> _contentTagsRepo;
        private readonly IRepository<ContentInformation> _contentInformationRepo;
        private readonly IRepository<NatureOfChange> natureOfChangeRepo;
        private readonly IRepository<PrivateAssets> _privateAssetsRepo;
        private readonly IRepository<ActivityPage> _activityPageRepo;
        private readonly IRepository<ActivityContainer> activityContainerRepo;
        private readonly IRepository<Discipline> disciplineRepo;
        private readonly IConfiguration _config;
        private readonly IExportExcelAppService _excelService;
        private readonly IProcessMapCommonAppService _commonService;
        private readonly IKnowledgeAssetAppService _kaService;
        private readonly IPublishedCommonAppService _pubCommonService;

        /// <summary>
        /// ProcessMapRepository
        /// </summary>
        /// <param name="processMapsRepo"></param>
        /// <param name="mapper">mapper</param>
        /// <param name="config">mapper</param>
        /// <param name="activitiesRepo"></param>
        /// <param name="activityGroupsRepo"></param>
        /// <param name="processMapMetaRepo"></param>
        /// <param name="activityDocumentsRepo"></param>
        /// <param name="activityMetaRepo"></param>
        /// <param name="activityConnectionsRepo"></param>
        /// <param name="phasesRepo"></param>
        /// <param name="contentPhaseRepo"></param>
        /// <param name="contentExportCompliancesRepo"></param>
        /// <param name="contentTagsRepo"></param>
        /// <param name="disciplineCodeRepo"></param>
        /// <param name="contentInformationRepo"></param>
        /// <param name="contentTypeRepo"></param>
        /// <param name="natureOfChangeRepo"></param>
        /// <param name="privateAssetsRepo"></param>
        /// <param name="phaseRepo"></param>
        /// <param name="tagRepo"></param>
        /// <param name="activityContainerRepo"></param>
        /// <param name="disciplineRepo"></param>
        public ProcessMapsAppService(
            ILogManager logManager,
            IProcessMapRepository processMapsExtendRepo,
            IRepository<ProcessMap> processMapsRepo,
            IMapper mapper,
            IConfiguration config,
            IRepository<ActivityBlocks> activitiesRepo,
            IRepository<SwimLanes> activityGroupsRepo,
            IRepository<ActivityConnections> activityConnectionsRepo,
            IRepository<Phases> phasesRepo,
            IRepository<ContentPhases> contentPhaseRepo,
            IRepository<ContentExportCompliances> contentExportCompliancesRepo,
            IRepository<ContentTags> contentTagsRepo,
            IRepository<ContentInformation> contentInformationRepo,
            IRepository<NatureOfChange> natureOfChangeRepo,
            IRepository<PrivateAssets> privateAssetsRepo,
            IRepository<ActivityPage> activityPageRepo,
            IRepository<ActivityContainer> activityContainerRepo,
            IRepository<Discipline> disciplineRepo,
            IExportExcelAppService excelService,
            IProcessMapCommonAppService commonService,
            IKnowledgeAssetAppService kaService,
            IPublishedCommonAppService pubCommonService)
        {
            this._logManager = logManager;
            this._processMapsExtendRepo = processMapsExtendRepo;
            this._processMapsRepo = processMapsRepo;
            this._mapper = mapper;
            this._config = config;
            this._activitiesRepo = activitiesRepo;
            this._activityGroupsRepo = activityGroupsRepo;
            this._activityConnectionsRepo = activityConnectionsRepo;
            this._phasesRepo = phasesRepo;
            this._contentPhasesRepo = contentPhaseRepo;
            this._contentExportCompliancesRepo = contentExportCompliancesRepo;
            this._contentTagsRepo = contentTagsRepo;
            this._contentInformationRepo = contentInformationRepo;
            this.natureOfChangeRepo = natureOfChangeRepo;
            this._privateAssetsRepo = privateAssetsRepo;
            this._activityPageRepo = activityPageRepo;
            this.activityContainerRepo = activityContainerRepo;
            this.disciplineRepo = disciplineRepo;
            this._excelService = excelService;
            this._commonService = commonService;
            this._kaService = kaService;
            this._pubCommonService = pubCommonService;
        }

        /// <summary>
        /// Get all process map
        /// </summary>
        /// <returns>List of process map</returns>
        public async Task<IEnumerable<ProcessMapModel>> GetAllProcessMapsAsync()
        {
            List<ProcessMapModel> processMapModelResult = new List<ProcessMapModel>();
            List<ProcessMap> processMap = this._processMapsRepo.GetAllIncluding(x => x.ActivityBlocks, x => x.SwimLanes, x => x.ProcessMapMeta, x => x.Phases).ToList();
            var lstProcessMapModel = this._mapper.Map<IEnumerable<ProcessMapModel>>(processMap).ToList();
            
            var activityConnections = await this._activityConnectionsRepo.GetAllAsyn().ConfigureAwait(false);
            var lstactivityConnections = this._mapper.Map<IEnumerable<ActivityConnectionsModel>>(activityConnections).ToList();

            var contentPhases = await this._contentPhasesRepo.GetAllAsyn().ConfigureAwait(false);

            var contentExportCompliances = await this._contentExportCompliancesRepo.GetAllAsyn().ConfigureAwait(false);
            var lstContentExportCompliances = this._mapper.Map<IEnumerable<ContentExportCompliancesModel>>(contentExportCompliances).ToList();

            var contentTags = await this._contentTagsRepo.GetAllAsyn().ConfigureAwait(false);

            foreach (var processMapModel in lstProcessMapModel)
            {
                var data = new ProcessMapModel
                {
                    Id = processMapModel.Id,
                    ContentId = processMapModel.ContentId,
                    Title = processMapModel.Title,
                    DisciplineId = processMapModel.DisciplineId,
                    //SubDisciplineId = processMapModel.SubDisciplineId,
                    //SubSubDisciplineId = processMapModel.SubSubDisciplineId,
                    //SubSubSubDisciplineId = processMapModel.SubSubSubDisciplineId,
                    DisciplineCodeId = processMapModel.DisciplineCodeId,
                    AssetTypeId = processMapModel.AssetTypeId,
                    AssetStatusId = processMapModel.AssetStatusId,
                    ApprovalRequirementId = processMapModel.ApprovalRequirementId,
                    ClassifierId = processMapModel.ClassifierId,
                    ClockId = processMapModel.ClockId,
                    ConfidentialityId = processMapModel.ConfidentialityId,
                    RevisionTypeId = processMapModel.RevisionTypeId,
                    ProgramControlled = processMapModel.ProgramControlled,
                    Outsourceable = processMapModel.Outsourceable,
                    Version = processMapModel.Version,
                    EffectiveFrom = processMapModel.EffectiveFrom,
                    EffectiveTo = processMapModel.EffectiveTo,
                    CreatedDateTime = processMapModel.CreatedDateTime,
                    CreatedUser = processMapModel.CreatedUser,
                    LastUpdateDateTime = processMapModel.LastUpdateDateTime,
                    LastUpdateUser = processMapModel.LastUpdateUser,
                    UsjurisdictionId = processMapModel.UsjurisdictionId,
                    UsclassificationId = processMapModel.UsclassificationId,
                    ClassificationRequestNumber = processMapModel.ClassificationRequestNumber,
                    ClassificationDate = processMapModel.ClassificationDate,
                    Tpmdate = processMapModel.Tpmdate,
                    ExportAuthorityId = processMapModel.ExportAuthorityId,
                    ControllingProgramId = processMapModel.ControllingProgramId,
                    ContentOwnerId = processMapModel.ContentOwnerId,
                    Keywords = processMapModel.Keywords,
                    Author = processMapModel.Author,
                    Confidentiality = processMapModel.Confidentiality,
                    Purpose = processMapModel.Purpose,
                    ProcessInstId = processMapModel.ProcessInstId,
                    CustomId = processMapModel.CustomId,
                    DisciplineCode = processMapModel.DisciplineCode,
                    SwimLanes = processMapModel.SwimLanes,
                    ActivityBlocks = this.GetActivityBlocksByProcessMapId(processMapModel.ActivityBlocks, lstactivityConnections),
                    Phases = processMapModel.Phases,
                    ContentPhase = this.GetContentPhasesByProcessMapId(processMapModel.Id, processMapModel.AssetTypeId, contentPhases.ToList()),
                    // ContentExportCompliances = this.GetContentExportComplianceByProcessMapId(processMapModel.Id, processMapModel.AssetTypeId, lstContentExportCompliances),
                    ContentTag = this.GetContentTagByProcessMapId(processMapModel.Id, processMapModel.AssetTypeId, contentTags.ToList()),
                };
                processMapModelResult.Add(data);
            }

            return await Task.FromResult(processMapModelResult).ConfigureAwait(false);
        }

        public async Task<ProcessMapInputOutputModel> CreateProcessMapAsync(ProcessMapModel processMapModel)
        {
            ProcessMapInputOutputModel resultDataModel = new ProcessMapInputOutputModel();
            string contentId = string.Empty;
            ProcessMap resultData = new ProcessMap();
            try
            {
                processMapModel.AssetStatusId = (int)EksEnum.AssetStatuses.Draft;
                processMapModel.CreatedDateTime = DateTime.Now;
                processMapModel.LastUpdateDateTime = DateTime.Now;
                processMapModel.LastUpdateUser = processMapModel.CreatedUser;
                processMapModel.PrivateInd = false;

                // Added
                if (processMapModel.AssetTypeId == null
                    || processMapModel.AssetTypeId == 0
                    || processMapModel.AssetTypeId == (int)EksEnum.AssetTypes.M)
                {
                    processMapModel.AssetTypeId = (int)EksEnum.AssetTypes.SF;
                }

                resultData = await this._processMapsRepo.AddAsyn(this._mapper.Map<ProcessMap>(processMapModel)).ConfigureAwait(false);
                resultDataModel = this._mapper.Map<ProcessMapInputOutputModel>(resultData);

                processMapModel.Id = resultData.Id;
                resultDataModel.StepFlowId = resultData.Id;

                if (resultDataModel.StepFlowId > 0
                    && processMapModel.SwimLanes?.Count > 0)
                {
                    this._activityGroupsRepo.Add(this._mapper.Map<SwimLanes>(processMapModel.SwimLanes.FirstOrDefault()));
                }

                if (processMapModel.ContentPhase != null)
                {
                    processMapModel.ContentPhase.ForEach(contentPhase =>
                    {
                        var objDataPhase = new ContentPhases();
                        {
                            objDataPhase.ContentId = Convert.ToInt32(resultData.Id);
                            objDataPhase.TypeId = Convert.ToInt32(resultData.AssetTypeId);
                            objDataPhase.PhaseId = Convert.ToInt32(contentPhase);
                            this._contentPhasesRepo.Add(objDataPhase);
                        }
                    });
                }

                if (processMapModel.ContentTag != null)
                {
                    processMapModel.ContentTag.ForEach(contentTag =>
                    {
                        var objData = new ContentTags();
                        {
                            objData.ContentId = Convert.ToInt32(resultData.Id);
                            objData.TypeId = resultData.AssetTypeId ?? 0;
                            objData.TagId = Convert.ToInt32(contentTag);
                            this._contentTagsRepo.Add(objData);
                        }
                    });
                }

                contentId = await this.GenerateContentId(resultData, resultDataModel).ConfigureAwait(false);
                resultDataModel.ContentId = contentId;
                resultDataModel.ContentTag = processMapModel.ContentTag;

                await AddPhase(processMapId: resultData.Id);

                // Saving for NatureOfChange
                NatureOfChange nocData = new NatureOfChange();
                nocData.ContentId = string.IsNullOrEmpty(contentId) ? processMapModel.ContentId : contentId;
                nocData.ContentItemId = Convert.ToInt32(resultData.Id);
                nocData.AssetTypeId = processMapModel.AssetTypeId;
                nocData.Version = resultData.Version;
                nocData.NocdateTime = resultData.CreatedDateTime;
                nocData.Definition = "Initial Version";
                nocData.CreatedDateTime = resultData.CreatedDateTime ?? DateTime.Today;
                nocData.CreatedUser = resultData.CreatedUser;
                NatureOfChange natureOfChange = await this.natureOfChangeRepo.AddAsyn(nocData).ConfigureAwait(false);

            }
            catch (Exception ex)
            {
                ex.Message.ToString();
            }

            return resultDataModel;
        }

        public async Task<ProcessMapModel> UpdateProcessMapAsync(ProcessMapModel processMapModel)
        {
            int resultData = await this._processMapsRepo.UpdateAsyn(this._mapper.Map<ProcessMap>(processMapModel), processMapModel.Id).ConfigureAwait(false);
            Task<ProcessMapModel> processMapData = this.GetProcessMapByIdAsync(processMapModel.Id);
            return await processMapData.ConfigureAwait(false);
        }

        public async Task<ProcessMapsPurposeModel> UpdateProcessMapPurposeAsync(ProcessMapsPurposeModel model)
        {
            var entity = this._processMapsRepo.FindBy(x => x.Id == model.StepFlowId).FirstOrDefault();
            entity.ContentId = model.ContentId;
            entity.Purpose = model.Purpose;

            await this._processMapsRepo.SaveAsync().ConfigureAwait(false);

            return model;
        }

        public async Task<ProcessMapModel> UpdatePropertiesInProcessMapAsync(ProcessMapModel processMapModel)
        {
            try
            {
                ProcessMap resultData = this._processMapsRepo.FindBy(x => x.Id == processMapModel.Id).FirstOrDefault();

                resultData.Title = processMapModel.Title;
                resultData.DisciplineId = processMapModel.DisciplineId;
                resultData.Outsourceable = processMapModel.Outsourceable;
                resultData.LastUpdateDateTime = DateTime.Now;
                resultData.LastUpdateUser = processMapModel.LastUpdateUser;
                resultData.ContentOwnerId = processMapModel.ContentOwnerId;
                resultData.ClassifierId = processMapModel.ClassifierId;
                resultData.ClockId = processMapModel.ClockId;
                resultData.ConfidentialityId = processMapModel.ConfidentialityId;
                resultData.RevisionTypeId = processMapModel.RevisionTypeId;
                resultData.ProgramControlled = processMapModel.ProgramControlled;
                resultData.ContentId = processMapModel.ContentId;
                resultData.Confidentiality = processMapModel.Confidentiality;
                resultData.Author = processMapModel.Author;
                resultData.Keywords = processMapModel.Keywords;
                resultData.ControllingProgramId = processMapModel.ControllingProgramId;
                resultData.ExportAuthorityId = processMapModel.ExportAuthorityId;
                resultData.DisciplineCode = processMapModel.DisciplineCode;
                resultData.PrivateInd = processMapModel.PrivateInd;
                resultData.SourceFileUrl = processMapModel.SourceFileUrl;
                resultData.ExportPdfurl = processMapModel.ExportPdfurl;

                ProcessMap updateData = await this._processMapsRepo.UpdateExAsyn(resultData, processMapModel.Id).ConfigureAwait(false);

                this.DeleteContentPhase(processMapModel.Id);
                this.DeleteContentTag(processMapModel.Id);

                if (processMapModel.ContentPhase != null && processMapModel.ContentPhase.Count > 0)
                {
                    var newPhases = new List<ContentPhases>();
                    processMapModel.ContentPhase.ForEach(contentPhase =>
                    {
                        var objDataPhase = new ContentPhases
                        {
                            ContentId = Convert.ToInt32(resultData.Id),
                            TypeId = Convert.ToInt32(resultData.AssetTypeId),
                            PhaseId = Convert.ToInt32(contentPhase),
                        };
                        newPhases.Add(objDataPhase);
                    });
                    this._contentPhasesRepo.AddRange(newPhases);
                }

                if (processMapModel.ContentTag != null && processMapModel.ContentTag.Count > 0)
                {
                    var newTags = new List<ContentTags>();
                    processMapModel.ContentTag.ForEach(contentTag =>
                    {
                        var objData = new ContentTags
                        {
                            ContentId = Convert.ToInt32(resultData.Id),
                            TypeId = resultData.AssetTypeId ?? 0,
                            TagId = Convert.ToInt32(contentTag),
                        };
                        newTags.Add(objData);
                    });
                    this._contentTagsRepo.AddRange(newTags);
                }
                return this._mapper.Map<ProcessMapModel>(updateData);
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                throw;
            }

        }

        /// <summary>
        /// DeleteProcessMapAsync
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteProcessMapAsync(long id)
        {
            var resultData = await this._processMapsRepo.GetAsync((int)id);

            if (resultData != null)
            {
                int result = await this._processMapsRepo.DeleteAsyn(resultData);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get all process map record on basis of user id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Obsolete("This method is depricated")]
        public async Task<IEnumerable<ProcessMapModel>> GetAllProcessMapsByUserIdAsync(string userId)
        {
            ICollection<ProcessMap> resultData = await this._processMapsRepo.FindAllAsync(x => x.CreatedUser == userId).ConfigureAwait(false);

            return this._mapper.Map<IEnumerable<ProcessMapModel>>(resultData);
        }

        /// <summary>
        /// Get process map by id.
        /// </summary>
        /// <param name="id">process map id</param>
        /// <returns></returns>
        public async Task<ProcessMapModel> GetProcessMapByIdAsync(long id)
        {
            var processMap = this._processMapsRepo.GetAllIncluding(
                x => x.ActivityBlocks, x => x.SwimLanes, x => x.ProcessMapMeta, x => x.Phases).FirstOrDefault(x => x.Id == id);
            
            return await AddProcessMapReferenceData(processMap);
        }

        public async Task<ProcessMapModel> GetProcessMapByContentId(string contentId, int version)
        {
            var processMaps = this._processMapsRepo.GetAllIncluding(x => x.ActivityBlocks, x => x.SwimLanes, x => x.ProcessMapMeta, x => x.Phases)
                .Where(x => x.ContentId == contentId).ToList();

            var processMap = new ProcessMap();
            if (version == 0)
            {
                processMap = processMaps.LastOrDefault();
            }
            else
            {
                processMap = processMaps.FirstOrDefault(x => x.Version == version);
            }
            
            return await AddProcessMapReferenceData(processMap);
        }

        /// <summary>
        /// GetProcessMapFlowViewByIdAsync
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<ProcessMapModel> GetProcessMapFlowViewByIdAsync(long id, string contentId, int version)
        {
            var processMaps = this._processMapsRepo.GetAllIncluding(
                x => x.ActivityBlocks, x => x.SwimLanes, x => x.Phases)
                .Where(x => (id > 0 && x.Id == id) || (version == 0 && x.ContentId == contentId) || (x.Version == version && x.ContentId == contentId))
                .ToList();
            var latestProcessMap = processMaps.LastOrDefault();

            var processMap = await this.AddProcessMapReferenceData(latestProcessMap);

            return this.AddActivityContainerToActivityBlock(processMap);
        }

        /// <summary>
        /// AddActivityContainerToActivityBlock
        /// </summary>
        /// <param name="processMap"></param>
        /// <returns></returns>
        public ProcessMapModel AddActivityContainerToActivityBlock(ProcessMapModel processMap)
        {
            foreach (var ab in processMap.ActivityBlocks)
            {
                if (ab.ActivityTypeId == (int)EksEnum.ActivityBlockTypes.Activity)
                {
                    if (!string.IsNullOrEmpty(ab.AssetContentId))
                    {
                        var content = this.GetActivityPageByContentId(ab.AssetContentId)?.Content;
                        if (content != null)
                        {
                            if (ab.ActivityContainers == null)
                            {
                                ab.ActivityContainers = new List<ActivityContainerModel>();
                            }
                            ab.ActivityContainers.AddRange(content);
                        }
                    }
                }
            }

            return processMap;
        }

        private async Task<ProcessMapModel> AddProcessMapReferenceData(ProcessMap processMap)
        {
            if (processMap == null)
            {
                return null;
            }

            if (processMap.AssetTypeId == (int)EksEnum.AssetTypes.SP)
            { 
                await AddDisciplineToSwimlane(processMap);
            }
            await AddActivityConnectionToActivityBlock(processMap);
            var processMapModel = _mapper.Map<ProcessMapModel>(processMap);
            await this.AddActivityPageId(processMapModel);
            await this.AddContentPhases(processMapModel);
            await this.AddContentTag(processMapModel);

            processMapModel.AssetStatus = await this._commonService.GetAssetStatusAsync(Convert.ToInt32(processMapModel.AssetStatusId)).ConfigureAwait(false);
            return processMapModel;
        }

        private async Task AddActivityConnectionToActivityBlock(ProcessMap processMap)
        {
            foreach(var ab in processMap.ActivityBlocks)
            {
                ab.ActivityConnections = await _activityConnectionsRepo.FindAllAsync(x => x.ActivityBlockId == ab.Id);
            }
        }

        private async Task AddDisciplineToSwimlane(ProcessMap processMap)
        {
            if (processMap == null || processMap.SwimLanes == null)
            {
                return;
            }
            foreach(var sw in processMap.SwimLanes)
            {
                if (sw.DisciplineId != null)
                {
                    sw.Discipline = await disciplineRepo.GetAsync(sw.DisciplineId.Value);
                }
            }
        }

        private async Task AddActivityPageId(ProcessMapModel processMap)
        {
            var activityPages = await this._activityPageRepo.GetAllAsyn().ConfigureAwait(false);
            foreach(var activityBlock in processMap.ActivityBlocks)
            {
                var activityPage = activityPages.FirstOrDefault(x => x.ContentId == activityBlock.AssetContentId);
                activityBlock.ActivityPageId = activityPage?.Id;
            }
        }

        private async Task AddContentPhases(ProcessMapModel processMap)
        {
            var contentPhases = await this._contentPhasesRepo
                .FindAllAsync(x => x.ContentId == processMap.Id && x.TypeId == processMap.AssetTypeId).ConfigureAwait(false);
            processMap.ContentPhase = contentPhases.Select(x => x.PhaseId).ToList();
        }
        private async Task AddContentTag(ProcessMapModel processMap)
        {
            var contentTags = await this._contentTagsRepo
                .FindAllAsync(x => x.ContentId == processMap.Id && x.TypeId == processMap.AssetTypeId).ConfigureAwait(false);
            processMap.ContentTag = contentTags.Select(x => x.TagId).ToList();
        }
        private List<SwimLanesModel> GetSwimLanesByProcessMapId(int processMapId)
        {
            var swimLanesModels = new List<SwimLanesModel>();
            var swimLanes = this._activityGroupsRepo.GetAllIncluding(x => x.Discipline).Where(x => x.ProcessMapId == processMapId).ToList();

            foreach (var entity in swimLanes)
            {
                var model = this._mapper.Map<SwimLanesModel>(entity);

                if (entity.Discipline != null)
                {
                    var discipline = string.Empty;
                    List<string> listDiscipline = new List<string>();

                    listDiscipline.Add(entity.Discipline.Discipline1);
                    listDiscipline.Add(entity.Discipline.Discipline2);

                    if (entity.Discipline.Discipline3 != null)
                    {
                        listDiscipline.Add(entity.Discipline.Discipline3);
                    }

                    if (entity.Discipline.Discipline4 != null)
                    {
                        listDiscipline.Add(entity.Discipline.Discipline4);
                    }

                    model.DisciplineText = string.Join(" > ", listDiscipline);
                }

                swimLanesModels.Add(model);
            }

            return swimLanesModels.OrderBy(x => x.SequenceNumber).ToList();
        }

        /// <summary>
        /// GetContentPhasesByProcessMapId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentTypeId"></param>
        /// <returns></returns>
        private List<int> GetContentPhasesByProcessMapId(long id, int? contentTypeId, List<ContentPhases> contentPhases)
        {
            List<ContentPhases> resultData = contentPhases.Where(x => x.ContentId == id && x.TypeId == contentTypeId).ToList();
            List<int> phase = new List<int>();
            resultData.ForEach(x =>
            {
                //Phase resultData = this.phaseRepo.FindAll(m => m.PhaseId == x.PhaseId).FirstOrDefault();
                //int result = resultData.PhaseId;
                phase.Add(x.PhaseId);
            });
            return phase;
        }

        /// <summary>
        /// GetContentTagByProcessMapId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentTypeId"></param>
        /// <returns></returns>
        private List<int> GetContentTagByProcessMapId(long id, int? contentTypeId, List<ContentTags> contentTags)
        {
            List<ContentTags> resultData = contentTags.Where(x => x.ContentId == Convert.ToInt32(id) && x.TypeId == contentTypeId).ToList();
            List<int> tag = new List<int>();
            resultData.ForEach(x =>
            {
                //Tags resultData = this.tagRepo.FindAll(m => m.TagsId == x.TagId).FirstOrDefault();
                //int result = resultData.TagsId ?? 0;
                tag.Add(x.TagId);
            });
            return tag;
        }

        /// <summary>
        /// DeleteContentPhase
        /// </summary>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        private bool DeleteContentPhase(int processMapId)
        {
            List<ContentPhases> resultData = this._contentPhasesRepo.FindBy(x => x.ContentId == processMapId).ToList();
            if (resultData != null)
            {
                int result = this._contentPhasesRepo.DeleteAll(resultData);
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// DeleteContentTag
        /// </summary>
        /// <param name="processMapId"></param>
        /// <returns></returns>
        private bool DeleteContentTag(int processMapId)
        {
            List<ContentTags> resultData = this._contentTagsRepo.FindBy(x => x.ContentId == processMapId).ToList();
            if (resultData != null)
            {
                int result = this._contentTagsRepo.DeleteAll(resultData);
                return true;
            }
            else
            {
                return false;
            }
        }

        private List<ActivityBlocksModel> GetActivityBlocksByProcessMapId(List<ActivityBlocksModel> lstactivityBlocks, List<ActivityConnectionsModel> lstactivityConnections)
        {
            List<ActivityBlocksModel> result = new List<ActivityBlocksModel>();

            foreach (var activityBlocks in lstactivityBlocks)
            {
                var activityBlockData = new ActivityBlocksModel()
                {
                    Id = activityBlocks.Id,
                    SwimLaneId = activityBlocks.SwimLaneId,
                    ActivityTypeId = activityBlocks.ActivityTypeId,
                    Name = activityBlocks.Name,
                    SequenceNumber = activityBlocks.SequenceNumber,
                    Color = activityBlocks.Color,
                    BorderColor = activityBlocks.BorderColor,
                    BorderStyle = activityBlocks.BorderStyle,
                    BorderWidth = activityBlocks.BorderWidth,
                    Version = activityBlocks.Version,
                    EffectiveFrom = activityBlocks.EffectiveFrom,
                    EffectiveTo = activityBlocks.EffectiveTo,
                    CreatedDateTime = activityBlocks.CreatedDateTime,
                    CreatedUser = activityBlocks.CreatedUser,
                    LastUpdateDateTime = activityBlocks.LastUpdateDateTime,
                    LastUpdateUser = activityBlocks.LastUpdateUser,
                    LocationX = activityBlocks.LocationX,
                    LocationY = activityBlocks.LocationY,
                    AssetContentId = activityBlocks.AssetContentId,
                    ProtectedInd = activityBlocks.ProtectedInd,
                    ProcessMapId = activityBlocks.ProcessMapId,
                    PhaseId = activityBlocks.PhaseId,
                    ActivityConnections = this.GetActivityConnectionsByActivityBlockId(activityBlocks.Id, lstactivityConnections),
                    Length = activityBlocks.Length,
                    Width = activityBlocks.Width,
                };
                result.Add(activityBlockData);
            }

            return result;
        }

        private List<ActivityConnectionsModel> GetActivityConnectionsByActivityBlockId(long id, List<ActivityConnectionsModel> lstactivityConnections)
        {
            List<ActivityConnectionsModel> lstactivityConnection = lstactivityConnections.FindAll(x => x.ActivityBlockId == id);
            return lstactivityConnection;
        }

        private void PrintActivityContainerChilds(List<ProcessMapExcelModel> processMapExcels, List<ActivityContainerModel> acContainers, ICollection<ContentType> contentTypes, string indents, string url)
        {
            foreach (var con in acContainers)
            { 
                var typeCode = contentTypes.FirstOrDefault(x => x.ContentTypeId == con.ContentTypeId)?.Code;
                processMapExcels.Add(new ProcessMapExcelModel 
                { 
                    Type = $"{typeCode}", 
                    Map = $"{indents}{indents}{indents}{indents}{indents}{con.Title}", 
                    ContentId = _excelService.Hyperlink(url, typeCode, con.ContentNo, 1) 
                });
                if (con.ChildList != null && con.ChildList.Count > 0)
                {
                    PrintActivityContainerChilds(processMapExcels, con.ChildList, contentTypes, indents, url);
                }
            }
        }

        private static string BuildDisciplineText(Discipline dis)
        {
            var disciplines = new List<string> { dis.Discipline1};
            if (!string.IsNullOrEmpty(dis.Discipline2))
            {
                disciplines.Add(dis.Discipline2);
            }
            if (!string.IsNullOrEmpty(dis.Discipline3))
            {
                disciplines.Add(dis.Discipline3);
            }
            if (!string.IsNullOrEmpty(dis.Discipline4))
            {
                disciplines.Add(dis.Discipline4);
            }
            var disciplineText = string.Join(" > ", disciplines);
            return disciplineText;
        }

        /// <summary>
        /// ContentId Genreation Generation for Process Maps
        /// </summary>
        /// <param name="resultData"></param>
        /// <param name="resultDataModel"></param>
        /// <returns>string contentId</returns>
        private async Task<string> GenerateContentId(ProcessMap resultData, ProcessMapInputOutputModel resultDataModel)
        {
            var disciplineCodeId = resultDataModel.DisciplineId;// disciplineId needs to go
            var assetTypeId = resultDataModel.AssetTypeId;
            var createdUser = resultDataModel.CreatedUser;
            var processMapId = resultData.Id;
            return await this._commonService.GenerateContentId(processMapId, assetTypeId, createdUser, resultData.PrivateInd);
        }


        /// <summary>
        /// GetProcessMapIdByContentIdAndVersionAsync.
        /// </summary>
        /// <param name="contentId"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public async Task<int?> GetProcessMapIdByContentIdAndVersionAsync(string contentId, int version)
        {
            var processMap = this._processMapsRepo.FindBy(x => x.ContentId == contentId && (version == 0 || x.Version == version))
                .OrderByDescending(x => x.Version)
                .FirstOrDefault();
            var processMapId = processMap?.Id;
            return processMapId;
        }

        /// <summary>
        /// GetActivityPageByContentIdAsync.
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public ActivityPageAllOutputModel GetActivityPageByContentId(string contentId)
        {
            var activityPage = this._activityPageRepo.FindAll(x => x.ContentId == contentId).LastOrDefault();
            if (activityPage == null)
            {
                return null;
            }

            Discipline discipline = this.disciplineRepo.FindBy(d => d.DisciplineId == activityPage.DisciplineId).FirstOrDefault();
            string disciplineName = string.Empty;
            if (discipline != null)
            {
                List<string> listDiscipline = new List<string>();

                listDiscipline.Add(discipline.Discipline1);
                listDiscipline.Add(discipline.Discipline2);

                if (discipline.Discipline3 != null)
                    listDiscipline.Add(discipline.Discipline3);

                if (discipline.Discipline4 != null)
                    listDiscipline.Add(discipline.Discipline4);

                String[] disciplineCollection = listDiscipline.ToArray();
                disciplineName = string.Join(" ---> ", disciplineCollection);
            }

            ActivityPageAllOutputModel data = new ActivityPageAllOutputModel
            {
                Id = activityPage.Id,
                ContentId = activityPage.ContentId,
                Title = activityPage.Title,
                DisciplineId = activityPage.DisciplineId,
                DisciplineCodeId = activityPage.DisciplineCodeId,
                Purpose = activityPage.Purpose,
                AssetTypeId = activityPage.AssetTypeId,
                ProcessInstId = activityPage.ProcessInstId,
                Content = this.GetActivityContainerByActivityPageId(activityPage.Id),
                DisciplineName = disciplineName,
            };

            return data;
        }

        /// <summary>
        /// GetActivityContainerByActivityPageId.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private List<ActivityContainerModel> GetActivityContainerByActivityPageId(long id)
        {
            ICollection<ActivityContainer> resultData = this.activityContainerRepo.FindAll(x => x.ActivityPageId == id);

            List<ActivityContainerModel> objData = this._mapper.Map<List<ActivityContainerModel>>(resultData).ToList();
            List<ActivityContainerModel> parentData = objData.Where(x => x.ParentActivityContainerId == 0).ToList();

            //Bind data as per parent child relationship.
            foreach (var item in parentData)
            {
                item.AssetStatus = this._pubCommonService.GetKnowledgeAssetStatus(item.ContentNo, version: 0);
                item.ChildList = this.GetChildListByParentId(item.ActivityContainerId, objData);
            }

            return parentData;
        }

        /// <summary>
        /// GetChildListByParentId
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objData"></param>
        /// <returns></returns>
        private List<ActivityContainerModel> GetChildListByParentId(long id, List<ActivityContainerModel> objData)
        {
            //Find child activity container.
            List<ActivityContainerModel> activityContainers = objData.FindAll(c => c.ParentActivityContainerId == id);
            List<ActivityContainerModel> childContainerModel = new List<ActivityContainerModel>();

            foreach (var child in activityContainers)
            {
                ActivityContainerModel childActivityContainerModel = new ActivityContainerModel();

                childActivityContainerModel.ActivityContainerId = child.ActivityContainerId;
                childActivityContainerModel.ActivityPageId = child.ActivityPageId;
                childActivityContainerModel.ContentItemId = child.ContentItemId;
                childActivityContainerModel.Title = child.Title;
                childActivityContainerModel.ContentTypeId = child.ContentTypeId;
                childActivityContainerModel.ContentNo = child.ContentNo;
                childActivityContainerModel.US_JC = child.US_JC;
                childActivityContainerModel.Url = child.Url;
                childActivityContainerModel.Version = child.Version;
                childActivityContainerModel.ParentActivityContainerId = child.ParentActivityContainerId;
                childActivityContainerModel.OrderNo = child.OrderNo;
                childActivityContainerModel.CreatedOn = child.CreatedOn;
                childActivityContainerModel.CreatorClockId = child.CreatorClockId;
                childActivityContainerModel.ModifiedOn = child.ModifiedOn;
                childActivityContainerModel.ModifierClockId = child.ModifierClockId;
                childActivityContainerModel.Guidance = child.Guidance;
                childActivityContainerModel.ChildList = GetChildListByParentId(child.ActivityContainerId, objData);
                childActivityContainerModel.AssetStatus = this._pubCommonService.GetKnowledgeAssetStatus(child.ContentNo, version: 0);
                childContainerModel.Add(childActivityContainerModel);
            }

            return childContainerModel;
        }

        public async Task<ProcessMapModel> UpdateProcessMapStatusAsync(ProcessMapModel processMapModel)
        {
            ProcessMap modifiedData = new ProcessMap();
            ProcessMap inputData = this._mapper.Map<ProcessMap>(processMapModel);

            ProcessMap resultData = this._processMapsRepo.FindBy(x => x.Id == inputData.Id).FirstOrDefault();
            if (resultData != null)
            {
                resultData.AssetStatusId = inputData.AssetStatusId;
                modifiedData = await this._processMapsRepo.UpdateExAsyn(resultData, resultData.Id).ConfigureAwait(false);
            }

            return this._mapper.Map<ProcessMapModel>(modifiedData);
        }

        /// <summary>
        /// GetAllStepFlowsAsync.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StepFlowModel>> GetStepFlowByIdOrContentIdAsync(int id, string contentId, int? version)
        {
            ICollection<ProcessMap> allStepFlows = null;
            if (id > 0)
            {
                allStepFlows = await this._processMapsRepo.FindAllAsync(x => x.Id == id).ConfigureAwait(false);
            }
            else if (!string.IsNullOrEmpty(contentId) && !version.HasValue)
            {
                allStepFlows = await this._processMapsRepo.FindAllAsync(x => x.ContentId == contentId).ConfigureAwait(false);
            }
            else if (!string.IsNullOrEmpty(contentId) && version.HasValue)
            {
                allStepFlows = await this._processMapsRepo.FindAllAsync(x => x.ContentId == contentId && x.Version == version).ConfigureAwait(false);
            }

            var processMapModels = this._mapper.Map<IEnumerable<ProcessMapModel>>(allStepFlows);
            var processMapModel = processMapModels.FirstOrDefault();
            var stepFlow = this._mapper.Map<StepFlowModel>(processMapModel);
            stepFlow.AssetStatus = await this._commonService.GetAssetStatusAsync(Convert.ToInt32(stepFlow.AssetStatusId)).ConfigureAwait(false);
            var swimLanes = this.GetSwimLanesByProcessMapId(processMapModel.Id);
            if (swimLanes?.Count > 0)
            {
                var swimLane = swimLanes.FirstOrDefault();
                var sfSwimLane = this._mapper.Map<SFSwimLanesModel>(swimLane);
                
                var privateAssets = this._privateAssetsRepo.FindAll(x => x.ParentContentAssetId == processMapModel.Id);
                if (privateAssets != null && privateAssets.Any()) // Private step
                {
                    foreach (var pvtAsset in privateAssets)
                    {
                        var step = await this.GetStepByIdOrContentIdAsync(pvtAsset.ContentAssetId, string.Empty, 0);
                        sfSwimLane.Steps.AddRange(step);
                    }
                }
                else // Public step
                {
                    var stepActivityBlocks = this._activitiesRepo.FindBy(act => act.SwimLaneId == swimLane.Id 
                        && act.ActivityTypeId == (int)EksEnum.ActivityBlockTypes.Step).ToList();
                    foreach (var stepActivityBlock in stepActivityBlocks)
                    {
                        // version = 0 --> get current step in published db
                        var steps = await this._kaService.GetStepByIdOrContentIdAsync(0, stepActivityBlock.AssetContentId, version ?? 0);
                        foreach (var step in steps)
                        {
                            step.StepActivityBlockId = stepActivityBlock?.Id;
                            step.SequenceNumber = stepActivityBlock?.SequenceNumber;
                        }

                        sfSwimLane.Steps.AddRange(steps);
                    }
                }

                stepFlow.SFSwimLanes.Add(sfSwimLane);
            }

            return new List<StepFlowModel> { stepFlow };
        }
    }
}