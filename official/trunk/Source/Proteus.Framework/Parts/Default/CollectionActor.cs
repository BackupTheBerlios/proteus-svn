using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Kernel.Configuration;

namespace Proteus.Framework.Parts.Default
{
    public abstract class CollectionActor : Actor,IActorCollection
    {
        protected Environment collectionEnvironment = null;

        private static Kernel.Diagnostics.Log<CollectionActor> log =
            new Kernel.Diagnostics.Log<CollectionActor>();

        #region IActorCollection Members

        public virtual IActor this[string name]
        {
            get { return collectionEnvironment[name]; }
        }

        public virtual IActor this[int index]
        {
            get { return collectionEnvironment[index]; }
        }

        public virtual int Count
        {
            get { return collectionEnvironment.Count; }
        }

        public virtual IEnumerator<IActor> GetEnumerator()
        {
            return collectionEnvironment.GetEnumerator();
        }

        public virtual bool Add(IActor actor)
        {
            return collectionEnvironment.Add( actor );
        }

        public virtual bool Remove(IActor actor)
        {
            return collectionEnvironment.Remove(actor);
        }

        public void Clear()
        {
            collectionEnvironment.Clear();
        }

        public bool IsCompatible(IActor actor)
        {
            return collectionEnvironment.IsCompatible(actor);
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
