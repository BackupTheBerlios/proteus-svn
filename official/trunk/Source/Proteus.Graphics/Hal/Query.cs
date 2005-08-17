using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class Query : Kernel.Pattern.Disposable,
                                Kernel.Pattern.IPoolItem<Query,QueryManager.Creator>
    {
        private Kernel.Pattern.Pool<Query,QueryManager.Creator>    owner       = null;
        private D3d.Query                                          d3dQuery    = null;
        private bool                                               finished    = false;
        private int                                                fragments   = 0;
        private static Kernel.Diagnostics.Log<Query>               log         = 
            new Kernel.Diagnostics.Log<Query>();

        #region IPoolItem<Query,QueryCreator> Members

        public Proteus.Kernel.Pattern.Pool<Query, QueryManager.Creator> Pool
        {
            set { owner = value; }
        }

        public bool IsFinished
        {
            get 
            {
                if (!finished)
                {
                    fragments = (int)d3dQuery.GetData( typeof(int),true,out finished );
                }

                return finished;
            }
        }

        public int Fragments
        {
            get { return fragments; }
        }

        public void Begin()
        {
            d3dQuery.Issue( D3d.IssueFlags.Begin );
        }

        public void End()
        {
            d3dQuery.Issue( D3d.IssueFlags.End );
        }

        public bool Reset()
        {
            finished    = false;
            fragments   = 0;
            return true;
        }

        public void Release()
        {
            owner.Release(this);
        }

        #endregion

        protected override void ReleaseManaged()
        {
            d3dQuery.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        private bool Initialize(QueryManager manager )
        {
            d3dQuery = new D3d.Query( manager.Device.D3dDevice,D3d.QueryType.Occlusion );
            return true;
        }

        public static Query Create(QueryManager manager)
        {
            Query newQuery = new Query();
            if ( newQuery.Initialize( manager ) )
                return newQuery;

            return null;
        }

        private Query()
        {
        }
    }
}
