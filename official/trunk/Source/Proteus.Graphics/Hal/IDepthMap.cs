using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public interface IDepthMap : ITarget
    {
        bool IsPcf { get; }
    }
}
