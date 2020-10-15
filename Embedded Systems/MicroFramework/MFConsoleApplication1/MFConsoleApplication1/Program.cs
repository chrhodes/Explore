using System;
using System.IO;
using System.Threading;
using Microsoft.SPOT.IO;
using Microsoft.SPOT;

using GHIElectronics.NETMF.USBHost;

namespace MFConsoleApplication1
{
    public class Program
    {
        public static void Main()
        {
            // Subscribe to USBHost events
            USBHostController.DeviceConnectedEvent += USBHostController_DeviceConnectedEvent;
            USBHostController.DeviceDisconnectedEvent += USBHostController_DeviceDisconnectedEvent;
            Debug.Print("Testing USB Host events");
            // Sleep forever
            Thread.Sleep(Timeout.Infinite);
        }

        private static void USBHostController_DeviceConnectedEvent(USBH_Device device)
        {
            Debug.Print("Device Connected");
            Debug.Print("ID: " + device.ID + ", Interface: " + device.INTERFACE_INDEX + ", Type: " + device.TYPE);
            //Debug.Print(string.Format("ID: {0}, Interface: {1}, Type: {2}", device.ID, device.INTERFACE_INDEX, device.TYPE));

        }

        private static void USBHostController_DeviceDisconnectedEvent(USBH_Device device)
        {
            Debug.Print("Device DisConnected");
            Debug.Print("ID: " + device.ID + ", Interface: " + device.INTERFACE_INDEX + ", Type: " + device.TYPE);
        }
    }
}
