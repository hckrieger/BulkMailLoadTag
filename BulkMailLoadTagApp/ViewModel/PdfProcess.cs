using BulkMailLoadTagApp.Model;
using MigraDoc.DocumentObjectModel;
using MigraDoc.Rendering;
using PdfSharp.Fonts;
using PdfSharp.Pdf;
using PdfSharp.Quality;

namespace BulkMailLoadTagApp.ViewModel
{
	public class PdfProcess
	{
		private BulkMailTagData? data;
		public PdfProcess(BulkMailTagData? data)
		{
			this.data = data;

			GlobalFontSettings.UseWindowsFontsUnderWindows = true;


			var document = new Document();
			var section = document.AddSection();
			var paragraph = section.AddParagraph($"{data.JobNumber} \n {data.DispatchNumber} \n {data.CustomerNameTitle}");


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
				}
			};

			pdfRenderer.RenderDocument();

			var filename = PdfFileUtility.GetTempPdfFullFileName("bulkLoadTag");
			pdfRenderer.PdfDocument.Save(filename);

			PdfFileUtility.ShowDocument(filename);


		}
	}
	
}
