using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Method)]
    public class MessageHandlerAttribute : System.Attribute
    {
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public MessageHandlerAttribute()
        {
        }

        public MessageHandlerAttribute(string _name)
        {
            name = _name;
        }
    }
}
