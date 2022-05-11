using Xamarin.Forms;
using Printing_XAMARIN;


namespace Printing_XAMARIN
{

    internal class Extensions
    {

    }

    public interface IPrintService
    {
        void Print(WebView viewToPrint);
    }
}
