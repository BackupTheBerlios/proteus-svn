using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public sealed class EventMethodConnection : Connection
    {
        private System.Delegate connectionDelegate = null;

        protected override void ReleaseManaged()
        {
            EventPlug eventPlug = this.Source as EventPlug;

            eventPlug.EventInfo.RemoveEventHandler(eventPlug.Owner, connectionDelegate);
        }

        public EventMethodConnection(IOutputPlug _outputPlug, IInputPlug _inputPlug,System.Delegate _delegate)
            : base( _outputPlug,_inputPlug )
        {
            connectionDelegate = _delegate;
        }
    }
}
