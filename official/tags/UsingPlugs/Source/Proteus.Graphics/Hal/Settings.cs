using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Settings
    {
        public int                      fullScreenWidth         = 800;
        public int                      fullScreenHeight        = 600;
        public int                      fullScreenRefresh       = 60;
        public bool                     windowed                = true;

        public D3d.Format               backBufferFormat        = D3d.Format.A8B8G8R8;
        public D3d.DepthFormat          depthBufferFormat       = D3d.DepthFormat.D24S8;

        public int                      adapterIndex            = 0;
        public D3d.DeviceType           deviceType              = D3d.DeviceType.Hardware;
        public int                      multiSampleLevel        = 0;
        public int                      backBufferCount         = 1;

        private void                    FixMultiSampleType()
        {
            while ( !D3d.Manager.CheckDeviceMultiSampleType(adapterIndex, deviceType, backBufferFormat, windowed, (D3d.MultiSampleType)this.multiSampleLevel) ||
                    !D3d.Manager.CheckDeviceMultiSampleType(adapterIndex, deviceType, (D3d.Format)depthBufferFormat, windowed, (D3d.MultiSampleType)this.multiSampleLevel))
            {
                multiSampleLevel --;
                if ( multiSampleLevel < 0 )
                    break;
            }
        }

        public D3d.PresentParameters    GetPresentParameters(System.Windows.Forms.Control renderWindow)
        {
            D3d.PresentParameters pp            = new D3d.PresentParameters();

            pp.AutoDepthStencilFormat           = depthBufferFormat;
            pp.BackBufferCount                  = backBufferCount;
            pp.BackBufferFormat                 = backBufferFormat;
            pp.Windowed                         = windowed;
            pp.DeviceWindow                     = renderWindow;
            pp.EnableAutoDepthStencil           = true;
            pp.ForceNoMultiThreadedFlag         = false;
            pp.MultiSample                      = (D3d.MultiSampleType)multiSampleLevel;
            pp.MultiSampleQuality               = 0;
            
            pp.PresentationInterval             = D3d.PresentInterval.Default;
            pp.PresentFlag                      = D3d.PresentFlag.DiscardDepthStencil;
            pp.SwapEffect                       = D3d.SwapEffect.Discard;

            if (windowed)
            {
                pp.BackBufferWidth              = 0;
                pp.BackBufferHeight             = 0;
                pp.FullScreenRefreshRateInHz    = 0;
            }
            else
            {
                pp.BackBufferWidth              = fullScreenWidth;
                pp.BackBufferHeight             = fullScreenHeight;
                pp.FullScreenRefreshRateInHz    = fullScreenRefresh;
            }

            return pp;
        }

        public D3d.CreateFlags          GetCreateFlags( bool pure,bool hardware )
        {
            D3d.CreateFlags createFlags = (D3d.CreateFlags)0;
        
            if ( pure )
                createFlags |= D3d.CreateFlags.PureDevice;
            
            if ( hardware )
                createFlags |= D3d.CreateFlags.HardwareVertexProcessing;
            else
                createFlags |= D3d.CreateFlags.SoftwareVertexProcessing;
        
            return createFlags;
        }

        private void Read()
        {
            fullScreenWidth     = Kernel.Registry.Manager.Instance.GetValue("Graphics.Width",fullScreenWidth );
            fullScreenHeight    = Kernel.Registry.Manager.Instance.GetValue("Graphics.Height",fullScreenHeight);
            fullScreenRefresh   = Kernel.Registry.Manager.Instance.GetValue("Graphics.RefreshRate",fullScreenRefresh);
            
            windowed            = Kernel.Registry.Manager.Instance.GetValue("Graphics.Windowed",windowed );
            backBufferFormat    = Kernel.Registry.Manager.Instance.GetValue("Graphics.BackBufferFormat",backBufferFormat );
            depthBufferFormat   = Kernel.Registry.Manager.Instance.GetValue("Graphics.DepthBufferFormat",depthBufferFormat );

            adapterIndex        = Kernel.Registry.Manager.Instance.GetValue("Graphics.Adapter",adapterIndex );
            deviceType          = Kernel.Registry.Manager.Instance.GetValue("Graphics.DeviceType",deviceType );
            multiSampleLevel    = Kernel.Registry.Manager.Instance.GetValue("Graphics.Multisample",multiSampleLevel );
            backBufferCount     = Kernel.Registry.Manager.Instance.GetValue("Graphics.BackBufferCount",backBufferCount );
        }

        public Settings()
        {
            Read();
            FixMultiSampleType();
        }
    }
}
