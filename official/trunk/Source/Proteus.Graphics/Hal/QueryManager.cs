using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class QueryManager 
    {
        public sealed class QueryCreator : Kernel.Pattern.IPoolCreator<Query>
        {
            #region IPoolCreator<Query> Members

            public Query CreateInstance()
            {
                return null;
            }

            #endregion
        }

        private Device device = null;

        private bool Initialize(Device _device)
        {
            device = _device;
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
