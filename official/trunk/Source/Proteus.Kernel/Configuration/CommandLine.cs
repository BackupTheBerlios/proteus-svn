using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Configuration
{
    public class CommandLine
    {
        public ValueType GetOption(string name, ValueType def)
        {
            return def;
        }

        public CommandLine()
        {
        }
    }
}
