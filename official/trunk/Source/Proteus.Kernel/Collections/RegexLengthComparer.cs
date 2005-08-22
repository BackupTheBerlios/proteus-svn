using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Proteus.Kernel.Collections
{
    public sealed class RegexLengthComparer : IComparer<Regex>
    {
        private int multiplyer = 1;

        #region IComparer<Regex> Members

        public int Compare(Regex x, Regex y)
        {
            return multiplyer * x.ToString().Length.CompareTo( y.ToString().Length );
        }

        #endregion

        public RegexLengthComparer()
        {
        }

        public RegexLengthComparer(bool ascending)
        {
            if (ascending)
                multiplyer = -1;
        }
    }
}
