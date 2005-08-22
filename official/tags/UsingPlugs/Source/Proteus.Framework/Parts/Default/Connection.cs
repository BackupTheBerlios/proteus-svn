using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public abstract class Connection :  Kernel.Pattern.Disposable,
                                        Parts.IConnection
    {
        protected IOutputPlug   outputPlug  = null;
        protected IInputPlug    inputPlug   = null;

        #region IConnection Members

        public virtual IOutputPlug Source
        {
            get { return outputPlug; }
        }

        public virtual IInputPlug Target
        {
            get { return inputPlug; }
        }

        #endregion

        protected override void ReleaseManaged()
        {
        }

        protected override void ReleaseUnmanaged()
        {
        }

        protected Connection( IOutputPlug _outputPlug,IInputPlug _inputPlug )
        {
            outputPlug = _outputPlug;
            inputPlug = _inputPlug;
        }
    }
}
