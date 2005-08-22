using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IEnvironment : IActorCollection
    {
        IActor          Owner { get; }
    }
}
