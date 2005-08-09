using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Kernel.Configuration
{
    /// <summary>
    /// Base class for all configuration documents.
    /// </summary>
    public abstract class Document : Resource.Item
    {
        private Chunk documentRoot = null;

        public Chunk RootChunk
        {
            set { documentRoot = value; }
            get { return documentRoot; }
        }

        public abstract void Write( System.IO.Stream stream);
        public abstract bool Read( System.IO.Stream stream);

        public virtual void Write(string url)
        {
            Stream writeStream = Io.Manager.Instance.Open(url, true);
            if (writeStream != null)
            {
                this.Write(writeStream);
                writeStream.Close();
            }
        }

        public override int OnLoad(string url)
        {
            Stream loadStream = Io.Manager.Instance.Open(url);
            int size = 0;
            if (loadStream != null)
            {
                size = (int)loadStream.Length;
                if (!this.Read(loadStream))
                {
                    size = -1;
                }
                loadStream.Close();
            }
            else
            {
                size = -1;
            }

            return size;
        }

        public override int OnUnload()
        {
            documentRoot = null;
            return 0;
        }
    }
}
