using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Collections
{
    public sealed class StringLengthComparer : IComparer<string>
    {
        private int multiplyer = 1;

        #region IComparer<string> Members

        public int Compare(string x, string y)
        {
            return multiplyer * x.Length.CompareTo(y.Length);
        }

        #endregion

        public StringLengthComparer()
        {
        }

        public StringLengthComparer(bool ascending)
        {
            if (ascending)
                multiplyer = -1;
        }
    }
}
