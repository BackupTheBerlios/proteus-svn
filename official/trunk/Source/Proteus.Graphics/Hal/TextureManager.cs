using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TextureManager : Kernel.Pattern.Disposable
    {
        private Device device = null;

        public Texture2d CreateTexture2d(D3d.Format format, int width, int height, bool dynamic,bool mipmap )
        {
            return null;
        }

        public TextureCube CreateTextureCube(D3d.Format format, int size, bool dynamic, bool mipmap)
        {
            return null;
        }

        public Texture3d CreateTexture3d(D3d.Format format, int width, int height, int depth, bool dynamic, bool mipmap)
        {
            return null;
        }

        public RenderTexture2d CreateRenderTexture2d(D3d.Format format, int width, int height, bool mipmap, int multisample)
        {
            return null;
        }

        public RenderTextureCube CreateRenderTextureCube(D3d.Format format, int size, bool mipmap, int multisample)
        {
            return null;
        }

        public DepthMap2d CreateDepthMap2d(int width, int height, bool mipmap, bool pcf)
        {
            return null;
        }

        public DepthMapCube CreateDepthCube(int size, bool mipmap, bool pcf)
        {
            return null;
        }

        public bool SetAsSampler(ITexture texture,int channel)
        {
            device.D3dDevice.SetTexture( channel,texture.BaseTexture );
            return true;
        }

        public bool SetAsTarget(ITarget target,int channel)
        {
            return true;
        }

        protected override void ReleaseManaged()
        {
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public static TextureManager Create(Device _device)
        {
            TextureManager textureManager = new TextureManager();
            if (textureManager.Initialize(_device))
                return textureManager;

            return null;
        }

        private bool Initialize(Device _device)
        {
            this.device = _device;
            return true;
        }

        private TextureManager()
        {
        }     
    }
}
