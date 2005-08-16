using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public class RenderTextureCube : RenderTextureBase
    {
        protected D3d.CubeTexture   d3dTexture      = null;
        protected D3d.Surface       d3dDepthBuffer  = null;

        private static Kernel.Diagnostics.Log<TextureCube> log =
            new Kernel.Diagnostics.Log<TextureCube>();

        public override D3d.Surface Lock(int surface)
        {
            if (!textureLock)
            {
                D3d.Surface lockedSurface = d3dTexture.GetCubeMapSurface((D3d.CubeMapFace)surface,0 );
                textureLock = true;
                return lockedSurface;
            }
            return null;
        }

        public override void Unlock()
        {
            if (textureLock)
            {
                textureLock = false;
            }
        }

        public override bool SetAsTarget(int channel, int surface)
        {
            if (channel != 0)
            {
                textureManager.Device.D3dDevice.DepthStencilSurface = d3dDepthBuffer;
            }

            textureManager.Device.D3dDevice.SetRenderTarget(channel, d3dTexture.GetCubeMapSurface((D3d.CubeMapFace)surface,0 ) );
            return true;
        }

        public static RenderTextureCube Create(TextureManager manager,D3d.Format format, int size, bool mipmap,int multisample )
        {
            RenderTextureCube newTexture = new RenderTextureCube();
            if ( newTexture.Initialize( manager,format,size,mipmap,multisample ) )
                return newTexture;

            return null;
        }

        private bool Initialize(TextureManager manager, D3d.Format format, int size, bool mipmap,int multisample )
        {
            D3d.Pool d3dPool = D3d.Pool.Default;
            D3d.Usage d3dUsage = manager.GetUsageFlags( false,mipmap,true );

            try
            {
                d3dTexture = new D3d.CubeTexture( manager.Device.D3dDevice,size,manager.GetMipLevelCount(mipmap),d3dUsage,format,d3dPool);
                
                // Create depth buffer.
                d3dDepthBuffer = manager.Device.D3dDevice.CreateDepthStencilSurface(size, size, manager.Device.Settings.depthBufferFormat, (D3d.MultiSampleType)multisample, 0, true);

                this.Initialize(manager, format, size,size,6, d3dTexture);

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create render cube texture: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create render cube texture: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create render cube texture: {0}", e.Message);
            }

            return false;
        }

        protected RenderTextureCube()
        {
        }    
    }
}
