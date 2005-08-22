using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IActorCollection 
    {
        IActor              this[string name]   { get; }
        IActor              this[int index]     { get; }
        
        int                 Count               { get; }
        
        IEnumerator<IActor> GetEnumerator();

        bool                Add( IActor actor );
        bool                Remove( IActor actor );
        
        void                Clear();
        bool                IsCompatible( IActor actor );
    }
}
