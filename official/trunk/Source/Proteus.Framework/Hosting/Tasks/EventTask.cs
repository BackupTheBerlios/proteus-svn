using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Hosting.Tasks
{
    public sealed class EventTask : Kernel.Pattern.Disposable,ITask
    {
        #region ITask Members

        public bool Initialize(Engine engine)
        {
            return true;
        }

        public bool Update(double deltaTime)
        {
            System.Threading.Thread.Sleep(0);
            System.Windows.Forms.Application.DoEvents();
            return true;
        }

        #endregion

        protected override void ReleaseManaged()
        {
        }

        protected override void ReleaseUnmanaged()
        {
        }
    }
}
