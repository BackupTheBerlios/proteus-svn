using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public interface ITexture
    {
        D3d.BaseTexture BaseTexture { get; }
        int             Sampler     { get; set; }
    }
}
