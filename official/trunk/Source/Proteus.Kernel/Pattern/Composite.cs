using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public class Composite : Component
    {
        private SortedList<string, Component> compositeChildren
            = new SortedList<string, Component>();

        public Component this[string name]
        {
            get
            {
                if (compositeChildren.ContainsKey(name))
                {
                    return compositeChildren[name];
                }
                return null;
            }
        }

        public IEnumerator<Component> GetEnumerator()
        {
            foreach (Component c in compositeChildren.Values)
            {
                yield return c;
            }
        }

        public void AddChild(Component child)
        {
            compositeChildren.Add(child.Name, child);
        }

        public void RemoveChild(Component child)
        {
            compositeChildren.Remove(child.Name);
        }

        public void RemoveChild(string name)
        {
            compositeChildren.Remove(name);
        }

        public override bool Accept(IVisitor visitor)
        {
            bool result = true;
            
            foreach (Component c in compositeChildren.Values)
            {
                if (!c.Accept(visitor))
                {
                    result = false;
                }
            }

            return result;
        }
    }
}
