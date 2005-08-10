using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Plugin
{
    public sealed class RenderTask : Kernel.Pattern.Disposable,Framework.Hosting.ITask
    {      
        #region ITask Members

        public bool Initialize(Proteus.Framework.Hosting.Engine engine)
        {
            return Hal.Manager.Instance.Initialize(engine);
        }

        public bool Update(double deltaTime)
        {
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
