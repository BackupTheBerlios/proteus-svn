using System;
using System.Collections.Generic;
using System.Text;

using Dp = WeifenLuo.WinFormsUI;

namespace Proteus.Editor.Controls
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private List<DockControl>                       dockedControls  = new List<DockControl>();
        private Kernel.Pattern.TypeFactory<DockControl> controlFactory  = new Kernel.Pattern.TypeFactory<DockControl>();
        private WeifenLuo.WinFormsUI.DockPanel          dockPanel       = null;

        public WeifenLuo.WinFormsUI.DockPanel DockPanel
        {
            set { dockPanel = value; }
        }

        public void Add(DockControl control)
        {
            
        }

        public DockControl Create(string name)
        {
            DockControl newControl = controlFactory.Create( name );
            if (newControl != null)
            {
                // Check if multiple instances are allowed.
                if (!newControl.AllowsMultiple)
                {
                    foreach (DockControl d in dockedControls)
                    {
                        if ( d.GetType() == newControl.GetType() )
                            return null;
                    }
                }

                // We can allow it to be created so add it to the default content pane.

                return newControl;
            }

            return null;
        }
    }
}
