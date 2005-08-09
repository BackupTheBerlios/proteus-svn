using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class FrameBuffer : ITarget
    {

        #region IRestorable Members

        public bool Restore()
        {
            return false;
        }

        #endregion
    }
}
