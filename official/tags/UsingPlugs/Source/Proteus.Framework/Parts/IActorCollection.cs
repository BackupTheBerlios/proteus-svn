using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [Plug()]
    public interface IActorCollection : ICollection<IActor>
    {
    }
}
