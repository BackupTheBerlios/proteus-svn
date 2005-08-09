using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Kernel.Io
{
    public abstract class Archive : Pattern.Disposable
    {
        public delegate void                        FileChangedDelegate( Archive archive,string filename );

        public event FileChangedDelegate            FileChanged;
        public event FileChangedDelegate            FileRemoved;

        private string                              archiveMountUrl = string.Empty;
        private string                              archiveInitUrl  = string.Empty;

        private static Diagnostics.Log<Archive>     log = new Diagnostics.Log<Archive>();

        public string MountPoint
        {
            get { return archiveMountUrl; }
        }

        public string InitUrl
        {
            get { return archiveInitUrl; }
        }

        public abstract string[]                    GetDirectories(string searchDirectory);
        public abstract string[]                    GetFiles( string searchDirectory );
        public abstract Stream                      Open( string fileName,bool write );
        public abstract bool                        Exists(string fileName);


        public virtual bool Initialize(string initUrl, string mountUrl)
        {
            archiveMountUrl     = mountUrl;
            archiveInitUrl      = initUrl;

            return true;
        }

        protected void OnFileChanged(string url)
        {
            log.Debug("File changed detected: [{0}]", url);

            if (FileChanged != null)
            {
                FileChanged(this, url);
            }
        }

        protected void OnFileRemoved(string url)
        {
            log.Debug("File deletion detected: [{0}]", url);
            
            if (FileRemoved != null)
            {
                FileRemoved(this, url);
            }
        }
    }
}
