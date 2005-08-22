using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Interface | 
                    AttributeTargets.Method | 
                    AttributeTargets.Event |
                    AttributeTargets.Property )]
    public sealed class PlugAttribute : System.Attribute
    {
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public PlugAttribute()
        {
        }

        public PlugAttribute(string _name)
        {
            name = _name;
        }
    }
}
