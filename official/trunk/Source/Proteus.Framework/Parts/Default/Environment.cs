using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public class Environment : IEnvironment
    {
        private SortedList<string, IActor>  actors      = new SortedList<string, IActor>();
        private IActor                      owner       = null;

        #region IEnvironment Members

        public virtual IActor this[string name]
        {
            get
            {
                if (actors.ContainsKey(name))
                {
                    return actors[name];
                }
                return null;
            }
        }

        public virtual IActor this[int index]
        {
            get
            {
                if ( index >= 0 && index < actors.Count )
                    return actors.Values[index];
                return null;
            }
        }

        public virtual int Count
        {
            get { return actors.Count; }
        }

        public virtual IActor Owner
        {
            get { return owner; }
        }

        public virtual IEnumerator<IActor> GetEnumerator()
        {
            foreach (IActor a in actors.Values )
                yield return a;
        }

        public virtual bool IsCompatible(IActor actor)
        {
            return true;
        }
      
        public virtual bool Add(IActor actor)
        {
            if (!actors.ContainsKey(actor.Name))
            {
                actors.Add(actor.Name, actor);
                return true;
            }
            return false;
        }

        public virtual bool Remove(IActor actor)
        {
            if (actors.ContainsKey(actor.Name))
            {
                actors.Remove(actor.Name);
                return true;
            }

            return false;
        }

        public virtual void Clear()
        {
            actors.Clear();
        }

        #endregion

        public Environment(IActor _owner)
        {
            owner = _owner;
        }
    }
}
