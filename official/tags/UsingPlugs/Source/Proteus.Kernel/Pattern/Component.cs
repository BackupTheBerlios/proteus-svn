using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public class Component : IVisitable
    {
        private string componentName = string.Empty;

        public virtual string Name
        {
            get { return componentName; }
            set { componentName = value; }
        }

        public virtual bool Configure(Configuration.Chunk chunk,bool write )
        {
            return true;
        }

        public virtual bool Accept(IVisitor visitor)
        {
            return true;
        }

        public Component()
        {
        }
    }
}
