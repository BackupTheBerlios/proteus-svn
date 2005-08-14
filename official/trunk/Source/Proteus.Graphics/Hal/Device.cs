using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Device : Kernel.Pattern.Disposable 
    {
        private D3d.Device      d3dDevice               = null;
        private Capabilities    d3dCapabilities         = null;
        private Settings        d3dSettings             = null;
        private FrameBuffer     d3dPrimaryFrameBuffer   = null;
        private TextureManager  d3dTextureManager       = null;
        private GeometryManager d3dGeometryManager      = null;
  

        public D3d.Device D3dDevice
        {
            get { return d3dDevice; }
        }

        public Capabilities Capabilities
        {
            get { return d3dCapabilities; }
        }

        public Settings Settings
        {
            get { return d3dSettings; }
        }

        public TextureManager TextureManager
        {
            get { return d3dTextureManager; }
        }

        public GeometryManager GeometryManager
        {
            get { return d3dGeometryManager; }
        }

        public FrameBuffer CreateFrameBuffer(System.Windows.Forms.Control renderWindow)
        {
            return null;
        }

        protected override void ReleaseManaged()
        {
            if (d3dDevice != null)
                d3dDevice.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public static Device Create(System.Windows.Forms.Control renderWindow)
        {     
            Device newDevice = new Device();
            if ( newDevice.Initialize(renderWindow) )
            {
                return newDevice;
            }
         
            return null;
        }

        private bool Initialize(System.Windows.Forms.Control renderWindow )
        {
            d3dSettings     = new Settings();
            d3dDevice       = DeviceUtility.CreateDevice( d3dSettings,renderWindow );

            if (d3dDevice != null)
            {
                d3dCapabilities         = new Capabilities(this);
                d3dPrimaryFrameBuffer   = FrameBuffer.Create( d3dDevice );
                d3dGeometryManager      = GeometryManager.Create( this );
                d3dTextureManager       = TextureManager.Create(this);

                return true;
            }
            return false;
        }

        private Device()
        {
        }
    }
}
