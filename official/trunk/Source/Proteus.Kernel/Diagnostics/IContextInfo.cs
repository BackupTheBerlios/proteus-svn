using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public interface IContextInfo
    {
        string Name { get; }
        string Text { get; }
    }
}
