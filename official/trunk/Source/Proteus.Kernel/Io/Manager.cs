using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Io
{
    public sealed class Manager : Pattern.Singleton<Manager>
    {
        internal class MountPoint : IComparable<MountPoint>
        {
            public string   url     = string.Empty;
            public Archive  archive = null;

            #region IComparable<MountPoint> Members

            public int CompareTo(MountPoint other)
            {
                return url.Length.CompareTo( other.url.Length );
            }

            #endregion

            public MountPoint(string _url, Archive _archive)
            {
                url = _url;
                archive = _archive;
            }
        }
        
        private Pattern.TypeFactory<Archive> archiveFactory =
            new Pattern.TypeFactory<Archive>();

        private List<MountPoint> mountPoints = new List<MountPoint>();

        private static Diagnostics.Log<Manager> log = new Diagnostics.Log<Manager>();

        public event Archive.FileChangedDelegate FileChanged;
        public event Archive.FileChangedDelegate FileRemoved;

        public Pattern.TypeFactory<Archive> Factory
        {
            get { return archiveFactory; }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach (MountPoint m in mountPoints)
                yield return m.url;
        }

        public string[] GetDirectories(string url)
        {
            if (Exists(url))
            {
                if (Url.IsDirectory(url))
                {
                    string relativeUrl = url;
                    Archive foundArchive = FindArchive(url, ref relativeUrl);

                    if (foundArchive != null)
                    {
                        return foundArchive.GetDirectories(relativeUrl);
                    }
                }
            }

            return new string[0];
        }

        public bool Exists(string url)
        {
            string relativeUrl = url;
            Archive foundArchive = FindArchive(url, ref relativeUrl);

            if (foundArchive != null)
            {
                return foundArchive.Exists(relativeUrl);
            }

            return false;
        }

        public string[] GetFiles(string url)
        {
            if (Exists(url))
            {
                if (Url.IsDirectory(url))
                {
                    string relativeUrl = url;
                    Archive foundArchive = FindArchive(url, ref relativeUrl);

                    if (foundArchive != null)
                    {
                        return foundArchive.GetFiles(relativeUrl);
                    }
                }
            }

            return new string[0];
        }

        public bool CreateDirectory(string url)
        {
            if (Url.IsDirectory(url))
            {
                string relativeUrl = url;
                Archive foundArchive = FindArchive(url, ref relativeUrl);

                if (foundArchive != null)
                {
                    return foundArchive.CreateDirectory( relativeUrl );
                }
            }
            return false;
        }

        public System.IO.Stream Open(string url, bool write)
        {
            log.Debug("Trying to open file [{0}] write: {1}", url, write);
            
            // Find matching archive.
            if (!write)
            {
                if (!Exists(url))
                {
                    log.Warning("Trying to open [{0}] for reading, file does not exist.", url);
                    return null;
                } 
            }

            if (Url.IsFilename(url))
            {
                string relativeUrl = url;
                Archive foundArchive = FindArchive(url, ref relativeUrl);

                if (foundArchive != null)
                {
                    System.IO.Stream stream = foundArchive.Open(relativeUrl, write);

                    if (stream != null)
                    {
                        log.Debug("File [{0}] successfully opened for write: {1}", url,write );
                    }
                    return stream;
                }
                else
                {
                    log.Info("No archive found for url: [{0}][{1}]", url,relativeUrl );
                }
            }
            else
            {
                log.Info("[{0}] is not a file url.", url);
            }
           
            return null;
        }

        public System.IO.Stream Open(string url)
        {
            return Open(url, false);
        }

        public bool Mount(string mountPoint, string archiveType,string initUrl)
        {
            log.Debug("Trying to mount [{0}] at mountpoint [{1}] with init url [{2}]", archiveType, mountPoint, initUrl);

            if (Url.IsDirectory(mountPoint))
            {
                Archive newArchive = archiveFactory.Create(archiveType);
                if (newArchive != null)
                {
                    if (newArchive.Initialize(initUrl, mountPoint))
                    {
                        newArchive.FileChanged += new Archive.FileChangedDelegate(newArchive_FileChanged);
                        newArchive.FileRemoved += new Archive.FileChangedDelegate(newArchive_FileRemoved);
                        
                        mountPoints.Add(new MountPoint(mountPoint, newArchive) );
                        mountPoints.Sort();

                        log.Debug("Mounting was successful.");
                        return true;
                    }
                }
            }

            return false;
        }

        public void Unmount(string mountPoint)
        {
            MountPoint toRemove = null;

            foreach (MountPoint p in mountPoints)
            {
                if (p.url == mountPoint)
                {
                    toRemove = p;
                    break;
                }
            }

            if ( toRemove != null )
            {     
                Archive mountedArchive = toRemove.archive;

                mountedArchive.FileChanged -= new Archive.FileChangedDelegate(newArchive_FileChanged);
                mountedArchive.FileRemoved -= new Archive.FileChangedDelegate(newArchive_FileRemoved);
                
                mountPoints.Remove(toRemove);
                mountedArchive.Dispose();           
            }
        }

        private Archive FindArchive(string url, ref string relativeUrl)
        {
            foreach (MountPoint m in mountPoints)
            {
                if (url.StartsWith(m.url) || m.url == string.Empty )
                {
                    if (m.url != string.Empty)
                    {
                        relativeUrl = url.Replace(m.url, "");
                    }
                    else
                    {
                        relativeUrl = url;
                    }
                    return m.archive;
                }
            }

            return null;
        }

        private string FindUrl(string relativeUrl, Archive archive)
        {
            return archive.MountPoint + relativeUrl;
        }

        private void newArchive_FileRemoved(Archive archive, string filename)
        {
            if (this.FileRemoved != null)
            {
                string url = this.FindUrl(filename, archive);
                this.FileRemoved(archive, url);
            }
        }

        private void newArchive_FileChanged(Archive archive, string filename)
        {
            if (this.FileChanged != null)
            {
                string url = this.FindUrl(filename, archive);
                this.FileChanged(archive, url);
            }
        }

        public Manager()
        {
            // Register default archives.
            archiveFactory.Register<FileArchive>();
            this.Mount("", "Proteus.Kernel.Io.FileArchive", Information.Program.Path );
        }
    }
}
