using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public interface IRenderTarget 
    {
        int     Width { get; }
        int     Height { get; }
        bool    SetAsTarget( int channel,int surface );
    }
}
