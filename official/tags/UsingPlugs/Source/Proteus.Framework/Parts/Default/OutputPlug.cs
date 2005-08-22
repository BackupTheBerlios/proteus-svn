using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public abstract class OutputPlug : Plug,IOutputPlug
    {
        #region IOutputPlug Members
       
        public virtual bool IsCompatible(IInputPlug inputPlug)
        {
            return false;
        }

        public virtual IConnection Connect(IInputPlug inputPlug)
        {
            return null;
        }

        #endregion

        protected OutputPlug(string _name, string _description, string _documentation, bool _isMultiplex, IActor actor)
            : base( _name,_description,_documentation,_isMultiplex,actor )
        {
        }
  
        protected OutputPlug(Type type, bool _isMultiplex, IActor actor)
            : base ( type,_isMultiplex,actor )
        {
        }

        protected OutputPlug(MemberInfo info,bool _isMultiplex,IActor actor )
            : base( info,_isMultiplex,actor )
        {
        }
    }
}
