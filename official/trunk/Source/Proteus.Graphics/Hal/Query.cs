using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class Query : Kernel.Pattern.Disposable,
                                Kernel.Pattern.IPoolItem<Query,QueryManager.QueryCreator>
    {
        protected override void ReleaseManaged()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override void ReleaseUnmanaged()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #region IPoolItem<Query,QueryCreator> Members

        public Proteus.Kernel.Pattern.Pool<Query, QueryManager.QueryCreator> Pool
        {
            set { throw new Exception("The method or operation is not implemented."); }
        }

        public bool Reset()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Release()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
}
}
