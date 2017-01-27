using System;
using System.IO;
using System.Net;
using Android.Webkit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PDFView;
using PDFView.Droid;

[assembly: ExportRenderer(typeof(MyWebView), typeof(MyWebViewRender))]
namespace PDFView.Droid
{
	public class MyWebViewRender : WebViewRenderer
	{
		private string _documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
		private string _pdfPath;
		private string _pdfFileName = "thePDFDocument.pdf";
		private string _pdfFilePath;
		private WebClient _webClient = new WebClient();

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.WebView> e)
		{
			base.OnElementChanged(e);

			var settings = Control.Settings;
			settings.JavaScriptEnabled = true;
			settings.AllowFileAccessFromFileURLs = true;
			settings.AllowUniversalAccessFromFileURLs = true;
			settings.BuiltInZoomControls = true;
			Control.SetWebChromeClient(new WebChromeClient());

			DownloadPDFDocument();
		}

		private void DownloadPDFDocument()
		{
			_pdfPath = _documentsPath + "/PDFView";
			_pdfFilePath = Path.Combine(_pdfPath, _pdfFileName);

			// Check if the PDFDirectory Exists
			if (!Directory.Exists(_pdfPath))
			{
				Directory.CreateDirectory(_pdfPath);
			}
			else {
				// Check if the pdf is there, If Yes Delete It. Because we will download the fresh one just in a moment
				if (File.Exists(_pdfFilePath))
				{
					File.Delete(_pdfFilePath);
				}
			}

			// This will be executed when the pdf download is completed
			_webClient.DownloadDataCompleted += OnPDFDownloadCompleted;
			// Lets downlaod the PDF Document
			var url = new Uri(Control.Url);
			_webClient.DownloadDataAsync(url);
		}

		private void OnPDFDownloadCompleted(object sender, DownloadDataCompletedEventArgs e)
		{
			try
			{
				// Okay the download's done, Lets now save the data and reload the webview.
				var pdfBytes = e.Result;
				File.WriteAllBytes(_pdfFilePath, pdfBytes);

				//if (File.Exists(_pdfFilePath))
				//{
				//	var bytes = File.ReadAllBytes(_pdfFilePath);
				//}

				Control.LoadUrl("file:///android_asset/pdfviewer/index.html?file=" + _pdfFilePath);
			}
			catch (Exception ep)
			{
				Console.WriteLine(ep);
			}

		}
	}
}
