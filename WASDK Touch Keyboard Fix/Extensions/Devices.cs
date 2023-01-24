using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Enumeration;

namespace WASDK_Touch_Keyboard_Fix.Extensions
{

    public static class Devices
    {

        public static string CurrentDeviceId = "";


        /// <summary>
        /// Event Handler for detecting devices
        /// </summary>
        public static event StatusUpdatedEventHandler StatusUpdated;
        public delegate void StatusUpdatedEventHandler(DeviceType Type, bool IsConnected);


        /// <summary>
        /// Device Watcher to detect a user plugging in USB / Connected Wireless Devices
        /// </summary>
        public static DeviceWatcher watcher = DeviceInformation.CreateWatcher(DeviceClass.All);

        /// <summary>
        /// Detects hardware changes.
        /// </summary>
        public static void Monitor()
        {
            watcher.Updated += Watcher_Updated;
            watcher.Start();
        }


        /// <summary>
        /// Determine if physical keyboard is present on tablet device.
        /// </summary>
        /// <returns></returns>
        public static async Task<bool> IsPhysicalKeyboardAttachedToTablet()
        {
            List<SupportedDevice> supportedDevices = ListSupportedDevices();

            foreach (SupportedDevice supportedDevice in supportedDevices)
            {
                if (supportedDevice.DeviceType == DeviceType.Keyboard)
                {
                    var collection = await DeviceInformation.FindAllAsync();
                    foreach (var device in collection)
                    {
                        if (device.Id == supportedDevice.DeviceId)
                        {
                            var Keys = device.Properties.Keys.ToList();
                            var Values = device.Properties.Values.ToList();

                            for (int i = 0; i < Keys.Count; i++)
                            {
                                if (Keys[i] != null)
                                {
                                    if (Keys[i].ToString() == "System.Devices.InterfaceEnabled")
                                    {
                                        bool Attached = Convert.ToBoolean(Values[i].ToString());
                                        UI.PhysicalKeyboardAttached = Attached;
                                        return Attached;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return false;
        }


        /// <summary>
        /// When a Human Interface Device (HID) is connected this event will fire, use the "System.Devices.InterfaceEnabled" Key to determine connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private static async void Watcher_Updated(DeviceWatcher sender, DeviceInformationUpdate args)
        {
            watcher.Stop();

            CurrentDeviceId = args.Id;

            var Device = await DeviceInformation.CreateFromIdAsync(args.Id);

            List<SupportedDevice> supportedDevices = ListSupportedDevices();

            foreach (SupportedDevice supporteddevice in supportedDevices)
            {
                if (Device.Id == supporteddevice.DeviceId)
                {
                    var Keys = Device.Properties.Keys.ToList();
                    var Values = Device.Properties.Values.ToList();

                    for (int i = 0; i < Keys.Count; i++)
                    {
                        if (Keys[i] != null)
                        {
                            if (Keys[i].ToString() == "System.Devices.InterfaceEnabled")
                            {
                                bool Connected = Convert.ToBoolean(Values[i].ToString());
                                StatusUpdated.Invoke(supporteddevice.DeviceType, Connected);
                                break;
                            }
                        }
                    }
                }
            }

            await Task.Delay(100);
            watcher.Start();
        }



        /// <summary>
        /// Add device properties here you wish to detect. Use the CurrentDeviceId string to get information about devices (Device Manager isn't reliable to compare strings)
        /// </summary>
        /// <returns></returns>
        public static List<SupportedDevice> ListSupportedDevices()
        {
            List<SupportedDevice> supportedDevices = new List<SupportedDevice>
            {
                //Asus Touch Keyboard
                new SupportedDevice { DeviceType = DeviceType.Keyboard, DeviceId = @"\\?\USB#VID_0B05&PID_0603#DK_8231_001#{a5dcbf10-6530-11d2-901f-00c04fb951ed}" },

                //Surface Touch Keyboard
                new SupportedDevice { DeviceType = DeviceType.Keyboard, DeviceId = @"Keyboard Id Goes here. Use CurrentDeviceId String to get keyboard Id." },
            };

            return supportedDevices;
        }
    }


    /// <summary>
    /// Add device types here to determine what to do with StatusUpdated event handler (Can be generic or specific)
    /// </summary>
    public enum DeviceType
    {
        Keyboard,
        Mouse,
        FlashDrive,
    }

    /// <summary>
    /// Class to save devices that you want to detect when a user plugs in a USB or connects a wireless device.
    /// </summary>
    public class SupportedDevice
    {
        public DeviceType DeviceType { get; set; }

        public string DeviceId { get; set; }
    }
}
