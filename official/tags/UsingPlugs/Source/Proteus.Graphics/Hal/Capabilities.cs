using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class Capabilities
    {
        public int textureMaxWidth  = 512;
        public int textureMaxHeight = 512;
        public int textureMaxDepth  = 512;

        public Capabilities( Device device )
        {
        }
    }
}
