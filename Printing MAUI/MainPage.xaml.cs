using Printing_MAUI.Services;

namespace Printing_MAUI;

public partial class MainPage : ContentPage
{

	public MainPage()
	{
		InitializeComponent();
	}

    private void HTMLbtn_Clicked(object sender, EventArgs e)
    {

        DevicePrintService.Print(WebPages.Test);

    }
}

