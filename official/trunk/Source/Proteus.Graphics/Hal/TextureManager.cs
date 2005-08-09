using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TextureManager
    {
        public RenderTexture CreateRenderTexture(D3d.Format format, int width, int height, int multisample)
        {
            return null;
        }

        public RenderCubeTexture CreateRenderCubeTexture(D3d.Format format, int size, int multisample)
        {
            return null;
        }

        
    }
}
