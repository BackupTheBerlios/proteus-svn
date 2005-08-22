using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public interface IVisitable
    {
        bool Accept(IVisitor visitor);
    }
}
