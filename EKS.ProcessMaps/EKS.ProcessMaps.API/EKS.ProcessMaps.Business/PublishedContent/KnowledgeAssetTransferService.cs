using AutoMapper;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;
using E = EKS.ProcessMaps.Entities;
using EKSEnum = EKS.ProcessMaps.Helper.Enum;
using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Entities.PublishedContent;
using Newtonsoft.Json;
using EKS.ProcessMaps.API.Helper;

namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class KnowledgeAssetTransferService : IKnowledgeAssetTransferService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<E.Classifiers> _classifierRepo;
        private readonly IRepository<E.ContentType> _contentTypeRepo;

        private readonly IPublishContentRepository<PE.UsersCache> _userCacheRepo;
        private readonly IPublishContentRepository<PE.Usclassifications> _classificationRepo;
        private readonly IPublishContentRepository<PE.Disciplines> _disciplineRepo;
        private readonly IPublishContentRepository<PE.KnowledgeAssets> _kaRepo;
        private readonly IPublishContentRepository<PE.ContainerItems> _containerItemsRepo;
        private readonly IPublishContentRepository<PE.ActivityBlocks> _activityBlockRepo;
        private readonly IPublishContentRepository<PE.AssetStatuses> _assetStatusRepo;
        private readonly IKnowledgeAssetRepository _kaCustomRepo;
        private readonly IPublishedCommonAppService _pubCommonService;

        /// <summary>
        /// KnowledgeAssetTransferService
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="classifierRepo"></param>
        /// <param name="userCacheRepo"></param>
        /// <param name="classificationRepo"></param>
        public KnowledgeAssetTransferService(
            IMapper mapper,
            IRepository<E.Classifiers> classifierRepo,
            IRepository<E.ContentType> contentTypeRepo,
            IPublishContentRepository<PE.UsersCache> userCacheRepo,
            IPublishContentRepository<PE.Usclassifications> classificationRepo,
            IPublishContentRepository<PE.Disciplines> disciplineRepo,
            IPublishContentRepository<PE.KnowledgeAssets> kaRepo,
            IPublishContentRepository<PE.ContainerItems> containerItemsRepo,
            IPublishContentRepository<PE.ActivityBlocks> activityBlockRepo,
            IPublishContentRepository<PE.AssetStatuses> assetStatusRepo,
            IKnowledgeAssetRepository kaCustomRepo,
            IPublishedCommonAppService pubCommonService)
        {
            this._mapper = mapper;
            this._classifierRepo = classifierRepo;
            this._contentTypeRepo = contentTypeRepo;
            this._userCacheRepo = userCacheRepo;
            this._classificationRepo = classificationRepo;
            this._disciplineRepo = disciplineRepo;
            this._kaRepo = kaRepo;
            this._containerItemsRepo = containerItemsRepo;
            this._activityBlockRepo = activityBlockRepo;
            this._assetStatusRepo = assetStatusRepo;
            this._kaCustomRepo = kaCustomRepo;
            this._pubCommonService = pubCommonService;
        }

        /// <summary>
        /// TransferKnowledgeAsset_To_StepFlowModel
        /// </summary>
        /// <param name="ka"></param>
        /// <returns></returns>
        public StepFlowModel TransferKnowledgeAsset_To_StepFlowModel(PE.KnowledgeAssets ka)
        {
            var mapper = this.CreateMapping_From_KnowledgeAsset_To_StepFlowModel();
            var stepFlowModel = mapper.Map<StepFlowModel>(ka);
            return stepFlowModel;
        }

        /// <summary>
        /// TransferKnowledgeAssetToProcessMap
        /// </summary>
        /// <param name="ka"></param>
        /// <returns></returns>
        public ProcessMap TransferKnowledgeAsset_To_ProcessMap(PE.KnowledgeAssets ka)
        {
            var mapper = this.CreateMapping_From_KnowledgeAsset_To_ProcessMap();
            var processMap = mapper.Map<ProcessMap>(ka);
            return processMap;
        }

        /// <summary>
        /// TransferKnowledgeAssetToProcessMapModel
        /// </summary>
        /// <param name="ka"></param>
        /// <returns></returns>
        public ProcessMapModel TransferKnowledgeAsset_To_ProcessMapModel(PE.KnowledgeAssets ka)
        {
            var mapper = this.CreateMapping_From_KnowledgeAsset_To_ProcessMapModel();
            var processMapModel = mapper.Map<ProcessMapModel>(ka);

            processMapModel.AssetStatus = _assetStatusRepo.FindBy(x => x.Id == processMapModel.AssetStatusId)?.FirstOrDefault()?.Name;
            return processMapModel;
        }

        /// <summary>
        /// TransferKnowledgeAsset_To_StepModel
        /// </summary>
        /// <param name="step"></param>
        /// <returns></returns>
        public StepModel TransferKnowledgeAsset_To_StepModel(KnowledgeAssets step)
        {
            var activityBlock = this._activityBlockRepo.FindBy(x => x.AssetContentId == step.ContentId)?.FirstOrDefault();
            var stepModel = this._mapper.Map<StepModel>(step);
            stepModel.StepSwimLanes = new List<StepSwimlane>();
            stepModel.BaseType = "P";
            stepModel.StepActivityBlockId = activityBlock?.Id;
            stepModel.SequenceNumber = activityBlock?.SequenceNumber;
            stepModel.AssetStatus = step.AssetStatus?.Name;
            foreach (var stepSwimlane in step.SwimLanes)
            {
                var swimLaneModel = this._mapper.Map<StepSwimlane>(stepSwimlane);
                swimLaneModel.ActivityBlocks = new List<ActivityBlocksModel>();
                swimLaneModel.SwimLaneTitle = swimLaneModel.DisciplineText = this.GetDisciplineText(swimLaneModel.DisciplineId);
                swimLaneModel.BaseType = "SL";

                foreach (var ab in stepSwimlane.ActivityBlocks)
                {
                    var activityBlockModel = this._mapper.Map<ActivityBlocksModel>(ab);
                    activityBlockModel.AssetStatus = this._pubCommonService.GetKnowledgeAssetStatus(activityBlockModel.AssetContentId, ab.Version);
                    activityBlockModel.ActivityContainers = new List<ActivityContainerModel>();
                    if (!string.IsNullOrEmpty(activityBlockModel.AssetContentId))
                    {
                        var containers = this.GetActivityContainerByActivityPageContentIdId(activityBlockModel.AssetContentId);
                        if (containers != null)
                        {
                            activityBlockModel.ActivityContainers.AddRange(containers);
                        }
                    }

                    swimLaneModel.ActivityBlocks.Add(activityBlockModel);
                }

                stepModel.StepSwimLanes.Add(swimLaneModel);
            }

            return stepModel;
        }

        /// <summary>
        /// TransferAssetPartToNatureOfChange
        /// </summary>
        /// <param name="assetParts"></param>
        /// <returns></returns>
        public List<NatureOfChange> TransferAssetPartToNatureOfChange(ICollection<PE.AssetParts> assetParts)
        {
            var result = new List<NatureOfChange>();

            var noc = assetParts?.FirstOrDefault(x =>
                x.AssetPartType.AssetPartTypeCode == EKSEnum.AssetPartTypeCode.NOC.ToString());

            if (noc != null)
            {
                result = JsonConvert.DeserializeObject<List<NatureOfChange>>(noc.Text);
            }

            return result;
        }

        /// <summary>
        /// GetActivityContainerByActivityPageContentIdId
        /// </summary>
        /// <param name="activityPageContentId"></param>
        /// <returns></returns>
        public List<ActivityContainerModel> GetActivityContainerByActivityPageContentIdId(string activityPageContentId)
        {
            var contentTypes = this._contentTypeRepo.GetAll().ToList();
            var activityPage = this._kaRepo.FindBy(x => x.ContentId == activityPageContentId).FirstOrDefault();

            if (activityPage != null)
            {
                var containerItems = this._containerItemsRepo.FindAll(x => x.ContainerKnowledgeAssetId == activityPage.Id);
                var allItems = this._mapper.Map<List<ActivityContainerModel>>(containerItems);
                var parents = allItems.Where(x => x.ParentActivityContainerId == null || x.ParentActivityContainerId == 0).ToList();

                foreach (var parent in parents)
                {
                    var ka = this._kaRepo.GetAllIncluding(x => x.AssetTypeCodeNavigation, y => y.AssetStatus).Where(x => x.ContentId == parent.AssetContentId).FirstOrDefault();
                    parent.ContentTypeId = this.TransferAssetTypeCodeToId(ka?.AssetTypeCodeNavigation?.Code);
                    parent.Title = ka != null ? ka.Title : "Title not available";
                    parent.AssetStatus = ka?.AssetStatus?.Name;
                    parent.ChildList = this.GetChildrenByParentId(parent.ActivityContainerId, allItems, contentTypes);
                }

                return parents;
            }

            return null;
        }

        /// <summary>
        /// CreateMapping_From_KnowledgeAsset_To_ProcessMap
        /// </summary>
        /// <returns></returns>
        private IMapper CreateMapping_From_KnowledgeAsset_To_ProcessMap()
        {
            var author = EKSEnum.RelatedPeople.A.ToString();
            var contentOwner = EKSEnum.RelatedPeople.O.ToString();
            var classifier = EKSEnum.RelatedPeople.C.ToString();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PE.KnowledgeAssets, ProcessMap>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.DisciplineCodeId, opt => opt.Ignore())
                .ForMember(des => des.AssetStatusId, opt => opt.MapFrom(src => src.AssetStatusId))
                .ForMember(des => des.AssetTypeId, opt => opt.MapFrom(src => 
                    src.AssetTypeCode == EKSEnum.PublishedAssetTypeCode.F.ToString()
                    ? (int)EKSEnum.AssetTypes.SF : (int)EKSEnum.AssetTypes.SP))
                .ForMember(des => des.ClassifierId, opt => opt.MapFrom(src => this.GetClassifierId(src.AssetUsers)))
                .ForMember(des => des.ClockId, opt => opt.MapFrom(src => this.GetClassifierClockId(src.AssetUsers)))
                .ForMember(des => des.ContentOwnerId, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, contentOwner)))
                .ForMember(des => des.Author, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, author)))
                .ForMember(des => des.ProgramControlled, opt => opt.MapFrom(src => src.ProgramControlledInd))
                .ForMember(des => des.Outsourceable, opt => opt.MapFrom(src => src.OutsourcableInd))
                .ForMember(des => des.ExportAuthorityId, opt => opt.MapFrom(src => this.GetExportAuthorityId(src)))
                .ForMember(des => des.ControllingProgramId, opt => opt.MapFrom(src => this.GetControllingProgramId(src.AssetControllingPrograms)))
                .ForMember(des => des.Keywords, opt => opt.MapFrom(src => this.GetKeywords(src.AssetKeywords)))
                .ForMember(des => des.Confidentiality, opt => opt.MapFrom(src => src.ConfidentialityId.HasValue))
                .ForMember(des => des.Purpose, opt => opt.MapFrom(src => this.GetPurpose(src.AssetParts)))
                .ForMember(des => des.ProcessInstId, opt => opt.Ignore())
                .ForMember(des => des.CustomId, opt => opt.Ignore())
                .ForMember(des => des.DisciplineCode, opt => opt.MapFrom(src => src.DisciplineCode))
                .ForMember(des => des.PrivateInd, opt => opt.MapFrom(src => src.PrivateInd))
                .ForMember(des => des.SourceFileUrl, opt => opt.MapFrom(src => src.SourceFileUrl))
                .ForMember(des => des.ExportPdfurl, opt => opt.MapFrom(src => src.ExportPdfurl))
                .ForMember(des => des.SwimLanes, opt => opt.Ignore())
                .ForMember(des => des.ActivityBlocks, opt => opt.Ignore())
                .ForMember(des => des.ProcessMapMeta, opt => opt.Ignore())
                .ForMember(des => des.Phases, opt => opt.Ignore())
                .ForMember(des => des.ControllingProgram, opt => opt.Ignore())
                .ForMember(des => des.Discipline, opt => opt.Ignore())
                ;
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        /// <summary>
        /// CreateMapping_From_KnowledgeAsset_To_ProcessMap
        /// </summary>
        /// <returns></returns>
        private IMapper CreateMapping_From_KnowledgeAsset_To_ProcessMapModel()
        {
            var author = EKSEnum.RelatedPeople.A.ToString();
            var contentOwner = EKSEnum.RelatedPeople.O.ToString();
            var classifier = EKSEnum.RelatedPeople.C.ToString();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PE.KnowledgeAssets, ProcessMapModel>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.KnowledgeAssetId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.DisciplineCodeId, opt => opt.Ignore())
                .ForMember(des => des.AssetStatusId, opt => opt.MapFrom(src => src.AssetStatusId))
                .ForMember(des => des.AssetTypeId, opt => opt.MapFrom(src => src.AssetTypeCode == EKSEnum.PublishedAssetTypeCode.F.ToString()
                    ? (int)EKSEnum.AssetTypes.SF : (int)EKSEnum.AssetTypes.SP))
                .ForMember(des => des.ClassifierId, opt => opt.MapFrom(src => this.GetClassifierId(src.AssetUsers)))
                .ForMember(des => des.ClockId, opt => opt.MapFrom(src => this.GetClassifierClockId(src.AssetUsers)))
                .ForMember(des => des.ContentOwnerId, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, contentOwner)))
                .ForMember(des => des.Author, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, author)))
                .ForMember(des => des.ProgramControlled, opt => opt.MapFrom(src => src.ProgramControlledInd))
                .ForMember(des => des.Outsourceable, opt => opt.MapFrom(src => src.OutsourcableInd))
                .ForMember(des => des.ExportAuthorityId, opt => opt.MapFrom(src => this.GetExportAuthorityId(src)))
                .ForMember(des => des.ControllingProgramId, opt => opt.MapFrom(src => this.GetControllingProgramId(src.AssetControllingPrograms)))
                .ForMember(des => des.Keywords, opt => opt.MapFrom(src => this.GetKeywords(src.AssetKeywords)))
                .ForMember(des => des.Confidentiality, opt => opt.MapFrom(src => src.ConfidentialityId.HasValue))
                .ForMember(des => des.Purpose, opt => opt.MapFrom(src => this.GetPurpose(src.AssetParts)))
                .ForMember(des => des.ProcessInstId, opt => opt.Ignore())
                .ForMember(des => des.CustomId, opt => opt.Ignore())
                .ForMember(des => des.DisciplineCode, opt => opt.MapFrom(src => src.DisciplineCode))
                .ForMember(des => des.PrivateInd, opt => opt.MapFrom(src => src.PrivateInd))
                .ForMember(des => des.SourceFileUrl, opt => opt.MapFrom(src => src.SourceFileUrl))
                .ForMember(des => des.ExportPdfurl, opt => opt.MapFrom(src => src.ExportPdfurl))
                .ForMember(des => des.SwimLanes, opt => opt.MapFrom(src => this._mapper.Map<ICollection<SwimLanesModel>>(src.SwimLanes)))
                .ForMember(des => des.ActivityBlocks, opt => opt.MapFrom(src => this._mapper.Map<ICollection<ActivityBlocksModel>>(src.ActivityBlocks)))
                .ForMember(des => des.Phases, opt => opt.MapFrom(src => this._mapper.Map<ICollection<PhasesModel>>(src.PhasesMap)))
                .ForMember(des => des.ContentPhase, opt => opt.MapFrom(src => src.AssetPhases.Select(x => x.PhaseId)))
                .ForMember(des => des.ContentTag, opt => opt.MapFrom(src => src.AssetTags.Select(x => x.TagId)))
                ;
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }

        /// <summary>
        /// CreateMapping_From_KnowledgeAsset_To_StepFlowModel
        /// </summary>
        /// <returns></returns>
        private IMapper CreateMapping_From_KnowledgeAsset_To_StepFlowModel()
        {
            var author = EKSEnum.RelatedPeople.A.ToString();
            var contentOwner = EKSEnum.RelatedPeople.O.ToString();
            var classifier = EKSEnum.RelatedPeople.C.ToString();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<PE.KnowledgeAssets, StepFlowModel>()
                .ForMember(des => des.StepFlowId, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.DisciplineCodeId, opt => opt.Ignore())
                .ForMember(des => des.AssetStatusId, opt => opt.MapFrom(src => src.AssetStatusId))
                .ForMember(des => des.AssetStatus, opt => opt.MapFrom(src => this.GetAssetStatus(src.AssetStatus)))
                .ForMember(des => des.AssetTypeId, opt => opt.MapFrom(src =>
                    src.AssetTypeCode == EKSEnum.PublishedAssetTypeCode.F.ToString()
                    ? (int)EKSEnum.AssetTypes.SF : (int)EKSEnum.AssetTypes.SP))
                .ForMember(des => des.ClassifierId, opt => opt.MapFrom(src => this.GetClassifierId(src.AssetUsers)))
                .ForMember(des => des.ClockId, opt => opt.MapFrom(src => this.GetClassifierClockId(src.AssetUsers)))
                .ForMember(des => des.ContentOwnerId, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, contentOwner)))
                .ForMember(des => des.Author, opt => opt.MapFrom(src => this.GetRelatedPeople(src.AssetUsers, author)))
                .ForMember(des => des.ProgramControlled, opt => opt.MapFrom(src => src.ProgramControlledInd))
                .ForMember(des => des.Outsourceable, opt => opt.MapFrom(src => src.OutsourcableInd))
                .ForMember(des => des.ExportAuthorityId, opt => opt.MapFrom(src => this.GetExportAuthorityId(src)))
                .ForMember(des => des.ControllingProgramId, opt => opt.MapFrom(src => this.GetControllingProgramId(src.AssetControllingPrograms)))
                .ForMember(des => des.Keywords, opt => opt.MapFrom(src => this.GetKeywords(src.AssetKeywords)))
                .ForMember(des => des.Confidentiality, opt => opt.MapFrom(src => src.ConfidentialityId.HasValue))
                .ForMember(des => des.Purpose, opt => opt.MapFrom(src => this.GetPurpose(src.AssetParts)))
                .ForMember(des => des.ProcessInstId, opt => opt.Ignore())
                .ForMember(des => des.CustomId, opt => opt.Ignore())
                .ForMember(des => des.DisciplineCode, opt => opt.MapFrom(src => src.DisciplineCode))
                .ForMember(des => des.PrivateInd, opt => opt.MapFrom(src => src.PrivateInd))
                .ForMember(des => des.SourceFileUrl, opt => opt.MapFrom(src => src.SourceFileUrl))
                .ForMember(des => des.ExportPdfurl, opt => opt.MapFrom(src => src.ExportPdfurl))
                .ForMember(des => des.SFSwimLanes, opt => opt.MapFrom(src => TransferSFSwimLaneModel(src)))
                .ForMember(des => des.ContentPhase, opt => opt.Ignore())
                .ForMember(des => des.ContentExportCompliances, opt => opt.Ignore())
                .ForMember(des => des.ContentTag, opt => opt.Ignore())
                .ForMember(des => des.SFActivityConnection, opt => opt.Ignore())
                ;
            });

            IMapper mapper = config.CreateMapper();
            return mapper;
        }


        /// <summary>
        /// GetClassifierId
        /// </summary>
        /// <param name="users"></param>
        /// <returns></returns>
        private int? GetClassifierId(ICollection<PE.AssetUsers> users)
        {
            if (users == null || users.Count == 0)
            {
                return null;
            }

            var classifierUser = users.FirstOrDefault(x => x.AssetUserRole.AssetUserRoleCode == EKSEnum.RelatedPeople.C.ToString());
            if (classifierUser != null)
            {
                var userCache = this._userCacheRepo.FindBy(x => x.GlobalUid == classifierUser.UserId).FirstOrDefault();
                if (userCache != null)
                {
                    var classifier = this._classifierRepo.FindBy(x => x.Name == userCache.Email).FirstOrDefault();
                    return classifier.ClassifiersId;
                }
            }

            return null;
        }

        private string GetAssetStatus(AssetStatuses assetStatuses)
        {
            return assetStatuses?.Name;
        }

        private string GetClassifierClockId(ICollection<PE.AssetUsers> users)
        {
            if (users == null || users.Count == 0)
            {
                return null;
            }

            var classifierUser = users.FirstOrDefault(x => x.AssetUserRole.AssetUserRoleCode == EKSEnum.RelatedPeople.C.ToString());
            if (classifierUser != null)
            {
                var userCache = this._userCacheRepo.FindBy(x => x.GlobalUid == classifierUser.UserId).FirstOrDefault();
                if (userCache != null)
                {
                    var classifier = this._classifierRepo.FindBy(x => x.Name == userCache.Email).FirstOrDefault();
                    return classifier.ClockId;
                }
            }

            return null;
        }

        /// <summary>
        /// GetRelatedPeople
        /// </summary>
        /// <param name="users"></param>
        /// <param name="userRoleCode"></param>
        /// <returns></returns>
        private string GetRelatedPeople(ICollection<PE.AssetUsers> users, string userRoleCode)
        {
            if (users == null || users.Count == 0)
            {
                return null;
            }

            var classifierUser = users.FirstOrDefault(x => x.AssetUserRole.AssetUserRoleCode == userRoleCode);
            if (classifierUser != null)
            {
                var userCache = this._userCacheRepo.FindBy(x => x.GlobalUid == classifierUser.UserId).FirstOrDefault();
                return userCache?.Email;
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// GetExportAuthorityId
        /// </summary>
        /// <param name="ka"></param>
        /// <returns></returns>
        private int? GetExportAuthorityId(PE.KnowledgeAssets ka)
        {
            if (ka.UsclassificationId == null)
            {
                return null;
            }

            var usc = this._classificationRepo.FindBy(x => x.Id == ka.UsclassificationId).FirstOrDefault();
            return usc?.ExportAuthorityId;
        }

        /// <summary>
        /// GetControllingProgramId
        /// </summary>
        /// <param name="ctrl"></param>
        /// <returns></returns>
        private int? GetControllingProgramId(ICollection<PE.AssetControllingPrograms> ctrl)
        {
            return ctrl?.FirstOrDefault()?.ControllingProgramId;
        }

        /// <summary>
        /// GetPurpose
        /// </summary>
        /// <param name="assetParts"></param>
        /// <returns></returns>
        private string GetPurpose(ICollection<PE.AssetParts> assetParts)
        {
            var purpose = assetParts?.FirstOrDefault(x => 
                x.AssetPartType.AssetPartTypeCode == EKSEnum.AssetPartTypeCode.PP.ToString());
            return purpose?.Text;
        }

        /// <summary>
        /// GetKeywords
        /// </summary>
        /// <param name="assetKeywords"></param>
        /// <returns></returns>
        private string GetKeywords(ICollection<PE.AssetKeywords> assetKeywords)
        {
            var keywords = assetKeywords?.Select(x => x.Keyword.Keyword);
            return string.Join(",", keywords);
        }

        /// <summary>
        /// TransferSFSwimLaneModel
        /// </summary>
        /// <param name="knowledgeAsset"></param>
        /// <returns></returns>
        private List<SFSwimLanesModel> TransferSFSwimLaneModel(PE.KnowledgeAssets knowledgeAsset)
        {
            var swimLane = this._mapper.Map<SFSwimLanesModel>(knowledgeAsset.SwimLanes.FirstOrDefault());
            swimLane.Steps = new List<StepModel>();
            swimLane.DisciplineText = this.GetDisciplineText(swimLane.DisciplineId);

            var stepActivityBlocks = knowledgeAsset.SwimLanes.FirstOrDefault()?.ActivityBlocks
                .Where(x => x.ActivityBlockTypeId == (int)EKSEnum.ActivityBlockTypes.Step);
            foreach (var stepActivityBlock in stepActivityBlocks)
            {
                var step = this._kaCustomRepo.GetKnowledgeAssets_ForStepAsyn(0, stepActivityBlock.AssetContentId, 0);

                StepModel stepModel = TransferKnowledgeAsset_To_StepModel(step);
                swimLane.Steps.Add(stepModel);
            }

            return new List<SFSwimLanesModel> { swimLane };
        }

        /// <summary>
        /// GetDisciplineText
        /// </summary>
        /// <param name="disciplineId"></param>
        /// <returns></returns>
        private string GetDisciplineText(int? disciplineId)
        {
            if (disciplineId.HasValue)
            {
                var discipline = this._disciplineRepo.Get(disciplineId.Value);
                return StringHelper.BuildDisciplineText(discipline);
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// GetChildrenByParentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="allItems"></param>
        /// <returns></returns>
        private List<ActivityContainerModel> GetChildrenByParentId(long parentId, List<ActivityContainerModel> allItems, List<E.ContentType> contentTypes)
        {
            var children = allItems.FindAll(c => c.ParentActivityContainerId == parentId);
            var result = new List<ActivityContainerModel>();

            foreach (var child in children)
            {
                var ka = this._kaRepo.GetAllIncluding(x => x.AssetTypeCodeNavigation, x => x.AssetStatus).Where(x => x.ContentId == child.AssetContentId).FirstOrDefault();
                var newItem = new ActivityContainerModel();
                newItem = this._mapper.Map<ActivityContainerModel>(child);
                newItem.ContentTypeId = this.TransferAssetTypeCodeToId(ka?.AssetTypeCodeNavigation?.Code);
                newItem.Title = ka != null ? ka.Title : "Title not available";
                newItem.AssetStatus = ka?.AssetStatus?.Name;
                newItem.ChildList = GetChildrenByParentId(child.ActivityContainerId, allItems, contentTypes);
                result.Add(newItem);
            }

            return result;
        }

        /// <summary>
        /// TransferAssetTypeCodeToId
        /// </summary>
        /// <param name="assetTypeCode"></param>
        /// <returns></returns>
        private int TransferAssetTypeCodeToId(string assetTypeCode)
        {
            assetTypeCode = assetTypeCode == "TC" ? "TOC" : assetTypeCode;
            var contentTypeId = this._contentTypeRepo.FindBy(x => x.Code == assetTypeCode).FirstOrDefault()?.ContentTypeId;
            return contentTypeId ?? 0;
        }
    }
}
