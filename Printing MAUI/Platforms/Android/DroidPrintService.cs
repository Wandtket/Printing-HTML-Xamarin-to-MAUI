using Android.Content;
using Android.Print;
using Printing_MAUI.Platforms.Droid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[assembly: Dependency(typeof(DroidPrintService))]

namespace Printing_MAUI.Platforms.Droid
{

        public class DroidPrintService : IPrintService
        {

            public DroidPrintService()
            {
            }

            public void Print(WebView viewToPrint)
            {
                //This is where it differes and I'm having a hard time figuring out how to convert it to a handler.
                var droidViewToPrint = Platform.CreateRenderer(viewToPrint).ViewGroup.GetChildAt(0) as Android.Webkit.WebView;

                if (droidViewToPrint != null)
                {
                    
                    // Only valid for API 19+
                    var version = Android.OS.Build.VERSION.SdkInt;

                    if (version >= Android.OS.BuildVersionCodes.Kitkat)
                    {
                        var printMgr = (PrintManager)Platform.AppContext.GetSystemService(Context.PrintService);

                    printMgr.Print("Print", droidViewToPrint.CreatePrintDocumentAdapter(), null);
                    }
                }
            }
        }


}
