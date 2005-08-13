using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public abstract class TextureBase : ITexture
    {
        protected int               textureWidth    = 0;
        protected int               textureHeight   = 0;
        protected int               textureDepth    = 0;
        protected D3d.Format        textureFormat   = D3d.Format.Unknown;
        protected D3d.BaseTexture   textureBase     = null;
        protected TextureManager    textureManager  = null;
        protected bool              textureLock     = false;

        #region ITexture Members

        public D3d.BaseTexture BaseTexture
        {
            get { return textureBase; }
        }

        public Microsoft.DirectX.Direct3D.Format Format
        {
            get { return textureFormat; }
        }

        public int Width
        {
            get { return textureWidth; }
        }

        public int Height
        {
            get { return textureHeight; }
        }

        public int Depth
        {
            get { return textureDepth; }
        }

        public int Size
        {
            get { return textureWidth * textureHeight * textureDepth * FormatUtility.GetSize(textureFormat) / 8; }
        }

        public abstract Microsoft.DirectX.Direct3D.Surface Lock(int surface);
        public abstract void Unlock();

        public bool SetAsSampler(int sampler)
        {
            return textureManager.SetAsSampler( this,sampler );
        }

        #endregion

        #region IRestorable Members

        public bool Restore()
        {
            return true;
        }

        #endregion

        protected void Initialize(TextureManager manager, D3d.Format format, int width, int height, int depth,D3d.BaseTexture d3dBase )
        {
            textureManager  = manager;
            textureFormat   = format;
            textureWidth    = width;
            textureHeight   = height;
            textureDepth    = depth;
            textureBase     = d3dBase;
        }

        protected D3d.Usage CreateUsageFlags(bool dynamic, bool mipmap, bool isTarget)
        {
            D3d.Usage usageFlags = D3d.Usage.WriteOnly;

            if ( dynamic )
                usageFlags |= D3d.Usage.Dynamic;

            if ( mipmap )
                usageFlags |= D3d.Usage.AutoGenerateMipMap;

            if ( isTarget )
                usageFlags |= D3d.Usage.RenderTarget;

            return usageFlags;
        }

        protected int CreateMipLevels(bool mipmap)
        {
            if ( mipmap )
                return 0;
            return 1;
        }

        protected TextureBase()
        {
        }
    }
}
