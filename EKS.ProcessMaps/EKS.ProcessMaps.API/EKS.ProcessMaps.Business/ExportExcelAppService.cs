namespace EKS.ProcessMaps.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using DocumentFormat.OpenXml;
    using DocumentFormat.OpenXml.Packaging;
    using DocumentFormat.OpenXml.Spreadsheet;
    using EKS.ProcessMaps.Business.Interfaces;
    using EKS.ProcessMaps.Entities;
    using EKS.ProcessMaps.Models;
    using Newtonsoft.Json;

    /// <summary>
    /// ProcessMapsAppService
    /// </summary>
    public class ExportExcelAppService : IExportExcelAppService
    {
        public ExportExcelAppService()
        {
        }

        /// <summary>
        /// Hyperlink
        /// </summary>
        /// <param name="universalUrl"></param>
        /// <param name="contentType"></param>
        /// <param name="contentId"></param>
        /// <returns></returns>
        public string Hyperlink(string universalUrl, string contentType, string contentId, int version = 1)
        {
            // Example: https://eswnextgen.azurewebsites.net/view-document/SF/F-015010
            var hyperlink = $@"=HYPERLINK(""{universalUrl}/{contentType}/{contentId}/{version}"", ""{contentId}"")";
            return hyperlink;
        }

        /// <summary>
        /// CreateExcel
        /// </summary>
        /// <param name="stepFlowModels"></param>
        /// <returns></returns>
        public byte[] CreateExcel(List<ProcessMapExcelModel> stepFlowModels, string heading, string header)
        {
            MemoryStream spreadSheetStream = new MemoryStream();

            DataTable table = (DataTable)JsonConvert.DeserializeObject(JsonConvert.SerializeObject(stepFlowModels), typeof(DataTable));
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(spreadSheetStream, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();

                var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
                stylesPart.Stylesheet = new Stylesheet();
                stylesPart.Stylesheet = CreateStylesheet();
                stylesPart.Stylesheet.Save();

                Row topHeadingRow = new Row();
                topHeadingRow.RowIndex = (UInt32)1;

                Cell cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellReference = "A1";
                cell.CellValue = new CellValue(heading);
                cell.StyleIndex = Convert.ToUInt32(4);
                topHeadingRow.AppendChild(cell);
                cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellReference = "B1";
                cell.StyleIndex = Convert.ToUInt32(4);
                topHeadingRow.AppendChild(cell);
                cell = new Cell();
                cell.DataType = CellValues.String;
                cell.CellReference = "C1";
                cell.StyleIndex = Convert.ToUInt32(4);
                topHeadingRow.AppendChild(cell);
                sheetData.AppendChild(topHeadingRow);

                worksheetPart.Worksheet = new Worksheet(sheetData);

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Sheet1" };

                sheets.Append(sheet);
                List<string> columns = new List<string>();

                // create a MergeCells class to hold each MergeCell
                MergeCells mergeCells = new MergeCells();

                // append a MergeCell to the mergeCells for each set of merged cells
                mergeCells.Append(new MergeCell() { Reference = new StringValue("A1:C1") });

                worksheetPart.Worksheet.InsertAfter(mergeCells, worksheetPart.Worksheet.Elements<SheetData>().First());

                Row headerRow = new Row();
                foreach (System.Data.DataColumn column in table.Columns)
                {
                    columns.Add(column.ColumnName);
                    cell = new Cell();
                    cell.DataType = CellValues.String;
                    if (table.Columns.IndexOf(column) == 1)
                        cell.CellValue = new CellValue(header);
                    else
                        cell.CellValue = new CellValue(column.ColumnName);
                    cell.StyleIndex = table.Columns.IndexOf(column) == 0 ?
                        Convert.ToUInt32(3)
                        : Convert.ToUInt32(4);
                    headerRow.AppendChild(cell);
                }

                sheetData.AppendChild(headerRow);
                foreach (DataRow dsrow in table.Rows)
                {
                    Row newRow = new Row();
                    foreach (string col in columns)
                    {
                        Text text1 = new Text
                        {
                            Text = Convert.ToString(dsrow[col]),
                            Space = SpaceProcessingModeValues.Preserve,
                        };
                        cell = new Cell();
                        if (columns.IndexOf(col) == 2 && text1.Text.Contains("=HYPERLINK"))
                        {
                            cell.CellFormula = new CellFormula(text1.Text);
                        }
                        else
                        {
                            cell.InlineString = new InlineString(text1);
                        }
                        cell.DataType = new EnumValue<CellValues>(CellValues.InlineString);
                        uint index = 0;
                        if (Convert.ToString(dsrow[0]) == "M" || Convert.ToString(dsrow[0]) == "SL")
                        {
                            index = columns.IndexOf(col) == 0 ?
                        Convert.ToUInt32(3)
                        : Convert.ToUInt32(4);
                        }
                        else
                        {
                            index = columns.IndexOf(col) == 0 ? Convert.ToUInt32(2)
                                : Convert.ToUInt32(1);
                        }
                        if (columns.IndexOf(col) == 2 && text1.Text.Contains("=HYPERLINK"))
                        {
                            index = 5; // Blue color + underline
                        }
                        cell.StyleIndex = Convert.ToUInt32(index);
                        newRow.AppendChild(cell);
                    }

                    sheetData.AppendChild(newRow);
                }

                workbookPart.Workbook.Save();
            }

            // rewind the memory stream
            spreadSheetStream.Seek(0, SeekOrigin.Begin);
            byte[] spreadsheetByte = spreadSheetStream.ToArray();
            spreadSheetStream.Close();

            return spreadsheetByte;
        }

        private static Stylesheet CreateStylesheet()
        {
            Stylesheet ss = new Stylesheet();

            Font font0 = new Font();         // Default font

            Font font1 = new Font();         // Bold font
            Bold bold = new Bold();
            font1.Append(bold);

            Font font2 = new Font();         // Default font
            Color color2 = new Color { Rgb = "0563c1" };
            Underline underline = new Underline();
            font2.Append(color2);
            font2.Append(underline);

            Fonts fonts = new Fonts();      // <APENDING Fonts>
            fonts.Append(font0);
            fonts.Append(font1);
            fonts.Append(font2);

            // <Fills>
            Fill fill0 = new Fill();        // Default fill

            Fills fills = new Fills();      // <APENDING Fills>
            fills.Append(fill0);

            // <Borders>
            Border border0 = new Border();     // Default border
            Border border1 = new Border();     // Default border

            LeftBorder leftBorder2 = new LeftBorder() { Style = BorderStyleValues.Thin };
            RightBorder rightBorder2 = new RightBorder() { Style = BorderStyleValues.Thin };
            TopBorder topBorder2 = new TopBorder() { Style = BorderStyleValues.Thin };
            BottomBorder bottomBorder2 = new BottomBorder() { Style = BorderStyleValues.Thin };
            DiagonalBorder diagonalBorder2 = new DiagonalBorder();

            border1.Append(leftBorder2);
            border1.Append(rightBorder2);
            border1.Append(topBorder2);
            border1.Append(bottomBorder2);
            border1.Append(diagonalBorder2);

            Borders borders = new Borders();    // <APENDING Borders>
            borders.Append(border0);
            borders.Append(border1);

            Alignment alignment = new Alignment()
            {
                Horizontal = HorizontalAlignmentValues.Center,
                Vertical = VerticalAlignmentValues.Center,
            };

            CellFormat cellformat0 = new CellFormat() { FontId = 0, FillId = 0, BorderId = 0 };
            CellFormat cellformat1 = new CellFormat() { FontId = 0, FillId = 0, BorderId = 1 };
            CellFormat cellformat2 = new CellFormat(alignment) { FontId = 0, FillId = 0, BorderId = 1 }; // Text Center Aligned
            CellFormat cellformat3 = new CellFormat(alignment.CloneNode(false)) { FontId = 1, FillId = 0, BorderId = 1 }; // Text Bold & Center Aligned
            CellFormat cellformat4 = new CellFormat() { FontId = 1, BorderId = 1 }; // Text Bold
            CellFormat cellformat5 = new CellFormat() { FontId = 2, FillId = 0, BorderId = 1 }; // Font Blue color

            CellFormats cellformats = new CellFormats();
            cellformats.Append(cellformat0);
            cellformats.Append(cellformat1);
            cellformats.Append(cellformat2);
            cellformats.Append(cellformat3);
            cellformats.Append(cellformat4);
            cellformats.Append(cellformat5);

            ss.Append(fonts);
            ss.Append(fills);
            ss.Append(borders);
            ss.Append(cellformats);

            return ss;
        }
    }
}