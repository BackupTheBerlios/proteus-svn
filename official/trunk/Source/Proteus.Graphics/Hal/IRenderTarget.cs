using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public interface ITarget : ITexture
    {
        bool SetAsTarget( int targetChannel );
    }
}