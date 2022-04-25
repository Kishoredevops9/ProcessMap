using AutoMapper;
using EKS.ProcessMaps.API.EKS.ProcessMaps.Business.Interfaces.PublishedContent;
using EKS.ProcessMaps.Business.Interfaces;
using EKS.ProcessMaps.DA.Interfaces;
using EKS.ProcessMaps.Entities.PublishedContent;
using EKS.ProcessMaps.Models;
using EKS.ProcessMaps.Models.PublishedContent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using PE = EKS.ProcessMaps.Entities.PublishedContent;
using PM = EKS.ProcessMaps.Models.PublishedContent;
using EKSEnum = EKS.ProcessMaps.Helper.Enum;
using EKS.ProcessMaps.API.Helper;
namespace EKS.ProcessMaps.API.EKS.ProcessMaps.Business.PublishedContent
{
    public class KnowledgeAssetExportAppService : IKnowledgeAssetExportAppService
    {
        private readonly IMapper _mapper;
        private readonly IPublishContentRepository<AssetTypes> _assetTypeRepo;
        private readonly IPublishContentRepository<KnowledgeAssets> _knowledgeAssetRepo;
        private readonly IPublishContentRepository<PrivateAssets> _privateAssetRepo;
        private readonly IPublishContentRepository<Disciplines> _disciplineRepo;
        private readonly IPublishContentRepository<ActivityBlocks> _activityBlockRepo;
        private readonly IPublishContentRepository<ContainerItems> _containerItemsRepo;
        private readonly IPublishContentRepository<AssetStatements> _assetStatementsRepo;
        private readonly IExportExcelAppService _excelService;
        private string SF = EKSEnum.AssetTypes.SF.ToString();
        private string SP = EKSEnum.AssetTypes.SP.ToString();
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="mapper"></param>
        /// <param name="assetTypeRepo"></param>
        /// <param name="knowledgeAssetRepo"></param>
        /// <param name="privateAssetRepo"></param>
        /// <param name="disciplineRepo"></param>
        /// <param name="activityBlockRepo"></param>
        /// <param name="containerItemsRepo"></param>
        /// <param name="excelService"></param>
        public KnowledgeAssetExportAppService(
            IMapper mapper,
            IPublishContentRepository<AssetTypes> assetTypeRepo,
            IPublishContentRepository<KnowledgeAssets> knowledgeAssetRepo,
            IPublishContentRepository<PrivateAssets> privateAssetRepo,
            IPublishContentRepository<Disciplines> disciplineRepo,
            IPublishContentRepository<ActivityBlocks> activityBlockRepo,
            IPublishContentRepository<ContainerItems> containerItemsRepo,
            IPublishContentRepository<AssetStatements> assetStatementsRepo,
            IExportExcelAppService excelService)
        {
            this._mapper = mapper;
            this._assetTypeRepo = assetTypeRepo;
            this._knowledgeAssetRepo = knowledgeAssetRepo;
            this._privateAssetRepo = privateAssetRepo;
            this._disciplineRepo = disciplineRepo;
            this._activityBlockRepo = activityBlockRepo;
            this._containerItemsRepo = containerItemsRepo;
            this._assetStatementsRepo = assetStatementsRepo;
            this._excelService = excelService;
        }

        /// <summary>
        /// ExportProcessMapToExcel
        /// </summary>
        /// <param name="knowledgeAssetId"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task<byte[]> ExportStepFlowToExcel(int knowledgeAssetId, string url)
        {
            url = url.TrimEnd('/');

            HttpClient client = new HttpClient();
            var processMapExcels = new List<ProcessMapExcelModel>();
            var indents = "  ";
            var contentTypes = await this._assetTypeRepo.GetAllAsyn().ConfigureAwait(false);

            var processMap = await this._knowledgeAssetRepo.GetAsync(knowledgeAssetId);

            var heading = "Tree View StepFlow export of cases and Frames Preliminary Design";
            var header = "StepFlow/Swim Lane";
            await BuildStepFlowData(processMapExcels, url, indents, contentTypes, processMap);
            return this._excelService.CreateExcel(processMapExcels, heading, header);
        }

        public async Task<byte[]> ExportStepToExcel(int knowledgeAssetId, string url)
        {
            url = url.TrimEnd('/');

            HttpClient client = new HttpClient();
            var processMapExcels = new List<ProcessMapExcelModel>();
            var indents = "  ";
            var contentTypes = await this._assetTypeRepo.GetAllAsyn().ConfigureAwait(false);

            var heading = "Tree View STEP export of cases and Frames Preliminary Design";
            var header = "STEP/Discipline";
            var step = this._knowledgeAssetRepo.GetAllIncluding(x => x.SwimLanes).FirstOrDefault(x => x.Id == knowledgeAssetId);
            await BuildStepData(processMapExcels, url, indents, contentTypes, step);
            return this._excelService.CreateExcel(processMapExcels, heading, header);
        }

        private async Task BuildStepFlowData(List<ProcessMapExcelModel> processMapExcels, string url, string indents, ICollection<AssetTypes> contentTypes, KnowledgeAssets stepFlow)
        {
            processMapExcels.Add(new ProcessMapExcelModel 
            {
                Type = SF,
                Map = $"{stepFlow.Title}",
                ContentId = this._excelService.Hyperlink(url, SF, stepFlow.ContentId, stepFlow.Version)
            });

            var activityBlocks = this._activityBlockRepo.FindAll(x => x.KnowledgeAssetId == stepFlow.Id).ToList();
            foreach (var activityBlock in activityBlocks)
            {
                var step = this._knowledgeAssetRepo.GetAllIncluding(x => x.SwimLanes)
                    .FirstOrDefault(x => x.ContentId == activityBlock.AssetContentId && x.AssetStatusId == (int)EKSEnum.PublishedAssetStatus.Current);
                await BuildStepData(processMapExcels, url, indents, contentTypes, step);
            }
        }

        private async Task BuildStepData(List<ProcessMapExcelModel> processMapExcels, string url, string indents, ICollection<AssetTypes> contentTypes, PE.KnowledgeAssets step)
        {
            processMapExcels.Add(new ProcessMapExcelModel
            {
                Type = SP,
                Map = $"{indents}{indents}{step.Title}",
                ContentId = step.PrivateInd ? string.Empty : _excelService.Hyperlink(url, SP, step.ContentId, step.Version)
            });

            foreach (var swimLane in step.SwimLanes)
            {
                if (swimLane.DisciplineId != null)
                {
                    var discipline = await this._disciplineRepo.GetAsync(swimLane.DisciplineId.Value);
                    string disciplineText = StringHelper.BuildDisciplineText(discipline);
                    processMapExcels.Add(new ProcessMapExcelModel { Type = "Discipline", Map = $"{indents}{indents}{indents}{disciplineText}" });
                }

                var activityBlocks = await this._activityBlockRepo.FindAllAsync(x => x.SwimLaneId == swimLane.Id);
                foreach (var activityBlock in activityBlocks)
                {
                    var activityPage = this._knowledgeAssetRepo.FindBy(x => x.ContentId == activityBlock.AssetContentId 
                        && x.Version == activityBlock.Version).FirstOrDefault();

                    processMapExcels.Add(new ProcessMapExcelModel
                    {
                        Type = "ActivityBlock",
                        Map = $"{indents}{indents}{indents}{indents}{activityBlock.Caption}",
                        ContentId = _excelService.Hyperlink(url, EKSEnum.AssetTypes.AP.ToString(), activityBlock.AssetContentId, activityBlock.Version)
                    });

                    if (activityPage != null)
                    {
                        var acContainers = this.GetActivityContainerByActivityPageId(activityPage.Id);
                        if (acContainers != null && acContainers.Count > 0)
                        {
                            this.PrintActivityContainerChilds(processMapExcels, acContainers, contentTypes, indents, url);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// GetActivityContainerByActivityPageId
        /// </summary>
        /// <param name="activityPageId"></param>
        /// <returns></returns>
        private List<ContainerItemsExportModel> GetActivityContainerByActivityPageId(int activityPageId)
        {
            var containerItems = this._containerItemsRepo.FindAll(x => x.ContainerKnowledgeAssetId == activityPageId);

            var allItems = this._mapper.Map<List<ContainerItemsExportModel>>(containerItems);
            var parents = allItems.Where(x => x.ParentContainerItemId == null || x.ParentContainerItemId == 0).ToList();
            foreach (var parent in parents)
            {
                parent.Children = this.GetChildrenByParentId(parent.Id, allItems);

            }

            return parents;
        }

        /// <summary>
        /// GetChildrenByParentId
        /// </summary>
        /// <param name="parentId"></param>
        /// <param name="allItems"></param>
        /// <returns></returns>
        private List<ContainerItemsExportModel> GetChildrenByParentId(long parentId, List<ContainerItemsExportModel> allItems)
        {
            var childrens = allItems.FindAll(c => c.ParentContainerItemId == parentId);
            var result = new List<ContainerItemsExportModel>();

            foreach (var child in childrens)
            {
                var newItem = new ContainerItemsExportModel();
                newItem = this._mapper.Map<ContainerItemsExportModel>(child);
                newItem.Children = this.GetChildrenByParentId(child.Id, allItems);
                result.Add(newItem);
            }

            return result;
        }

        /// <summary>
        /// PrintActivityContainerChilds
        /// </summary>
        /// <param name="processMapExcels"></param>
        /// <param name="acContainers"></param>
        /// <param name="contentTypes"></param>
        /// <param name="indents"></param>
        /// <param name="url"></param>
        private void PrintActivityContainerChilds(List<ProcessMapExcelModel> processMapExcels, List<ContainerItemsExportModel> acContainers, ICollection<AssetTypes> contentTypes, string indents, string url)
        {
            foreach (var con in acContainers)
            {
                var knowledgeAsset = this._knowledgeAssetRepo.FindBy(x => x.ContentId == con.AssetContentId && x.AssetStatusId == 1)
                    .OrderByDescending(x => x.Version).FirstOrDefault();
                if (knowledgeAsset != null)
                {
                    var typeCode = contentTypes.FirstOrDefault(x => x.AssetTypeCode == knowledgeAsset.AssetTypeCode)?.Code;
                    processMapExcels.Add(new ProcessMapExcelModel
                    {
                        Type = $"{typeCode}",
                        Map = $"{indents}{indents}{indents}{indents}{indents}{knowledgeAsset.Title}",
                        ContentId = this._excelService.Hyperlink(url, typeCode, knowledgeAsset.ContentId, knowledgeAsset.Version)
                    });

                    //AssetStetement
                    //var statement = this._assetStatementsRepo.FindBy(x => x.KnowledgeAssetId == knowledgeAsset.Id
                    //    && (x.AssetStatementTypeCode == "B" || x.AssetStatementTypeCode == "C")).FirstOrDefault();
                    //if (statement != null && !string.IsNullOrEmpty(statement.Statement.Trim()))
                    //{
                    //    processMapExcels.Add(new ProcessMapExcelModel
                    //    {
                    //        Type = $"Criteria statement",
                    //        Map = $"{indents}{indents}{indents}{indents}{indents}{statement.Statement}",
                    //        ContentId = string.Empty
                    //    });
                    //}
                }

                if (con.Children != null && con.Children.Count > 0)
                {
                    this.PrintActivityContainerChilds(processMapExcels, con.Children, contentTypes, indents, url);
                }
            }
        }
    }
}
