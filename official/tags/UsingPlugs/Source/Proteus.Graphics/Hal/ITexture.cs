using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public interface ITexture : IRestorable
    {
        D3d.Format      Format          { get; }
        D3d.BaseTexture BaseTexture     { get; }
        int             Width           { get; }
        int             Height          { get; }
        int             Depth           { get; }
        int             Size            { get; }

        D3d.Surface     Lock( int surface );
        void            Unlock();

        bool            SetAsSampler( int sampler );
    }
}
