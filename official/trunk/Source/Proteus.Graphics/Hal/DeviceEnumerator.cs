using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;
using Swf = System.Windows.Forms;

namespace Proteus.Graphics.Hal
{
    public class DeviceEnumerator
    {
        private static Kernel.Diagnostics.Log<DeviceEnumerator> log =
            new Kernel.Diagnostics.Log<DeviceEnumerator>();

        private static int GetMultisampleMode()
        {
            int             adapter     = Kernel.Registry.Manager.Instance.GetValue("Graphics.Adapter",0);
            D3d.DeviceType  deviceType  = Kernel.Registry.Manager.Instance.GetValue("Graphics.DeviceType",D3d.DeviceType.Hardware );
            bool            windowed    = Kernel.Registry.Manager.Instance.GetValue("Graphics.Windowed",true );

            //D3d.Manager.CheckDeviceMultiSampleType(
            return 0;
        }

        public static D3d.PresentParameters CreatePresentParameters(    Swf.Control window,
                                                                        bool windowed,
                                                                        int width,
                                                                        int height,
                                                                        int freq )
        {
            D3d.PresentParameters pp = new D3d.PresentParameters();

            pp.AutoDepthStencilFormat = D3d.DepthFormat.D24S8;
            pp.BackBufferCount = 1;
            pp.BackBufferFormat = D3d.Format.A8R8G8B8;
            pp.Windowed = windowed;
            pp.DeviceWindow = window;
            pp.EnableAutoDepthStencil = true;
            pp.ForceNoMultiThreadedFlag = false;
            pp.FullScreenRefreshRateInHz = freq;
            pp.MultiSample = (D3d.MultiSampleType)GetMultisampleMode();

            if (windowed)
            {
            }
            else
            {
            }
          
            
            
            return pp;

        }

        public static void EnumerateAdapters()
        {
            log.BeginRegion("Display adapters");

            for (int i = 0; i < D3d.Manager.Adapters.Count; i++)
            {
                log.BeginRegion("Adapter");
                
                log.BeginMessage(Proteus.Kernel.Diagnostics.LogLevel.Info);
                D3d.AdapterInformation ai = D3d.Manager.Adapters[i];

                log.MessageContent("Adapter Ordinal: {0}", ai.Adapter);
                log.MessageContent("Current display mode: {0}", ai.CurrentDisplayMode);
                log.MessageContent("Description: {0}", ai.Information.Description);
                log.MessageContent("Id: {0}", ai.Information.DeviceId);
                log.MessageContent("Identifier: {0}", ai.Information.DeviceIdentifier);
                log.MessageContent("Name: {0}", ai.Information.DeviceName);
                log.MessageContent("Driver: {0}", ai.Information.DriverName);
                log.MessageContent("Driver version: {0}", ai.Information.DriverVersion);
                log.MessageContent("Revision: {0}", ai.Information.Revision);
                log.MessageContent("Subsystem: {0}", ai.Information.SubSystemId);
                log.MessageContent("Vendor: {0}", ai.Information.VendorId);

                log.EndMessage();

                log.BeginRegion("Supported display modes:");

                foreach (D3d.DisplayMode m in ai.SupportedDisplayModes)
                    log.Info("{0}", m);
                
                log.EndRegion();
                log.EndRegion();
            }

            log.EndRegion();
        }
    }
}
