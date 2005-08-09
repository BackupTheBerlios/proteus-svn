using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Resource
{
    public enum Priority
    {
        Low       = 0,
        Medium  = 1,
        High      = 2,
        Critical   = 3,
    }

    public abstract class Item : Pattern.Disposable
    {
        public virtual bool OnCreate(string url)
        {
            return Io.Manager.Instance.Exists(url);
        }

        public abstract int     OnLoad(string url);
        public abstract int     OnUnload();

        protected override void ReleaseManaged()
        {  
        }

        protected override void ReleaseUnmanaged()
        {
        }
    }
}
