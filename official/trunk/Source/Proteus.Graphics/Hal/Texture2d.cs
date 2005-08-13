using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public class Texture2d
    {
        private static Kernel.Diagnostics.Log<Texture2d> log = 
            new Kernel.Diagnostics.Log<Texture2d>();

        public static Texture2d Create(Device device,D3d.Format format, int width, int height, bool dynamic, bool mipmap)
        {
            int mipLevels = 1;
            
            D3d.Usage d3dUsage = D3d.Usage.WriteOnly;
            
            if ( dynamic )
                d3dUsage |= D3d.Usage.Dynamic;
            
            if (mipmap)
            {
                d3dUsage |= D3d.Usage.AutoGenerateMipMap;
                mipLevels = 0;
            }

            D3d.Pool d3dPool = D3d.Pool.Managed;

            try
            {
                D3d.Texture d3dTexture = new D3d.Texture(device.D3dDevice,
                                                            width,
                                                            height,
                                                            mipLevels,
                                                            d3dUsage,
                                                            format,
                                                            d3dPool);

                Texture2d newTexture = new Texture2d();
                

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
            return null;
        }

        private Texture2d()
        {
        }
    }
}
