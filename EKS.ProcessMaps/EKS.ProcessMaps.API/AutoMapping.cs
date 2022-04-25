using AutoMapper;
using EKS.ProcessMaps.Entities;
using EKS.ProcessMaps.Models;
using PE = EKS.ProcessMaps.Entities.PublishedContent;
using PM = EKS.ProcessMaps.Models.PublishedContent;
using E = EKS.ProcessMaps.Entities;
using M = EKS.ProcessMaps.Models;

/// <summary>
/// AutoMapping Class for map
/// </summary>
public class AutoMapping : Profile
{
    /// <summary>
    /// Constructor - AutoMapping to map all entity and model
    /// </summary>
    public AutoMapping()
    {
        // Mapping for ProcessMap
        CreateMap<ProcessMap, ProcessMapModel>();
        CreateMap<ProcessMapModel, ProcessMap>();

        // Mapping for create process map
        CreateMap<ProcessMap, ProcessMapInputOutputModel>();
        CreateMap<ProcessMapInputOutputModel, ProcessMap>();

        // Mapping for create process map model
        CreateMap<ProcessMapModel, ProcessMapInputOutputModel>();
        CreateMap<ProcessMapInputOutputModel, ProcessMapModel>();

        // Mapping for create process map model
        CreateMap<ContentAssetTypeModel, ProcessMapInputOutputModel>();
        CreateMap<ProcessMapInputOutputModel, ContentAssetTypeModel>();

        // Mapping for create public steps
        CreateMap<ProcessMap, PublicStepsInputOutputModel>();
        CreateMap<PublicStepsInputOutputModel, ProcessMap>();

        // Mapping for updating purpose in public steps
        CreateMap<ProcessMap, PublicStepsPurposeModel>();
        CreateMap<PublicStepsPurposeModel, ProcessMap>();

        // Mapping for create process map from existing process map
        CreateMap<ProcessMap, ProcessMapExistingModel>();
        CreateMap<ProcessMapExistingModel, ProcessMap>();

        // Mapping for ActivityBlocks
        CreateMap<ActivityBlocks, ActivityBlocksModel>();
        CreateMap<ActivityBlocksModel, ActivityBlocks>();

        // Mapping for SwimLanes
        CreateMap<SwimLanes, SwimLanesModel>()
            .ForMember(e => e.DisciplineText, m => m.MapFrom(u => (
            u.Discipline != null
            ? u.Discipline.Discipline4 ?? u.Discipline.Discipline3 ?? u.Discipline.Discipline2 ?? u.Discipline.Discipline1 ?? string.Empty
            : string.Empty)));
        CreateMap<SwimLanesModel, SwimLanes>();

        // Mapping for Swimlanes which use for existing process map
        CreateMap<SwimLanes, SwimLanesInputOutputModel>();
        CreateMap<SwimLanesInputOutputModel, SwimLanes>();

        // Mapping for ProcessMapMeta
        CreateMap<ProcessMapMeta, ProcessMapMetaModel>();
        CreateMap<ProcessMapMetaModel, ProcessMapMeta>();

        // Mapping for ActivityConnections
        CreateMap<ActivityConnections, ActivityConnectionsModel>();
        CreateMap<ActivityConnectionsModel, ActivityConnections>();

        // Mapping for UserPreferences
        CreateMap<UserPreferences, UserPreferencesModel>();
        CreateMap<UserPreferencesModel, UserPreferences>();

        // Mapping for Activity Block Types
        CreateMap<ActivityBlockTypes, ActivityBlockTypesModel>();
        CreateMap<ActivityBlockTypesModel, ActivityBlockTypes>();

        // Mapping for Phases
        CreateMap<Phases, PhasesModel>();
        CreateMap<PhasesModel, Phases>();

        // Mapping for Content Phases
        CreateMap<ContentPhases, ContentPhasesModel>();
        CreateMap<ContentPhasesModel, ContentPhases>();

        // Mapping for Content Export Compliances
        CreateMap<ContentExportCompliances, ContentExportCompliancesModel>();
        CreateMap<ContentExportCompliancesModel, ContentExportCompliances>();

        // Mapping for Content Tags
        CreateMap<ContentTags, ContentTagsModel>();
        CreateMap<ContentTagsModel, ContentTags>();

        // Mapping for Content Information
        CreateMap<ContentInformation, ContentInformationModel>();
        CreateMap<ContentInformationModel, ContentInformation>();

        // Mapping for Activity Pages
        CreateMap<ActivityPage, ActivityPageModel>();
        CreateMap<ActivityPageModel, ActivityPage>();

        // Mapping for Master Discipline Code
        CreateMap<MasterDisciplineCode, MasterDisciplineCodeModel>();
        CreateMap<MasterDisciplineCodeModel, MasterDisciplineCode>();

        // Mapping for Content Type
        CreateMap<ContentType, ContentTypeModel>();
        CreateMap<ContentTypeModel, ContentType>();

        CreateMap<PrivateAssets, PrivateAssetsModel>();
        CreateMap<PrivateAssetsModel, PrivateAssets>();

        CreateMap<KpacksMap, KPackMapModel>();
        CreateMap<KPackMapModel, KpacksMap>();
        CreateMap<KpacksMap, KPackMapExtendModel>();

        // Mapping for StepFlowModel
        CreateMap<ProcessMapModel, StepFlowModel>()
            .ForMember(des => des.StepFlowId, opt => opt.MapFrom(src => src.Id));
        CreateMap<StepFlowModel, ProcessMapModel>();

        // Mapping for StepFlowModel
        CreateMap<ProcessMapModel, StepModel>();
        CreateMap<StepModel, ProcessMapModel>();

        // Mapping for SFSwimLanesModel
        CreateMap<SwimLanesModel, SFSwimLanesModel>()
            .ForMember(des => des.SwimLaneId, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.StepFlowId, opt => opt.MapFrom(src => src.ProcessMapId));
        CreateMap<SFSwimLanesModel, SwimLanesModel>();

        // Mapping for ActivityContainer
        CreateMap<ActivityContainer, ActivityContainerModel>();
        CreateMap<ActivityContainerModel, ActivityContainer>();

        CreateMap<KnowledgeAssetModel, KnowledgeAsset>();
        CreateMap<KnowledgeAsset, KnowledgeAssetModel>();

        // Mapping for Asset Status
        CreateMap<AssetStatus, AssetStatusModel>();
        CreateMap<AssetStatusModel, AssetStatus>();

        AutoMappingPublishedContent();
    }

    private void AutoMappingPublishedContent()
    {
        CreateMap<PM.KnowledgeAssetModel, PE.KnowledgeAssets>();
        CreateMap<PE.KnowledgeAssets, PM.KnowledgeAssetModel>();
        CreateMap<PE.KnowledgeAssets, M.StepModel>()
            .ForMember(des => des.StepId, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.StepContentId, opt => opt.MapFrom(src => src.ContentId))
            .ForMember(des => des.StepTitle, opt => opt.MapFrom(src => src.Title));

        CreateMap<PE.SwimLanes, SwimLanesModel>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(src => src.KnowledgeAssetId))
            .ForMember(e => e.DisciplineText, m => m.MapFrom(u => (u.Discipline != null
                ? u.Discipline.Discipline4 ?? u.Discipline.Discipline3 ?? u.Discipline.Discipline2 ?? u.Discipline.Discipline1 ?? string.Empty
                : string.Empty)));

        CreateMap<PE.SwimLanes, SFSwimLanesModel>()
            .ForMember(des => des.SwimLaneId, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.StepFlowId, opt => opt.MapFrom(src => src.KnowledgeAssetId))
            .ForMember(des => des.DisciplineText, opt => opt.Ignore());

        CreateMap<PE.SwimLanes, SwimLanes>()
            .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(src => src.KnowledgeAssetId));

        CreateMap<PE.SwimLanes, StepSwimlane>()
            .ForMember(des => des.SwimLaneId, opt => opt.MapFrom(src => src.Id))
            .ForMember(des => des.StepId, opt => opt.MapFrom(src => src.KnowledgeAssetId));

        CreateMap<PE.ActivityBlocks, ActivityBlocksModel>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ActivityTypeId, opt => opt.MapFrom(x => x.ActivityBlockTypeId))
            .ForMember(des => des.Name, opt => opt.MapFrom(x => x.Caption))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(x => x.KnowledgeAssetId))
            .ForMember(des => des.ActivityPageId, opt => opt.Ignore())
            .ForMember(des => des.ActivityConnections, opt => opt.MapFrom(src => src.ConnectorsChildActivityBlock));

        CreateMap<PE.ActivityBlocks, E.ActivityBlocks>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ActivityTypeId, opt => opt.MapFrom(x => x.ActivityBlockTypeId))
            .ForMember(des => des.Name, opt => opt.MapFrom(x => x.Caption))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(x => x.KnowledgeAssetId))
            .ForMember(des => des.ActivityConnections, opt => opt.MapFrom(src => src.ConnectorsChildActivityBlock));

        CreateMap<PE.PhasesMap, PhasesModel>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(x => x.KnowledgeAssetId));

        CreateMap<PE.PhasesMap, E.Phases>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ProcessMapId, opt => opt.MapFrom(x => x.KnowledgeAssetId));

        CreateMap<PE.AssetPhases, E.ContentPhases>()
            .ForMember(des => des.ContentPhaseId, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ContentId, opt => opt.MapFrom(x => x.KnowledgeAssetId));

        CreateMap<PE.AssetTags, E.ContentTags>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ContentId, opt => opt.MapFrom(x => x.KnowledgeAssetId));

        CreateMap<PE.Connectors, ActivityConnectionsModel>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ActivityBlockId, opt => opt.MapFrom(x => x.ChildActivityBlockId))
            .ForMember(des => des.PreviousActivityBlockId, opt => opt.MapFrom(x => x.ParentActivityBlockId));

        CreateMap<PE.Connectors, E.ActivityConnections>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ActivityBlockId, opt => opt.MapFrom(x => x.ChildActivityBlockId))
            .ForMember(des => des.PreviousActivityBlockId, opt => opt.MapFrom(x => x.ParentActivityBlockId));

        CreateMap<PE.ContainerItems, KPackMapExtendModel>()
            .ForMember(des => des.KPacksMapId, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ContentAssetId, opt => opt.MapFrom(x => x.AssetContentId));

        CreateMap<PE.ContainerItems, E.KpacksMap>()
            .ForMember(des => des.KpacksMapId, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ContentAssetId, opt => opt.MapFrom(x => x.AssetContentId));

        CreateMap<PE.ContainerItems, PM.ContainerItemsExportModel>();

        CreateMap<PE.ContainerItems, M.ActivityContainerModel>()
            .ForMember(des => des.ActivityContainerId, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ParentActivityContainerId, opt => opt.MapFrom(x => x.ParentContainerItemId))
            .ForMember(des => des.ActivityPageId, opt => opt.MapFrom(x => x.ContainerKnowledgeAssetId))
            .ForMember(des => des.ContentNo, opt => opt.MapFrom(x => x.AssetContentId))
            .ForMember(des => des.OrderNo, opt => opt.MapFrom(x => x.Index))
            .ForMember(des => des.Version, opt => opt.MapFrom(x => x.Version.ToString()))
            .ForMember(des => des.Guidance, opt => opt.MapFrom(x => x.Text));

        CreateMap<M.ActivityContainerModel, M.ActivityContainerModel>();

        CreateMap<PM.ContainerItemsExportModel, PM.ContainerItemsExportModel>();

        CreateMap<PE.AssetExportCompliances, E.ContentExportCompliances>()
            .ForMember(des => des.Id, opt => opt.MapFrom(x => x.Id))
            .ForMember(des => des.ContentId, opt => opt.MapFrom(x => x.KnowledgeAssetId));

        CreateMap<PE.PrivateAssets, E.PrivateAssets>()
            .ForMember(des => des.ContentAssetId, opt => opt.MapFrom(x => x.KnowledgeAssetId))
            .ForMember(des => des.ParentContentAssetId, opt => opt.MapFrom(x => x.ParentAssetId));
    }
}
