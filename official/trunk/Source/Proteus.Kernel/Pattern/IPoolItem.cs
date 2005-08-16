using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public interface IPoolItem<ItemType,CreatorType>
        where ItemType : IPoolItem<ItemType, CreatorType>,IDisposable
        where CreatorType : IPoolCreator<ItemType>, new()
    {
        Pool<ItemType,CreatorType>  Pool { set; }
        bool                        Reset();
        void                        Release();
    }
}
