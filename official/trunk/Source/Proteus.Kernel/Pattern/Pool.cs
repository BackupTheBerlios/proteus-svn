using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public interface IPoolCreator<ItemType>
    {
        ItemType CreateInstance();
    }

    public class Pool<ItemType,CreatorType> : Kernel.Pattern.Disposable
        where ItemType : IPoolItem<ItemType,CreatorType>,IDisposable
        where CreatorType : IPoolCreator<ItemType>
    {
        private List<ItemType>      poolItems           = new List<ItemType>();
        private CreatorType         creator             = default(CreatorType);

        public ItemType Create()
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
                return Create();
            }

            IncreaseSize( poolItems.Count );
            return Create();
        }

        public void Release(ItemType item)
        {
            poolItems.Add( item );
        }

        private void IncreaseSize( int size )
        {
            for (int i = 0; i < size; i++)
            {
                ItemType newItem = creator.CreateInstance();
                if (!newItem.Equals(default(ItemType)))
                {
                    poolItems.Add(newItem);
                    return;
                }
                
                throw new ApplicationException("No instances createable.");
            }
        }

        protected override void ReleaseManaged()
        {
            foreach( ItemType t in poolItems )
                t.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public Pool(int initialSize,CreatorType _creator)
        {
            creator = _creator;
            IncreaseSize(initialSize);
        }

        public Pool( CreatorType _creator ) : this ( 8,_creator )
        {
        }
    }
}
