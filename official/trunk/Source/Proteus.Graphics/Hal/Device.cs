using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Device : Kernel.Pattern.Disposable 
    {
        private D3d.Device d3dDevice = null;

        public static Device Create(System.Windows.Forms.Control renderWindow)
        {
            D3d.Device _d3dDevice = DeviceUtility.CreateDevice( renderWindow );

            if ( _d3dDevice != null )
            {
                Device newDevice = new Device();
                if (newDevice.Initialize(_d3dDevice))
                {
                    return newDevice;
                }
            }
            return null;
        }

        private bool Initialize(D3d.Device _d3dDevice)
        {
            return true;
        }

        protected override void ReleaseManaged()
        {
            if (d3dDevice != null)
                d3dDevice.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        private Device()
        {
        }
    }
}
