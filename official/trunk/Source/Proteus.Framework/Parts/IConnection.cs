using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IConnection : IDisposable
    {
        IOutputPlug Source { get; }
        IInputPlug  Target { get; }
    }
}
