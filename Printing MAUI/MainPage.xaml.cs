namespace Printing_MAUI;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private void HTMLbtn_Clicked(object sender, EventArgs e)
    {
        //On some emulators the HTML doesn't load the first time, just back out and click the button again if it doesn't show.

        WebView PrintView = new WebView();
        PrintView.Source = new HtmlWebViewSource
        {
            Html = WebPages.Test
        };
        var printService = DependencyService.Get<IPrintService>();
        printService.Print(PrintView);
    }
}

