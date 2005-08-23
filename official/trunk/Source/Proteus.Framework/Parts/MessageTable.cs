using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts
{
    public sealed class MessageTable
    {
        private bool                            isInitialized   = false;
        private SortedList<string,MethodInfo>   methods         = new
                SortedList<string,MethodInfo>();

        public string[] GetHandledMessages(IActor actor)
        {
            Initialize( actor );
            
            string[] messageNames = new string[ methods.Count ];
            methods.Keys.CopyTo( messageNames,0 );
            
            return messageNames;
        }

        public object SendMessage(  IActor target, 
                                    string name,
                                    IActor sender, 
                                    object[] parameters)
        {
            Initialize( target );

            MethodInfo targetMethod = methods[name];
            if (targetMethod != null)
            {
                object[] callParameters = new object[parameters.Length +1];
                callParameters[0] = sender;
                for (int i = 1; i < callParameters.Length; i++)
                {
                    callParameters[i] = parameters[i -1];
                }

                return targetMethod.Invoke( target,callParameters );
            }
            return null;
        }

        private void Initialize(IActor actor)
        {
            if (!isInitialized)
            {
                // First get all public methods.
                MethodInfo[] allMethods = actor.GetType().GetMethods( BindingFlags.Public | BindingFlags.NonPublic );

                foreach (MethodInfo m in allMethods)
                {
                    // Check if its a method handler.
                    MessageHandlerAttribute messageAttribute = Attribute.GetCustomAttribute( m,typeof(MessageHandlerAttribute)) as MessageHandlerAttribute;

                    if (messageAttribute != null)
                    {
                        string messageName = messageAttribute.Name;

                        if (messageName == string.Empty)
                        {
                            messageName = m.Name; 
                        }

                        // Store it.
                        if (!methods.ContainsKey(messageName))
                        {
                            methods.Add( messageName,m );
                        }
                    }
                }
                
                isInitialized = true;
            }
        }

        public MessageTable()
        {
        }
    }
}
