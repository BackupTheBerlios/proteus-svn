using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Vdb
{
    public sealed class Database
    {
        private SortedList<Type, IIndexCollection> indexCollections =
            new SortedList<Type, IIndexCollection>();

    }
}
