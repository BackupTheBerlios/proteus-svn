using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public interface IRenderTarget 
    {
        bool SetAsTarget( int channel,int surface );
    }
}
