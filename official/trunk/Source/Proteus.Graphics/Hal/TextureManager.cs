using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TextureManager
    {
        Texture2d CreateTexture2d(D3d.Format format, int width, int height, bool dynamic,bool mipmap )
        {
            return null;
        }

        TextureCube CreateTextureCube(D3d.Format format, int size, bool dynamic, bool mipmap)
        {
            return null;
        }

        Texture3d CreateTexture3d(D3d.Format format, int width, int height, int depth, bool dynamic, bool mipmap)
        {
            return null;
        }

        RenderTexture2d CreateRenderTexture2d(D3d.Format format, int width, int height, bool mipmap, int multisample)
        {
            return null;
        }

        RenderTextureCube CreateRenderTextureCube(D3d.Format format, int size, bool mipmap, int multisample)
        {
            return null;
        }

        DepthMap2d CreateDepthMap2d(int width, int height, bool mipmap, bool pcf)
        {
            return null;
        }

        DepthMapCube CreateDepthCube(int size, bool mipmap, bool pcf)
        {
            return null;
        }

        public TextureManager(D3d.Device _d3dDevice)
        {
        }
    }
}
