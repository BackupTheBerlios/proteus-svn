using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public abstract class RenderTextureBase : TextureBase,IRenderTarget
    {      
        #region IRenderTarget Members

        public virtual bool SetAsTarget( int channel,int surface )
        {
            return true;
        }

        #endregion
    }
}
