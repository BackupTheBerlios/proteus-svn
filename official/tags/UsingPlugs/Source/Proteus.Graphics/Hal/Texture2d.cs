using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public class Texture2d : TextureBase
    {
        private D3d.Texture d3dTexture = null;

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

        public static Texture2d Create(TextureManager manager,D3d.Format format, int width, int height, bool dynamic, bool mipmap)
        {
            Texture2d newTexture = new Texture2d();
            if ( newTexture.Initialize( manager,format,width,height,dynamic,mipmap ) )
                return newTexture;

            return null;
        }

        private bool Initialize(TextureManager manager, D3d.Format format, int width, int height, bool dynamic, bool mipmap)
        {
            D3d.Pool d3dPool = D3d.Pool.Managed;
            D3d.Usage d3dUsage = manager.GetUsageFlags( dynamic,mipmap,false );

            try
            {
                d3dTexture = new D3d.Texture(   manager.Device.D3dDevice,
                                                width,
                                                height,
                                                manager.GetMipLevelCount(mipmap),
                                                d3dUsage,
                                                format,
                                                d3dPool );
                this.Initialize(manager, format, width, height, 1, d3dTexture);

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create texture: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create texture: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create texture: {0}", e.Message);
            }

            return false;
        }

        protected Texture2d()
        {
        } 
    }
}
