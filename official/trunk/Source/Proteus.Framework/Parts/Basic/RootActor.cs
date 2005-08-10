using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Basic
{
    public sealed class RootActor : ConfigFileActor
    {
        private Hosting.Engine engine = null;

        public void ForceLoad()
        {
            base.LoadFile();
        }

        public RootActor(Hosting.Engine _engine)
        {
            engine = _engine;
        }
    }
}
