using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public sealed class Environment : IEnvironment
    {
        private SortedList<string, IActor>  actors      = new SortedList<string, IActor>();
        private List<IConnection>           connections = new List<IConnection>();
        private IActor                      owner       = null;

        #region IEnvironment Members

        public IActor this[string name]
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

        public IActor Owner
        {
            get { return owner; }
        }

        public IActor[] Actors
        {
            get 
            {
                IActor[] tempArray = new IActor[actors.Count];
                actors.Values.CopyTo(tempArray, 0);
                return tempArray;
            }
        }

        public IConnection[] Connections
        {
            get
            {
                return connections.ToArray();
            }
        }

        #region IEnumerable<IActor> Members

        public IEnumerator<IActor> GetEnumerator()
        {
            foreach (IActor a in actors.Values )
                yield return a;
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (IActor a in actors.Values )
                yield return a;
        }

        #endregion

        public bool Add(IActor actor)
        {
            if (!actors.ContainsKey(actor.Name))
            {
                actors.Add(actor.Name, actor);
            }
            return false;
        }

        public bool Add(IConnection connection)
        {
            Remove(connection);
            connections.Add(connection);
            return true;
        }

        public void Remove(IActor actor)
        {
            if (actors.ContainsKey(actor.Name))
            {
                actors.Remove(actor.Name);
            }
        }

        public void Remove(IConnection connection)
        {
            connections.Remove(connection);
        }

        #endregion

        #region ICollection<IActor> Members

        public void Clear()
        {
            foreach (IActor a in actors.Values )
            {
                a.Dispose();
            }
            foreach (IConnection c in connections)
            {
                c.Dispose();
            }

            actors.Clear();
            connections.Clear();
        }

        public bool Contains(IActor item)
        {
            return actors.ContainsKey(item.Name);
        }

        public void CopyTo(IActor[] array, int arrayIndex)
        {
            actors.Values.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return actors.Count; }
        }

        public bool IsReadOnly
        {
            get { return actors.Values.IsReadOnly; }
        }

        #endregion    

        public Environment(IActor _owner)
        {
            owner = _owner;
        }
    }
}
