using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts
{
    public class MessagePrototype
    {
        private string  name            = string.Empty;
        private Type    returnType      = null;
        private Type[]  parameterTypes  = null;

        public override bool Equals(object obj)
        {
            MessagePrototype other = (MessagePrototype)obj;

            if ( name != other.name )
                return false;

            if (!returnType.Equals(other.returnType))
            {
                return false;
            }
            else
            {
                if (parameterTypes.Length != other.parameterTypes.Length)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < parameterTypes.Length; i++)
                    {
                        if ( !parameterTypes[i].Equals( other.parameterTypes[i] ) )
                            return false;
                    }
                }
            }

            return true;
        }

        public static MessagePrototype Create( string _name,MethodInfo minfo )
        {
            MessagePrototype newPrototype = new MessagePrototype();

            // Build up prototype.
            newPrototype.parameterTypes = new Type[minfo.GetParameters().Length];
            for (int i = 0; i < newPrototype.parameterTypes.Length; i++)
            {
                newPrototype.parameterTypes[i] = minfo.GetParameters()[i].ParameterType;
            }

            newPrototype.returnType = minfo.ReturnType;
            newPrototype.name = _name;

            return newPrototype;
        }

        private MessagePrototype()
        {
        }
    }
}
