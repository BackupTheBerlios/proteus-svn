using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public sealed class TimestampInfo : IContextInfo
    {
        #region IContextInfo Members

        public string Name
        {
            get { return "Timestamp"; }
        }

        public string Text
        {
            get { return DateTime.Now.ToString(); }
        }

        #endregion
}
}
