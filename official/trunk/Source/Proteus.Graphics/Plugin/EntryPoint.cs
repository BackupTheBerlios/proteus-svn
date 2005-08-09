using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Plugin
{
    public sealed class EntryPoint : Kernel.Extension.IPlugin
    {
        #region IPlugin Members

        public string Name
        {
            get { return "Proteus.Graphics"; }
        }

        public string Description
        {
            get { return "Direct3D 9.0c based graphics engine."; }
        }

        public string Copyright
        {
            get { return "(c) 2005 by the Proteus Team, released under GPL."; }
        }

        public bool OnLoad(Proteus.Kernel.Information.License license, Proteus.Kernel.Information.Version version, Proteus.Kernel.Information.Platform platform)
        {
            if (Hal.DeviceUtility.TestMdxPrescense())
            {
                // Register rendering task.
                Framework.Hosting.Engine.Instance.Tasks.Enqueue(new RenderTask());

                return true;
            }
            return false;
        }

        public bool OnUnload(Proteus.Kernel.Information.License license, Proteus.Kernel.Information.Version version, Proteus.Kernel.Information.Platform platform)
        {
            return true;
        }

        #endregion
    }
}
