using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IInputPlug : IPlug
    {
        bool OnConnection(IConnection connection);
    }
}
