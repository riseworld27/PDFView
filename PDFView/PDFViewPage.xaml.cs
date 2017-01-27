using Xamarin.Forms;

namespace PDFView
{
	public partial class PDFViewPage : ContentPage
	{
		public PDFViewPage()
		{
			InitializeComponent();

			var pdfUrl = "http://developer.xamarin.com/guides/cross-platform/getting_started/introduction_to_mobile_development/offline.pdf";
			var webView = new MyWebView
			{
				Source = new UrlWebViewSource
				{
					Url = pdfUrl
				},
				VerticalOptions = LayoutOptions.FillAndExpand,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			Content = new StackLayout
			{
				Children =
				{
					webView
				}
			};
		}
	}
}
