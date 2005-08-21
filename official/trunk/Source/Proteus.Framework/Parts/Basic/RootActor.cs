using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Basic
{
    [Actor("Root","ConfigFile")]
    public sealed class RootActor : ConfigFileActor
    {
        private         Hosting.Engine  engine      = null;
        private static  RootActor       instance    = null;

        public static RootActor Instance
        {
            get { return instance; }
        }

        public void ForceLoad()
        {
            base.LoadFile();
        }

        public RootActor(Hosting.Engine _engine)
        {
            engine = _engine;
            actorName = "Root";
            
            if (instance == null)
            {
                instance = this;
            }
        }
    }
}
