using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;
using Swf = System.Windows.Forms;

namespace Proteus.Graphics.Hal
{
    public class DeviceEnumerator
    {
        private static const D3d.Format backBufferFormat    = D3d.Format.A8R8G8B8;
        private static const D3d.Format depthBufferFormat   = D3d.Format.D24S8;

        private static Kernel.Diagnostics.Log<DeviceEnumerator> log =
            new Kernel.Diagnostics.Log<DeviceEnumerator>();

        private static int FindMultisampleMode()
        {
            int             adapter     = Kernel.Registry.Manager.Instance.GetValue("Graphics.Adapter",0);
            D3d.DeviceType  deviceType  = Kernel.Registry.Manager.Instance.GetValue("Graphics.DeviceType",D3d.DeviceType.Hardware );
            bool            windowed    = Kernel.Registry.Manager.Instance.GetValue("Graphics.Windowed",true );
            int             multisample = Kernel.Registry.Manager.Instance.GetValue("Graphics.Multisample", 0);

            while (!D3d.Manager.CheckDeviceMultiSampleType(adapter, deviceType, backBufferFormat, windowed, (D3d.MutliSampleType)multisample))
            {
                multisample--;
            }

            return multisample;
        }

        public static bool TestMdxPrescense()
        {
            try
            {
                int count = D3d.Manager.Adapters.Count;
                if ( count > 0 )
                    return true;
            }
            catch (System.Exception e)
            {
                log.Exception(e);
                return false;
            }
            return false;
        }

        public static D3d.PresentParameters CreatePresentParameters(    Swf.Control window,
                                                                        bool windowed,
                                                                        int width,
                                                                        int height,
                                                                        int freq )
        {
            D3d.PresentParameters pp = new D3d.PresentParameters();

            pp.AutoDepthStencilFormat = depthBufferFormat;
            pp.BackBufferCount = 1;
            pp.BackBufferFormat = backBufferFormat;
            pp.Windowed = windowed;
            pp.DeviceWindow = window;
            pp.EnableAutoDepthStencil = true;
            pp.ForceNoMultiThreadedFlag = false;
            pp.MultiSample = (D3d.MultiSampleType)FindMultisampleMode();
            pp.MultiSampleQuality = 0;
            pp.PresentationInterval = D3d.PresentInterval.Default;
            pp.PresentFlag = D3d.PresentFlag.DiscardDepthStencil;
            pp.SwapEffect = D3d.SwapEffect.Discard;

            if (windowed)
            {
                pp.BackBufferWidth = 0;
                pp.BackBufferHeight = 0;
                pp.FullScreenRefreshRateInHz = 0;
            }
            else
            {
                pp.BackBufferWidth = width;
                pp.BackBufferHeight = height;
                pp.FullScreenRefreshRateInHz = freq;
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
