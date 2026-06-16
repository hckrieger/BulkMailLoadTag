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
			row.Height = Unit.FromCentimeter(2);

			var field = row.Cells[0];

			var space = 18;
			var size = 16;

			Paragraph p = field.AddParagraph($"{fieldName}:");
			p
			var fieldText = field.AddParagraph($"{fieldName}:");
			
			
			fieldText.Format.Font.Size = size;

			var value = row.Cells[1];
			var valueText = value.AddParagraph(valueName ?? string.Empty);


			valueText.Format.SpaceBefore = Unit.FromCentimeter(1);
			//valueText.Format.SpaceAfter = space;
			valueText.Format.Font.Size = size;
		}

		public PdfProcess(BulkMailTagData? data)
		{
			this.data = data;

			GlobalFontSettings.UseWindowsFontsUnderWindows = true;


			



			var document = new Document();


			//Page Size
			var pageWidth = document.DefaultPageSetup.PageWidth;
			var pageHeight = document.DefaultPageSetup.PageHeight;

			//Margins
			var topMargin = document.DefaultPageSetup.TopMargin;
			var bottomMargin = document.DefaultPageSetup.BottomMargin;

			var leftMargin = document.DefaultPageSetup.LeftMargin;
			var rightMargin = document.DefaultPageSetup.RightMargin;

			//Content size
			var contentX = leftMargin;
			var contentY = topMargin;
			Unit contentWidth = pageWidth - (leftMargin + rightMargin);
			var contentHeight = pageHeight - (bottomMargin + topMargin);


			//Initialize main section to add to document
			//Initialize table to add to main Section 
			var mainSection = document.AddSection();
			


			var table = mainSection.AddTable();


			table.Borders.Visible = true;
			

			//left column for description of fields
			var fieldColumn = table.AddColumn();
			fieldColumn.Format.Alignment = ParagraphAlignment.Right;
			fieldColumn.Width = contentWidth * .45f;


			//Right column for values
			var valueColumn = table.AddColumn();
			valueColumn.Format.Alignment = ParagraphAlignment.Center;
			valueColumn.Width = contentWidth - fieldColumn.Width;




			//Format header
			var headerRow = table.AddRow();
			headerRow.Height = 2;
			var headerArea = headerRow.Cells[0];
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
			commentArea.Format.Alignment = ParagraphAlignment.Left;
			commentArea.MergeRight = 1;

			Unit tableHeight = 0;
			foreach (Row? row in table.Rows)
			{
				tableHeight += row.Height;
			}

			table.Rows.Alignment = RowAlignment.Center;

			Debug.WriteLine($"{pageHeight - tableHeight}");

			mainSection.PageSetup.TopMargin = ((pageHeight - topMargin) - tableHeight) / 2;

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
