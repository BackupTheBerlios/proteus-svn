using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Proteus.Framework.Parts
{
    public sealed class MessageRouter
    {
        private SortedList<Regex,IActor> routes = 
            new SortedList<Regex,IActor>( new Kernel.Collections.RegexLengthComparer() );

        public bool Add(string pattern, IActor target)
        {
            Regex regex = new Regex(pattern);
        }
    }
}
