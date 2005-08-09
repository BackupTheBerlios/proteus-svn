using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Resource
{
    internal sealed class Entry 
    {
        public Manager.ChangeNotificationDelegate FileChanged;

        private string      url                 = string.Empty;  
        private Item        item                = null;
        
        private bool        isLoaded            = false;
        private Priority    priority            = Priority.Medium;   
        private int         size                = 0;
        private DateTime    lastRequestTime     = DateTime.Now;
        private int         requestCount        = 0;
     

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public int RequestCount
        {
            get { return requestCount; }
            set { requestCount = value; }
        }

        public Item Item
        {
            get { return item; }
            set { item = value; }
        }

        public Priority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public bool IsLoaded
        {
            get { return isLoaded; }
            set { isLoaded = value; }
        }

        public int Size
        {
            get { return size; }
            set { size = value; }
        }

        public DateTime LastRequestTime
        {
            get { return lastRequestTime; }
            set { lastRequestTime = value; }
        }

        public bool OnCreate()
        {
            return item.OnCreate(Url);
        }

        private void OnRequest()
        {
            requestCount++;
            lastRequestTime = DateTime.Now;
        }

        public void OnLoad()
        {
            if (!isLoaded)
            {
                OnRequest();
                size = item.OnLoad(url);
                if (size != -1)
                {
                    isLoaded = true;
                }
            }
        }

        public void OnUnload()
        {
            if (isLoaded)
            {
                size = item.OnUnload();
                if (size != -1)
                {
                    isLoaded = false;
                }
            }
        }

        public void OnRelease()
        {
            if (!isLoaded)
            {
                item.Dispose();
            }
        }

        public void OnChange()
        {
            if (FileChanged != null)
            {
                FileChanged();
            }
        }
    }
}
