using Android.Content;
using Android.OS;
using Android.Print;
using Android.Webkit;
using WebView = Android.Webkit.WebView;


namespace Printing_MAUI.Services
{
    internal static partial class DevicePrintService
    {
        internal static partial void Print(string HTML)
        {
            AndroidPrintClient client = new AndroidPrintClient();
            WebView web = new WebView(Platform.CurrentActivity.ApplicationContext);
            web.Settings.UserAgentString = "Mozilla/5.0 (Android 4.4; Mobile; rv:41.0) Gecko/41.0 Firefox/41.0";
            web.Settings.JavaScriptEnabled = true;
            web.SetWebChromeClient(new WebChromeClient());
            web.SetWebViewClient(client);

            web.LoadDataWithBaseURL("", HTML, "text/html", "utf-8", "");
        }

        private class AndroidPrintClient : WebViewClient
        {
            PrintManager printMgr = (PrintManager)Platform.CurrentActivity.GetSystemService(Context.PrintService);

            public override async void OnPageFinished(WebView view, string url)
            {
                base.OnPageFinished(view, url);

                printMgr.Print("Print", view.CreatePrintDocumentAdapter(), null);
            }
        }
    }
}
