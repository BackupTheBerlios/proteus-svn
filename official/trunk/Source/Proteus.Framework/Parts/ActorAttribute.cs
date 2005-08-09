using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActorAttribute : System.Attribute
    {
        private string name = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public ActorAttribute(string _name)
        {
            name = _name;
        }
    }
}
