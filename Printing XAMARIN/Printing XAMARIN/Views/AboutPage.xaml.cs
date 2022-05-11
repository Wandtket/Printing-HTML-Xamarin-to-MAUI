using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Printing_XAMARIN.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
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
}