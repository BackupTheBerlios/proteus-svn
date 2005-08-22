using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class TextureVolume : TextureBase
    {
        private D3d.VolumeTexture d3dTexture = null;

        private static Kernel.Diagnostics.Log<TextureCube> log =
            new Kernel.Diagnostics.Log<TextureCube>();

        public override D3d.Surface Lock(int surface)
        {
            if (!textureLock)
            {
                return null;
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

        public static TextureVolume Create(TextureManager manager,D3d.Format format,int width,int height,int depth,bool dynamic, bool mipmap)
        {
            TextureVolume newTexture = new TextureVolume();
            if ( newTexture.Initialize( manager,format,width,height,depth,dynamic,mipmap ) )
                return newTexture;

            return null;
        }

        private bool Initialize(TextureManager manager, D3d.Format format, int width,int height,int depth, bool dynamic, bool mipmap)
        {
            D3d.Pool d3dPool = D3d.Pool.Managed;
            D3d.Usage d3dUsage = manager.GetUsageFlags( dynamic,mipmap,false );

            try
            {
                d3dTexture = new D3d.VolumeTexture( manager.Device.D3dDevice,width,height,depth,manager.GetMipLevelCount(mipmap),d3dUsage,format,d3dPool);
                this.Initialize(manager, format, width, height, depth,d3dTexture );

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create 3d texture: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create 3d texture: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create 3d texture: {0}", e.Message);
            }

            return false;
        }

        private TextureVolume()
        {
        } 
    }
}
