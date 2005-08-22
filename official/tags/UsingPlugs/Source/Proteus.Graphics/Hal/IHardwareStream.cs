using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Graphics.Hal
{
    public interface IHardwareStream : IRestorable,IDisposable
    {
        Type    Type { get; }
        int     Size { get; }
        
        Array   Lock( bool read );
        void    Unlock();

        bool    Activate( int channel );
        bool    Activate();
    }
}
