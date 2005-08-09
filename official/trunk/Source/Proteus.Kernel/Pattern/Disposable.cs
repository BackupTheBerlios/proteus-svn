using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public abstract class Disposable : IDisposable
    {
        private bool isDisposed;

        protected abstract void ReleaseManaged();
        protected abstract void ReleaseUnmanaged();
      
        private void Dispose(bool disposing)
        {
            if (!isDisposed)
            {
                if (disposing)
                {
                    GC.SuppressFinalize(this);
                    isDisposed = true;
                    
                    ReleaseManaged();
                }

                ReleaseUnmanaged();
            }
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        ~Disposable()
        {
            this.Dispose(false);
        }
    }
}
