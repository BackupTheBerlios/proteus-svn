using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public interface IPoolCreator<ItemType>
    {
        ItemType Create();
    }

    public sealed class Pool<ItemType,CreatorType> 
        where ItemType : IPoolItem<ItemType,CreatorType>,new() 
        where CreatorType : IPoolCreator<ItemType>,new()
    {
        private List<ItemType>      poolItems           = new List<ItemType>();
        private static CreatorType  creator             = new CreatorType();

        public ItemType New()
        {
            if (poolItems.Count > 0)
            {
                ItemType item = poolItems[0];
                poolItems.RemoveAt(0);

                if (item.Reset())
                {
                    item.Pool = this;
                    return item;
                }
                return New();
            }

            IncreaseSize( poolItems.Count );
            return New();
        }

        public void Release(ItemType item)
        {
            poolItems.Add( item );
        }

        private void IncreaseSize( int size )
        {
            for (int i = 0; i < size; i++)
            {
                poolItems.Add( creator.Create() );
            }
        }

        public Pool(int initialSize)
        {
            IncreaseSize(initialSize);
        }

        public Pool() : this ( 8 )
        {
        }
    }
}
