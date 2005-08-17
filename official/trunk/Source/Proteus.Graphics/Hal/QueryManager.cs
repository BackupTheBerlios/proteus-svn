using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class QueryManager 
    {
        public sealed class Creator : Kernel.Pattern.IPoolCreator<Query>
        {
            private QueryManager manager = null;

            #region IPoolCreator<Query> Members

            public Query CreateInstance()
            {
                return Query.Create( manager );
            }

            #endregion

            public Creator(QueryManager _manager)
            {
                manager = _manager;
            }
        }

        private Device                              device      = null;
        private Kernel.Pattern.Pool<Query,Creator>  queryPool   = null;

        public Device Device
        {
            get { return device; }
        }

        public Query Create()
        {
            return queryPool.Create();
        }

        private bool Initialize(Device _device)
        {
            device = _device;

            Creator queryCreator = new Creator( this );
            queryPool = new Kernel.Pattern.Pool<Query,Creator>( queryCreator );

            return true;
        }

        public static QueryManager Create(Device _device)
        {
            QueryManager newManager = new QueryManager();
            if ( newManager.Initialize( _device ) )
                return newManager;

            return null;
        }

        private QueryManager()
        {
        }
    }
}
