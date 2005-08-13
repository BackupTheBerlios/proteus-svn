using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class GeometryManager : Kernel.Pattern.Disposable,IRestorable
    {
        private Device              geometryDevice = null;
        
        private List<VertexStream>  geometryVertexStreams = 
            new List<VertexStream>();
        
        private List<IndexStream>   geometryIndexStreams = 
            new List<IndexStream>();

        public Device Device
        {
            get { return geometryDevice; }
        }

        public VertexStream CreateVertexStream(Type vertexType, int size, bool pointsprites,bool read)
        {
            VertexStream newStream = VertexStream.Create( this,vertexType,size,pointsprites,read );
            if (newStream != null)
            {
                geometryVertexStreams.Add( newStream );
                return newStream;
            }
            return null;
        }

        public IndexStream CreateIndexStream(int size, bool isLarge,bool read)
        {
            IndexStream newStream = IndexStream.Create(this,size,isLarge,read);
            if (newStream != null)
            {
                geometryIndexStreams.Add( newStream );
                return newStream;
            }
            return null;
        }

        public bool SetAsStream(VertexStream stream, int channel)
        {
            geometryDevice.D3dDevice.SetStreamSource( channel,stream.VertexBuffer,0 );
            return true;
        }

        public bool SetAsStream(IndexStream stream, int channel)
        {
            geometryDevice.D3dDevice.Indices = stream.IndexBuffer;
            return true;
        }

        #region IRestorable Members
        
        public bool Restore()
        {
            foreach (VertexStream v in geometryVertexStreams)
            {
                if ( !v.Restore() )
                    return false;
            }
            foreach (IndexStream i in geometryIndexStreams)
            {
                if ( !i.Restore() )
                    return false;
            }
            return true;
        }

        #endregion

        protected override void ReleaseManaged()
        {
            // Clear all vertex streams.
            for (int i = 0; i < geometryDevice.D3dDevice.DeviceCaps.MaxStreams; i++)
            {
                geometryDevice.D3dDevice.SetStreamSource(i, null, 0);
            }
            geometryDevice.D3dDevice.Indices = null;

            // Release them all.
            foreach (VertexStream v in geometryVertexStreams)
                v.Dispose();
            foreach (IndexStream i in geometryIndexStreams)
                i.Dispose();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public static GeometryManager Create(Device _device)
        {
            GeometryManager geometryManager = new GeometryManager();
            if ( geometryManager.Initialize(_device) )
                return geometryManager;

            return null;
        }

        private bool Initialize(Device device)
        {
            geometryDevice = device;
            return true;
        }

        private GeometryManager()
        {
        }
    }
}
