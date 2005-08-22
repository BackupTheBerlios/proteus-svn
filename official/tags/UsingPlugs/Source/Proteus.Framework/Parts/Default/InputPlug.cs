using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public abstract class InputPlug : Plug,IInputPlug
    {
        #region IInputPlug Members

        public virtual bool OnConnection(IConnection connection )
        {
            this.connections.Add(connection);
            return true;
        }

        #endregion

        protected InputPlug(string _name, string _description, string _documentation, bool _isMultiplex, IActor actor)
            : base( _name,_description,_documentation,_isMultiplex,actor )
        {
        }

        protected InputPlug(Type type, bool _isMultiplex, IActor actor)
            : base ( type,_isMultiplex,actor )
        {
        }

        protected InputPlug(MemberInfo info,bool _isMultiplex,IActor actor )
            : base( info,_isMultiplex,actor )
        {
        }
    }
}
