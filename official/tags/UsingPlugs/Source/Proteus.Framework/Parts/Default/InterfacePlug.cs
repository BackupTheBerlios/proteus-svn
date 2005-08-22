using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public sealed class InterfacePlug : InputPlug
    {
        private Type interfaceType = null;

        public Type InterfaceType
        {
            get { return interfaceType; }
            set { interfaceType = value; }
        }

        public static InterfacePlug Create( Type type,IActor owner )
        {
            if ( type.IsInterface && Attribute.GetCustomAttribute(type,typeof(PlugAttribute)) as PlugAttribute != null )
            {
                return new InterfacePlug( type,owner );
            }

            return null;
        }

        public static InterfacePlug[] Enumerate(IActor owner)
        {
            List<InterfacePlug> list = new List<InterfacePlug>();

            foreach (Type t in owner.Interfaces)
            {
                InterfacePlug newPlug = Create(t, owner);
                if (newPlug != null)
                    list.Add(newPlug);
            }

            return list.ToArray();
        }

        private InterfacePlug(Type type, IActor owner)
            : base( type,true,owner )
        {
            interfaceType = type;
        }
    }
}
