using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class QueryManager : 
        Kernel.Pattern.Pool<Query,QueryManager>,
        Kernel.Pattern.IPoolCreator<Query>
    {
        #region IPoolCreator<Query> Members

        public Query Create()
        {
            return null;
        }

        #endregion
}
}
