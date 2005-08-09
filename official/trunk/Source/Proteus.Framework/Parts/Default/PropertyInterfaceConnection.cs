using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public sealed class PropertyInterfaceConnection : Connection
    {
        protected override void ReleaseManaged()
        {
            PropertyPlug plug = this.Source as PropertyPlug;
            plug.Property.CurrentValue = null;
        }

        public PropertyInterfaceConnection(IOutputPlug _outputPlug, IInputPlug _inputPlug)
            : base( _outputPlug,_inputPlug )
        {
        }
    }
}
