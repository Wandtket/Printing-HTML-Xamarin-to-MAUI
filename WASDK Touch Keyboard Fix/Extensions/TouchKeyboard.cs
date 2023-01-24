using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace WASDK_Touch_Keyboard_Fix.Extensions
{
    public class TouchKeyboard : DependencyObject
    {
        public static readonly DependencyProperty AllowProperty =
           DependencyProperty.RegisterAttached("Allow", typeof(bool), typeof(TouchKeyboard), new PropertyMetadata("", OnChanged));

        public static bool GetAllow(DependencyObject obj)
        {
            return (bool)obj.GetValue(AllowProperty);
        }

        public static void SetAllow(DependencyObject obj, bool value)
        {
            obj.SetValue(AllowProperty, value);
        }

        private static void OnChanged(DependencyObject d, DependencyPropertyChangedEventArgs args)
        {
            bool? Value = args.NewValue as bool?;

            if (Value == true)
            {
                switch (d)
                {
                    case Control control:
                        control.GotFocus += Control_GotFocus;
                        break;
                }
            }
            else
            {
                switch (d)
                {
                    case Control control:
                        control.GotFocus -= Control_GotFocus;
                        break;
                }
            }
        }

        private static async void Control_GotFocus(object sender, RoutedEventArgs e)
        {
            if (UI.IsTouchEnabled() && UI.PhysicalKeyboardAttached == false)
            {
                var uiHostNoLaunch = new UIHostNoLaunch();
                var tipInvocation = (ITipInvocation)uiHostNoLaunch;

                var inputPane = (IFrameworkInputPane)new FrameworkInputPane();
                inputPane.Location(out var rect);

                if (rect.Height == 0 && rect.Width == 0)
                {
                    tipInvocation.Toggle(GetDesktopWindow());
                    Marshal.ReleaseComObject(uiHostNoLaunch);
                }
            }
        }



        [ComImport, Guid("4ce576fa-83dc-4F88-951c-9d0782b4e376")]
        class UIHostNoLaunch
        {
        }

        [ComImport, Guid("37c994e7-432b-4834-a2f7-dce1f13b834b")]
        [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
        interface ITipInvocation
        {
            void Toggle(IntPtr hwnd);
        }

        [DllImport("user32.dll", SetLastError = false)]
        static extern IntPtr GetDesktopWindow();



        [ComImport, Guid("D5120AA3-46BA-44C5-822D-CA8092C1FC72")]
        public class FrameworkInputPane
        {
        }

        [ComImport, System.Security.SuppressUnmanagedCodeSecurity,
        InterfaceType(ComInterfaceType.InterfaceIsIUnknown),
        Guid("5752238B-24F0-495A-82F1-2FD593056796")]
        public interface IFrameworkInputPane
        {
            [PreserveSig]
            int Advise(
                [MarshalAs(UnmanagedType.IUnknown)] object pWindow,
                [MarshalAs(UnmanagedType.IUnknown)] object pHandler,
                out int pdwCookie
                );

            [PreserveSig]
            int AdviseWithHWND(
                IntPtr hwnd,
                [MarshalAs(UnmanagedType.IUnknown)] object pHandler,
                out int pdwCookie
                );

            [PreserveSig]
            int Unadvise(
                int pdwCookie
                );

            [PreserveSig]
            int Location(
                out Rectangle prcInputPaneScreenLocation
                );
        }
    }

}
