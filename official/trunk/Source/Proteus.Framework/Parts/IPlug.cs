using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IPlug : IPart
    {
        bool            IsMultiplex     { get; }
        IConnection[]   Connections     { get; }
        IActor          Owner           { get; }
    }
}
