using Android.Content;
using Android.Print;
using Printing_XAMARIN.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Xamarin.Forms.Dependency(typeof(DroidPrintService))]

namespace Printing_XAMARIN.Droid
{
    public class DroidPrintService : IPrintService
	{

		public DroidPrintService()
		{
		}

		public void Print(WebView viewToPrint)
		{
            
			var droidViewToPrint = Platform.CreateRenderer(viewToPrint).ViewGroup.GetChildAt(0) as Android.Webkit.WebView;

			if (droidViewToPrint != null)
			{
				// Only valid for API 19+
				var version = Android.OS.Build.VERSION.SdkInt;

				if (version >= Android.OS.BuildVersionCodes.Kitkat)
				{
					var printMgr = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);

					printMgr.Print("Print", droidViewToPrint.CreatePrintDocumentAdapter(), null);
				}
			}
		}
	}


}
