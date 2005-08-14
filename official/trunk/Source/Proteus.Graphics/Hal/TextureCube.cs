using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public class TextureCube : TextureBase
    {
        protected D3d.CubeTexture d3dTexture = null;

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

        public static TextureCube Create(TextureManager manager,D3d.Format format, int size, bool dynamic, bool mipmap)
        {
            TextureCube newTexture = new TextureCube();
            if ( newTexture.Initialize( manager,format,size,dynamic,mipmap ) )
                return newTexture;

            return null;
        }

        private bool Initialize(TextureManager manager, D3d.Format format, int size, bool dynamic, bool mipmap)
        {
            D3d.Pool d3dPool = D3d.Pool.Managed;
            D3d.Usage d3dUsage = manager.GetUsageFlags( dynamic,mipmap,false );

            try
            {
                d3dTexture = new D3d.CubeTexture( manager.Device.D3dDevice,size,manager.GetMipLevelCount(mipmap),d3dUsage,format,d3dPool);
                this.Initialize(manager, format, size,size,6, d3dTexture);

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create cube texture: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create cube texture: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create cube texture: {0}", e.Message);
            }

            return false;
        }

        protected TextureCube()
        {
        } 
    }
}
