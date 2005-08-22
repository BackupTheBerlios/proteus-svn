using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class IndexStream : Kernel.Pattern.Disposable,IHardwareStream
    {
        private D3d.IndexBuffer d3dIndexBuffer  = null;
        private bool            d3dIsLarge      = false;
        private int             d3dIndexSize    = 0;
        private bool            d3dLock         = false;
        private GeometryManager d3dManager      = null;

        private static Kernel.Diagnostics.Log<IndexStream> log = 
            new Kernel.Diagnostics.Log<IndexStream>();

        public D3d.IndexBuffer IndexBuffer
        {
            get { return d3dIndexBuffer; }
        }

        #region IHardwareStream Members

        public Type Type
        {
            get 
            { 
                if ( d3dIsLarge )
                    return typeof(UInt32);
                else return typeof(UInt16);
            }
        }

        public int Size
        {
            get { return d3dIndexSize; }
        }

        public Array Lock(bool read)
        {
            D3d.LockFlags d3dLockFlags = D3d.LockFlags.None;
            if (!read)
                d3dLockFlags |= D3d.LockFlags.Discard;

            Array lockedArray = d3dIndexBuffer.Lock(0,d3dLockFlags);

            d3dLock = true;
            return lockedArray;
        }

        public void Unlock()
        {
            if (d3dLock)
            {
                d3dIndexBuffer.Unlock();
                d3dLock = false;
            }
        }

        public bool Activate(int channel)
        {
            return d3dManager.SetAsStream( this,channel );
        }

        public bool Activate()
        {
            return Activate(0);
        }

        #endregion

        #region IRestorable Members

        public bool Restore()
        {
            return true;
        }

        #endregion
        
        protected override void ReleaseManaged()
        {
            d3dIndexBuffer.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public static IndexStream Create(GeometryManager manager, int size, bool isLarge, bool dynamic )
        {
            IndexStream newStream = new IndexStream();
            if ( newStream.Initialize( manager,size,isLarge,dynamic ) )
                return newStream;

            return null;
        }

        private bool Initialize(GeometryManager manager,int size, bool isLarge, bool dynamic)
        {
          
            try
            {
                D3d.Pool d3dPool = D3d.Pool.Managed;
                D3d.Usage d3dUsage = D3d.Usage.WriteOnly;

                if( dynamic )
                {
                    d3dUsage |= D3d.Usage.Dynamic;
                }

                d3dManager = manager;
                d3dIndexSize = size;
                d3dIsLarge = isLarge;

                d3dIndexBuffer = new D3d.IndexBuffer( this.Type,d3dIndexSize,manager.Device.D3dDevice,
                    d3dUsage,d3dPool );

                return true;
            }
            catch (D3d.InvalidCallException e)
            {
                log.Warning("Unable to create index stream: {0}", e.Message);
            }
            catch (D3d.OutOfVideoMemoryException e)
            {
                log.Warning("Unable to create index stream: {0}", e.Message);
            }
            catch (OutOfMemoryException e)
            {
                log.Warning("Unable to create index stream: {0}", e.Message);
            }
           
            return false;
        }

        private IndexStream()
        {
        }
    }
}
