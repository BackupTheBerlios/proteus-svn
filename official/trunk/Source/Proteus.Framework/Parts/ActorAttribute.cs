using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ActorAttribute : System.Attribute
    {
        private string name     = string.Empty;
        private string baseName = string.Empty;

        public string Name
        {
            get { return name; }
        }

        public string BaseName
        {
            get { return baseName; }
        }

        public ActorAttribute(string _name, string _baseName)
            : this(_name)
        {
            baseName = _baseName;
        }

        public ActorAttribute(string _name)
        {
            name = _name;
        }
    }
}
