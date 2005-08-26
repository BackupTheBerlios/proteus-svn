using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    /// <summary>
    /// Todo: Share depthbuffer, backbuffer copy for anti aliasing.
    /// </summary>
    public class RenderTexture2d : RenderTextureBase
    {
        private D3d.Texture             d3dTexture      = null;
        private D3d.Surface             d3dDepthBuffer  = null;

        private static Kernel.Diagnostics.Log<Texture2d> log = 
            new Kernel.Diagnostics.Log<Texture2d>();

        public override Microsoft.DirectX.Direct3D.Surface Lock(int surface)
        {
            if (!textureLock)
            {
                D3d.Surface lockedSurface = d3dTexture.GetSurfaceLevel(0);
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
            if (channel == 0)
            {
                textureManager.Device.D3dDevice.DepthStencilSurface = d3dDepthBuffer;
            }
        
            textureManager.Device.D3dDevice.SetRenderTarget( channel,d3dTexture.GetSurfaceLevel(0) );
            return true;
        }

        public static RenderTexture2d Create(TextureManager manager,D3d.Format format, int width, int height, bool mipmap,int multisample)
        {
            RenderTexture2d newTexture = new RenderTexture2d();
            if ( newTexture.Initialize( manager,format,width,height,mipmap,multisample ) )
                return newTexture;

            return null;
        }

        private bool Initialize(TextureManager manager, D3d.Format format, int width, int height,bool mipmap,int multisample)
        {
            D3d.Pool d3dPool    = D3d.Pool.Default;
            D3d.Usage d3dUsage  = manager.GetUsageFlags( false,mipmap,true );

            try
            {
                d3dTexture = new D3d.Texture(   manager.Device.D3dDevice,
                                                width,
                                                height,
                                                manager.GetMipLevelCount(mipmap),
                                                d3dUsage,
                                                format,
                                                d3dPool );

                // Create depth buffer.
                d3dDepthBuffer = manager.Device.D3dDevice.CreateDepthStencilSurface( width,height,manager.Device.Settings.depthBufferFormat,(D3d.MultiSampleType)multisample,0,true );

                this.Initialize(manager, format, width, height, 1, d3dTexture);

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create render texture: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create render texture: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create render texture: {0}", e.Message);
            }

            return false;
        }

        protected RenderTexture2d()
        {
        } 
    }
}
