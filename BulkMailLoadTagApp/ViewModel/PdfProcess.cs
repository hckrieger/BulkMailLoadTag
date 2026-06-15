using BulkMailLoadTagApp.Model;
using Microsoft.Win32;
using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.DocumentObjectModel.Visitors;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Quality;
using System.Diagnostics;
using System.IO;

namespace BulkMailLoadTagApp.ViewModel
{
	public class PdfProcess
	{
		private BulkMailTagData? data;

		private void CreateRow(Table table, string fieldName, string valueName)
		{
			var row = table.AddRow();

			var field = row.Cells[0];

			var space = 18;
			var size = 16;
		

			var fieldText = field.AddParagraph($"{fieldName}:");
			fieldText.Format.SpaceBefore = space;
			fieldText.Format.SpaceAfter = space;
			fieldText.Format.Font.Size = size;

			var value = row.Cells[1];
			var valueText = value.AddParagraph(valueName ?? string.Empty);
			valueText.Format.SpaceBefore = space;
			valueText.Format.SpaceAfter = space;
			valueText.Format.Font.Size = size;
		}

		public PdfProcess(BulkMailTagData? data)
		{
			this.data = data;

			GlobalFontSettings.UseWindowsFontsUnderWindows = true;



			var document = new Document();
		

			var mainSection = document.AddSection();
			mainSection.PageSetup.TopMargin = Unit.FromPoint(150);
			//mainSection.PageSetup.BottomMargin = Unit.FromPoint(100);

			var table = mainSection.AddTable();
			table.Borders.Visible = true;
			
			
			var fieldColumn = table.AddColumn();
			fieldColumn.Format.Alignment = ParagraphAlignment.Right;
			int totalWidth = 466;
			fieldColumn.Width = totalWidth * .45f;
			var valueColumn = table.AddColumn();
			valueColumn.Format.Alignment = ParagraphAlignment.Center;
			valueColumn.Width = totalWidth * .55f;

			var headerRow = table.AddRow();
			var headerArea = headerRow.Cells[0];
			headerArea.Format.SpaceAfter = 12;
			var headerText = headerArea.AddParagraph($"Bulk Mail Load Tag");
			headerText.Format.Alignment = ParagraphAlignment.Center;
			headerText.Format.Font.Bold = true;
			headerText.Format.Font.Size = 36;
			headerArea.MergeRight = 1;
	

			//Dispatch row
			CreateRow(table, "Dispatch Number(s)", data.DispatchNumber);

			//Job Number
			CreateRow(table, "Job Number", data.JobNumber);

			CreateRow(table, "Customer Name/Title", data.CustomerNameTitle);

			CreateRow(table, "Version", data.Version);

			CreateRow(table, "Skids", data.SkidQuantity);

			CreateRow(table, "Classification", data.Classification);

			CreateRow(table, "Date", data.Date.ToString());

			CreateRow(table, "Shift/Initials", data.ShiftInitials);

			var commentRow = table.AddRow();
			var commentArea = commentRow.Cells[0];
			commentArea.AddParagraph("Comments:");
			commentArea.AddParagraph($"{data.Comments}");
			commentArea.MergeRight = 1;


			//var paragraph = section.AddParagraph($"{data.JobNumber} \n {data.DispatchNumber} \n {data.CustomerNameTitle}");
			var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
			var outputPath = PdfFileUtility.GetTempPdfFullFileName("bulkLoadTag");
			var fullPath = Path.Combine(baseDirectory, outputPath);

			var tempPath = Path.Combine(baseDirectory, IOUtility.GetTempPath() ?? "");

			//if (Directory.Exists(tempPath))
			//{
			//	Directory.Delete(tempPath, true);
			//}

			var fileCount = Directory.GetFiles(tempPath).Length;

			if (fileCount > 12)
			{
				
			}
		


			var pdfRenderer = new PdfDocumentRenderer
			{
				Document = document,
				PdfDocument =
				{
					PageLayout = PdfPageLayout.SinglePage,
					ViewerPreferences =
					{
						FitWindow = true
					}
				},
				WorkingDirectory = fullPath,
			};



				pdfRenderer.RenderDocument();



			pdfRenderer.PdfDocument.Save(fullPath);

			PdfFileUtility.ShowDocument(fullPath);


		}
	}
	

}
