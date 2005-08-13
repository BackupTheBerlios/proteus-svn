using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Graphics.Hal
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {       
        private static  Kernel.Diagnostics.Log<Manager>  log =
                    new Kernel.Diagnostics.Log<Manager>();

        private TextureManager  deviceTextureManager    = null;
        private GeometryManager deviceGeometryManager   = null;
        private Device          device                  = null;

        public Device Device
        {
            get { return device; }
        }

        public GeometryManager GeometryManager
        {
            get { return deviceGeometryManager; }
        }

        public TextureManager TextureManager
        {
            get { return deviceTextureManager; }
        }

        public bool Initialize(Framework.Hosting.Engine engine)
        {
            // Create rendering device wrapper.
            device = Device.Create( (System.Windows.Forms.Control)engine.Input[Proteus.Framework.Hosting.Input.InputType.MainWindow] );

            if (device != null)
            {
                deviceTextureManager = TextureManager.Create( device );

                if (deviceTextureManager != null)
                {
                    deviceGeometryManager = GeometryManager.Create( device );

                    if (deviceGeometryManager != null)
                    {
                        return true;
                    }
                }
            }
        
            return false;
        }
    }
}
