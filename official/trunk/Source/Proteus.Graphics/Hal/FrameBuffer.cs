using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class FrameBuffer : IRenderTarget
    {
        private Device          d3dDevice       = null;
        private D3d.SwapChain   d3dSwapChain    = null;
        private D3d.Surface     d3dBackBuffer   = null;
        private D3d.Surface     d3dDepthBuffer  = null;

        public void Present()
        {
            d3dSwapChain.Present();
        }

        #region IRenderTarget Members

        public int Width
        {
            get
            {
                return d3dBackBuffer.Description.Width;
            }
        }

        public int Height
        {
            get
            {
                return d3dBackBuffer.Description.Height;
            }
        }

        public bool SetAsTarget(int targetChannel,int surface )
        {
            // Always set as the first target, ignoring any settings.
            d3dDevice.D3dDevice.SetRenderTarget(0,d3dBackBuffer);
            d3dDevice.D3dDevice.DepthStencilSurface = d3dDepthBuffer;
            return true;
        }

        #endregion
        
        public static FrameBuffer Create(Device device)
        {
            FrameBuffer newBuffer       = new FrameBuffer();
            newBuffer.d3dSwapChain      = device.D3dDevice.GetSwapChain(0);
            newBuffer.d3dBackBuffer     = newBuffer.d3dSwapChain.GetBackBuffer(0,D3d.BackBufferType.Mono );
            newBuffer.d3dDepthBuffer    = device.D3dDevice.DepthStencilSurface;
            newBuffer.d3dDevice         = device;

            return newBuffer;
        }

        public static FrameBuffer Create(Settings settings, D3d.Device device, System.Windows.Forms.Control renderWindow)
        {
            D3d.SwapChain swapChain = new D3d.SwapChain( device,settings.GetPresentParameters(renderWindow) );
            return null;
        }
    }
}
