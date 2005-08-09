using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Resource
{
    public sealed class Manager : Pattern.Singleton<Manager>
    {
        public delegate void ChangeNotificationDelegate();

        private SortedList<string, Entry>   entries         = new SortedList<string, Entry>();
        private int                         memoryBudget    = 256000000;
        private int                         memoryUsage     = 0;

        public ResourceType Request<ResourceType>(string url,Priority priority ) where ResourceType : Item, new()
        {
            return Require<ResourceType>(url,priority );
        }

        public ResourceType Require<ResourceType>(string url,Priority priority ) where ResourceType : Item, new()
        {
            CollectGarbage();

            if (entries.ContainsKey(url))
            {
                Entry foundEntry = entries[url];

                foundEntry.Priority = priority;

                memoryUsage -= foundEntry.Size;
                foundEntry.OnLoad();
                memoryUsage += foundEntry.Size;

                return (ResourceType)foundEntry.Item;
            }
            else
            {
                if ( Create<ResourceType>(url,priority ) )
                    return Require<ResourceType>(url,priority );
            }

            return null;
        }

        public void AddChangeNotification( string url,ChangeNotificationDelegate handler )
        {
            if (entries.ContainsKey(url))
            {
                Entry entry = entries[url];
                entry.FileChanged += handler;
            }
        }

        public void RemoveChangeNotification(string url, ChangeNotificationDelegate handler)
        {
            if (entries.ContainsKey(url))
            {
                Entry entry = entries[url];
                entry.FileChanged -= handler;
            }
        }

        private void Instance_FileChanged(Proteus.Kernel.Io.Archive archive, string filename)
        {
            if (entries.ContainsKey(filename))
            {
                Entry entry = entries[filename];

                // Fully remove the item and recreate it.
                Recreate(entry);

                // Fire change notification.
                entry.OnChange();
            }
        }

        private void Recreate(Entry entry) 
        {
            entry.OnRelease();
            entry.OnCreate();
        }

        private bool Create<ResourceType>(string url,Priority priority ) where ResourceType : Item, new()
        {      
            Entry newEntry = new Entry();
            ResourceType newResource = new ResourceType();

            newEntry.Url = url;
            newEntry.Item = newResource;
            newEntry.Priority = priority;

            if (newEntry.OnCreate())
            {
                entries.Add(url, newEntry);
                return true;
            }

            return false;
        }

        private void CollectGarbage()
        {
            while (memoryUsage > memoryBudget)
            {
                Entry releaseCandidate = FindBestUnloadCandidate();

                if (releaseCandidate != null)
                {
                    memoryUsage -= releaseCandidate.Size;
                    releaseCandidate.OnUnload();
                    memoryUsage += releaseCandidate.Size;
                }
                else
                {
                    break;
                }
            }
        }

        private Entry FindBestUnloadCandidate()
        {
            Entry releaseEntry = null;

            foreach (Entry e in entries.Values)
            {
                if (e.IsLoaded && e.Priority < Priority.Critical )
                {
                    if (releaseEntry == null)
                    {
                        releaseEntry = e;
                    }
                    else
                    {
                        if (e.Priority < releaseEntry.Priority)
                        {
                            releaseEntry = e;
                        }
                        else if (e.Priority == releaseEntry.Priority)
                        {
                            // Go down further.
                            if (e.RequestCount < releaseEntry.RequestCount)
                            {
                                releaseEntry = e;
                            }
                            else if (e.RequestCount == releaseEntry.RequestCount)
                            {
                                if (e.LastRequestTime < releaseEntry.LastRequestTime)
                                {
                                    releaseEntry = e;
                                }
                                else if ( e.LastRequestTime == releaseEntry.LastRequestTime )
                                {
                                    if (e.Size > releaseEntry.Size)
                                    {
                                        releaseEntry = e;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return releaseEntry;
        }

        public Manager()
        {
            // Register change callback.
            Io.Manager.Instance.FileChanged += new Proteus.Kernel.Io.Archive.FileChangedDelegate(Instance_FileChanged);
        }
    }
}
