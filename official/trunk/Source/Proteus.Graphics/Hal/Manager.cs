using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private Device                                  device = null;
        
        private static Kernel.Diagnostics.Log<Manager>  log =
            new Kernel.Diagnostics.Log<Manager>();

        public bool Initialize(Framework.Hosting.Engine engine)
        {
            // Create rendering device wrapper.
            device = Device.Create( (System.Windows.Forms.Control)engine.Input[Proteus.Framework.Hosting.Input.InputType.MainWindow] );

            if ( device != null )
                return true;
        
            return false;
        }
    }
}
