using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Kernel.Configuration;

namespace Proteus.Framework.Parts.Default
{
    public abstract class CollectionActor : Actor,IActorCollection
    {
        protected Environment collectionEnvironment = null;

        #region ICollection<IActor> Members

        public void Add(IActor item)
        {
            collectionEnvironment.Add(item);
        }

        public void Clear()
        {
            collectionEnvironment.Clear();
        }

        public bool Contains(IActor item)
        {
            return collectionEnvironment.Contains(item);
        }

        public void CopyTo(IActor[] array, int arrayIndex)
        {
            collectionEnvironment.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get
            {
                return collectionEnvironment.Count;
            }
        }

        public bool IsReadOnly
        {
            get { return collectionEnvironment.IsReadOnly; }
        }

        public bool Remove(IActor item)
        {
            collectionEnvironment.Remove(item);
            return true;
        }

        #endregion

        #region IEnumerable<IActor> Members

        public IEnumerator<IActor> GetEnumerator()
        {
            return collectionEnvironment.GetEnumerator();
        }

        #endregion

        #region IEnumerable Members

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            foreach (IActor a in collectionEnvironment)
            {
                yield return a;
            }
        }

        #endregion

        public override bool ReadConfiguration(Chunk chunk)
        {
            this.Clear();

            bool success = base.ReadConfiguration(chunk);  
                
            foreach( Chunk c in chunk.GetChildrenByName("Actor") )
            {
                Utility.ReadActor(c, collectionEnvironment);
            }
            
            // Now read any connections we have.
            foreach (Chunk c in chunk.GetChildrenByName("Connection") )
            {
                Utility.ReadConnection(c, collectionEnvironment);
            }

            return success;
        }

        public override bool WriteConfiguration(Chunk chunk)
        {
            if (base.WriteConfiguration(chunk))
            {
                foreach (IActor a in collectionEnvironment)
                {
                    Chunk subChunk = Utility.WriteActor(a);
                    if (subChunk != null)
                    {
                        chunk.Add(subChunk);
                    }
                    else
                    {
                        return false;
                    }
                }

                // Connections.
                foreach (IConnection c in collectionEnvironment.Connections)
                {
                    chunk.Add(Utility.WriteConnection(c));
                }

                return true;
            }
            return false;
        }

        protected override bool OnUpdate(double deltaTime)
        {
            bool success = true;

            foreach (IActor a in collectionEnvironment)
            {
                if (!a.Update(deltaTime))
                {
                    success = false;
                }
            }

            return success;
        }

        protected override void ReleaseManaged()
        {
            this.Clear();
        }

        public CollectionActor()
        {
            collectionEnvironment = new Environment(this);
        }
    }
}
