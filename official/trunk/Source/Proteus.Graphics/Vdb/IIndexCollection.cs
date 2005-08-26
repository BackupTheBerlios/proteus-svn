using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Vdb
{
    public interface IIndexCollection
    {
        bool Register( IIndex index,Type queryType,int priority );
        void Unregister( IIndex index );
    }
}
