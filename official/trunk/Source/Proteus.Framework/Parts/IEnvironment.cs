using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IEnvironment : IEnumerable<IActor>
    {
        IActor          this[string name] { get; }

        IActor          Owner { get; }

        IActor[]        Actors { get; }
        IConnection[]   Connections { get; }

        bool            Add(IActor actor);
        bool            Add(IConnection connection);
        void            Remove(IActor actor);
        void            Remove(IConnection connection);
    }
}
