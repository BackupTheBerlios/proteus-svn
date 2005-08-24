using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts
{
    public sealed class MessageTable
    {
        private SortedList<string,MethodInfo>           methods         = new
                SortedList<string,MethodInfo>();

        private SortedList<string,MessagePrototype>     prototypes  = new
                SortedList<string,MessagePrototype>();

        private MessagePrototype[]                      returnArray = null;
        private MessageDebugger.DebugMessageDelegate    realCall    = null;


        public MessagePrototype this[string name]
        {
            get { return prototypes[name]; }
        }

        public MessagePrototype[] Prototypes
        {
            get { return returnArray; }
        }

        public object SendMessage(  IActor target,
                                    string name,
                                    IActor sender, 
                                    object[] parameters)
        {
            if (MessageRegistry.Debugger.Active)
            {
                return MessageRegistry.Debugger.InterceptMessage(realCall,target,name,sender,parameters );
            }
            else
            {
                return CallMethod( target,name,sender,parameters );
            }
        }

        private object CallMethod(IActor target, string name, IActor sender, object[] parameters)
        {
            MethodInfo targetMethod = methods[name];
            if (targetMethod != null)
            {
                object[] callParameters = new object[parameters.Length + 1];
                callParameters[0] = sender;
                for (int i = 1; i < callParameters.Length; i++)
                {
                    callParameters[i] = parameters[i - 1];
                }

                return targetMethod.Invoke(target, callParameters);
            }
            return null;
        }

        private void Initialize(IActor actor)
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
                        MessagePrototype newPrototype = MessagePrototype.Create( messageName,m );
                        
                        methods.Add( messageName,m );
                        prototypes.Add( messageName,newPrototype );
                    }
                }
            }  
        
            returnArray = new MessagePrototype[ prototypes.Count ];
            prototypes.Values.CopyTo( returnArray,0 );

            // Handle the dispatch
            realCall = new MessageDebugger.DebugMessageDelegate( this.CallMethod );
        }

        public MessageTable( IActor actor )
        {
            Initialize( actor );
        }
    }
}
