using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class FrameBuffer : IRenderTarget
    {
        private D3d.SwapChain d3dSwapChain = null;

        #region IRenderTarget Members

        public bool SetAsTarget(int targetChannel)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
        
        public static FrameBuffer Create(D3d.Device device)
        {
            return null;
        }       
    }
}
