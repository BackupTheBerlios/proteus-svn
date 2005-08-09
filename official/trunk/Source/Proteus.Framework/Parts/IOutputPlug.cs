using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IOutputPlug : IPlug
    {
        bool        IsCompatible( IInputPlug inputPlug );
        IConnection Connect( IInputPlug inputPlug );
    }
}
