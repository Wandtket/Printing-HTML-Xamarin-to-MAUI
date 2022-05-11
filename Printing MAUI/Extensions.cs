using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Printing_MAUI
{
    internal class Extensions
    {
    }

    public interface IPrintService
    {
        void Print(WebView viewToPrint);
    }
}
