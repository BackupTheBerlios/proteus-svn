using System;
using System.Collections.Generic;
using System.Text;

using D3d = Microsoft.DirectX.Direct3D;

namespace Proteus.Graphics.Hal
{
    public sealed class VertexStream : Kernel.Pattern.Disposable,IHardwareStream 
    {
        private D3d.VertexBuffer                                d3dVertexBuffer = null;
        private Type                                            d3dVertexType   = null;
        private int                                             d3dVertexSize   = 0;
        private bool                                            d3dLock         = false;
        private GeometryManager                                 d3dManager      = null;

        private static  Kernel.Diagnostics.Log<VertexStream>    log = 
                    new Kernel.Diagnostics.Log<VertexStream>();

        public D3d.VertexBuffer VertexBuffer
        {
            get { return d3dVertexBuffer; }
        }

        #region IHardwareStream Members

        public Type Type
        {
            get { return d3dVertexType; }
        }

        public int Size
        {
            get { return d3dVertexSize; }
        }

        public Array Lock(bool read)
        {
            D3d.LockFlags d3dLockFlags = D3d.LockFlags.None;
            if ( !read )
                d3dLockFlags |= D3d.LockFlags.Discard;

            Array lockedArray = d3dVertexBuffer.Lock(0,d3dLockFlags);
        
            d3dLock = true;
            return lockedArray;
        }

        public void Unlock()
        {
            if (d3dLock)
            {
                d3dVertexBuffer.Unlock();
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
            d3dVertexBuffer.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        private bool IsTypeValid( Type t )
        {
            // Check that a type is valid to store in a vertex stream.
            if ( t.IsValueType && !t.IsAbstract && t.IsLayoutSequential )
            {
                return true;
            }
            return false;
        }

        public static VertexStream Create(GeometryManager manager, Type vertexType, int size,bool pointsprites, bool dynamic )
        {
            VertexStream newVertexStream = new VertexStream();
            if ( newVertexStream.Initialize( manager,vertexType,size,pointsprites,dynamic ) )
                return newVertexStream;

            return null;
        }

        private bool Initialize(GeometryManager manager, Type vertexType, int size,bool pointsprites,bool dynamic )
        {
            if (IsTypeValid(vertexType))
            {
                try
                {
                    D3d.Pool d3dPool = D3d.Pool.Managed;
                    D3d.Usage d3dUsage = D3d.Usage.WriteOnly;
               
                    if ( dynamic )
                    {
                        d3dUsage |= D3d.Usage.Dynamic;
                    }

                    if (pointsprites)
                    {
                        d3dUsage |= D3d.Usage.Points;
                    }

                    d3dManager      = manager;
                    d3dVertexType   = vertexType;
                    d3dVertexSize   = size;

                    d3dVertexBuffer = new D3d.VertexBuffer( vertexType, 
                                                            size, 
                                                            manager.Device.D3dDevice, 
                                                            d3dUsage,
                                                            D3d.VertexFormats.None, 
                                                            d3dPool);

                    return true;
                }
                catch (D3d.InvalidCallException e)
                {
                    log.Warning("Unable to create vertex stream: {0}", e.Message);
                }
                catch (D3d.OutOfVideoMemoryException e)
                {
                    log.Warning("Unable to create vertex stream: {0}", e.Message);
                }
                catch (OutOfMemoryException e)
                {
                    log.Warning("Unable to create vertex stream: {0}", e.Message);
                }
           }

           return false;
        }

        private VertexStream()
        {
        }
    }
}
