using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Vdb
{
    public interface IIndex : IDisposable
    {
        bool Initialize(IIndexCollection collection);
    }
}
