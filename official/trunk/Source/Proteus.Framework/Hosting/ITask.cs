using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Hosting
{
    public interface ITask : IDisposable
    {
        bool Initialize( Engine engine );
        bool Update(double deltaTime);
    }
}
