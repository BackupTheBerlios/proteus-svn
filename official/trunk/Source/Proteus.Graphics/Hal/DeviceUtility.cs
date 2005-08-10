using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;
using Swf = System.Windows.Forms;

namespace Proteus.Graphics.Hal
{
    public sealed class DeviceUtility
    {
        private static Kernel.Diagnostics.Log<DeviceUtility> log =
            new Kernel.Diagnostics.Log<DeviceUtility>();

        private static void EnumerateAdapters()
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

                log.BeginMessage(Proteus.Kernel.Diagnostics.LogLevel.Info);

                foreach (D3d.DisplayMode m in ai.SupportedDisplayModes)
                    log.MessageContent("{0}", m);

                log.EndMessage();

                log.EndRegion();
                log.EndRegion();
            }

            log.EndRegion();
        }

        private static D3d.Device CreateDevice(System.Windows.Forms.Control renderWindow,
                                                    D3d.PresentParameters presentParams,
                                                    D3d.CreateFlags createFlags,
                                                    Settings deviceSettings)
        {
            try
            {
                D3d.Device d3dDevice = new D3d.Device(deviceSettings.adapterIndex,
                                                        deviceSettings.deviceType,
                                                        renderWindow,
                                                        createFlags,
                                                        presentParams);
                return d3dDevice;
            }
            catch (D3d.DeviceLostException e)
            {
                log.Warning("Unable to create Direct3D device: {0}", e.Message);
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create Direct3D device: {0}", e.Message);
            }
            catch (D3d.NotAvailableException e)
            {
                log.Warning("Unable to create Direct3D device: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create Direct3D device: {0}", e.Message);
            }

            return null;
        }

        public static bool TestMdxPresence()
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

        public static D3d.Device    CreateDevice(System.Windows.Forms.Control renderWindow)
        {
            EnumerateAdapters();
            
            Settings                deviceSettings  = new Settings();

            // First try full hardware acceleration.
            D3d.PresentParameters   presentParams   = deviceSettings.GetPresentParameters( renderWindow );
            D3d.CreateFlags         createFlags     = deviceSettings.GetCreateFlags(true,true);

            D3d.Device d3dDevice = CreateDevice( renderWindow,presentParams,createFlags,deviceSettings );

            if (d3dDevice == null)
            {
                log.Warning("Pure device not available.");
                createFlags = deviceSettings.GetCreateFlags(false,true);

                d3dDevice = CreateDevice( renderWindow,presentParams,createFlags,deviceSettings );

                if (d3dDevice == null)
                {
                    log.Warning("Hardware vertex processing not available.");
                    createFlags = deviceSettings.GetCreateFlags( false,false );

                    d3dDevice = CreateDevice( renderWindow,presentParams,createFlags,deviceSettings );
                }
            }

            return d3dDevice;
        }
    }
}
