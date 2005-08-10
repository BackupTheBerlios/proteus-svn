using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Kernel.Io
{
    public class FileArchive : Archive
    {
        private         FileSystemWatcher               fileWatcher = null;
        private static  Diagnostics.Log<FileArchive>    log         = new Diagnostics.Log<FileArchive>();


        public override string[] GetDirectories(string searchDirectory)
        {
            string nativePath = GetPath(searchDirectory);
            string[] nativeDirectories = Directory.GetDirectories(nativePath);
            string[] directories = new string[nativeDirectories.Length];

            for (int i = 0; i < nativeDirectories.Length; i++)
            {
                directories[i] = nativeDirectories[i].Substring(nativePath.Length);
            }

            return directories;
        }

        public override string[] GetFiles(string searchDirectory)
        {
            string   nativePath = GetPath(searchDirectory);
            string[] nativeFiles = Directory.GetFiles(nativePath);
            string[] fileNames = new string[nativeFiles.Length];
            
            for (int i = 0; i < nativeFiles.Length; i++)
            {
                fileNames[i] = nativeFiles[i].Substring(nativePath.Length);
            }

            return fileNames;
        }

        public override System.IO.Stream Open(string fileName, bool write)
        {
            try
            {
                if (write)
                {
                    FileStream file = new FileStream(GetPath(fileName), FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
                    return file;
                }
                else
                {
                    FileStream file = new FileStream(GetPath(fileName), FileMode.Open, FileAccess.Read, FileShare.Read);
                    return file;
                }
            }
            catch (System.IO.FileNotFoundException e)
            {
                log.Warning("File not found [{0}]", e.FileName);
                return null;
            }
        }

        public override bool Exists(string fileName)
        {
            if (File.Exists(fileName) || Directory.Exists(fileName))
                return true;

            return false;
        }

        public override bool Initialize(string initUrl, string mountPoint)
        {
            base.Initialize(initUrl, mountPoint);
            fileWatcher = new FileSystemWatcher(initUrl);
            fileWatcher.Filter = "";
            fileWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileWatcher.Deleted += new FileSystemEventHandler(fileWatcher_Deleted);
            fileWatcher.Changed += new FileSystemEventHandler(fileWatcher_Changed);
            return true;
        }

        private void fileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            // Find the relative url.
            string relativeUrl = GetRelativePath(e.FullPath);
            OnFileChanged(relativeUrl);
        }

        private void fileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            string relativeUrl = GetRelativePath(e.FullPath);
            OnFileRemoved(relativeUrl);
        }

        protected override void ReleaseManaged()
        {
            if (fileWatcher != null)
            {
                fileWatcher.Dispose();
            }
        }

        protected override void ReleaseUnmanaged()
        {
        }

        private string GetPath(string relativeUrl)
        {
            return this.InitUrl + relativeUrl;
        }

        private string GetRelativePath(string absoluteUrl)
        {
            return absoluteUrl.Substring(this.InitUrl.Length);
        }
    }
}
