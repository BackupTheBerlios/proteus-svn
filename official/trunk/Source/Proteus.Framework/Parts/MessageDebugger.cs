using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public sealed class MessageDebugger
    {
        public delegate object DebugMessageDelegate( IActor target,string name,IActor sender,params object[] parameters );

        private bool                                            debuggerActive = false;
        private static bool                                     debuggerDefault = true;
     
        private static Kernel.Diagnostics.Log<MessageDebugger>  log = 
            new Kernel.Diagnostics.Log<MessageDebugger> ();

        public static bool DebuggerDefault
        {
            set { debuggerDefault = value; }
        }

        public bool Active
        {
            set { debuggerActive = value; }
            get { return debuggerActive; }
        }

        public object InterceptMessage(DebugMessageDelegate targetSite,IActor targetActor,string name, IActor sender, params object[] parameters)
        {
            OnEnterMessage( targetActor,name,sender,parameters );

            object result = null;

            if (targetSite != null)
            {
                result = targetSite(targetActor,name, sender, parameters);
            }
            else
            {
                log.MessageContent("Unable to perform message call.");
            }

            OnExitMessage( result );

            return result;
        }

        protected void OnEnterMessage( IActor targetActor,string name, IActor sender, params object[] parameters)
        {
            log.BeginMessage(Proteus.Kernel.Diagnostics.LogLevel.Debug );
            log.MessageContent( "Actor [{0}] sent message [{1}].",sender,name );

            if (targetActor != null)
            {
                log.MessageContent("Actor [{0}] received the message [{1}].", targetActor, name);
            }
            else
            {
                log.MessageContent("Receiver of message could not be determined.");
            }

            for (int i = 0; i < parameters.Length; i++)
            {
                log.MessageContent("Parameter [{0}] was [{1}:{2}].",i,parameters[i].GetType().Name,parameters[i] );
            }
        }

        protected void OnExitMessage(object result)
        {
            if (result != null)
            {
                log.MessageContent("Result was [{0}:{1}].",result.GetType().Name,result );
            }
            else
            {
                log.MessageContent("Result was null, either a void message or the message could not be handled.");
            }

            log.EndMessage();
        }

        public MessageDebugger()
        {
            debuggerActive = debuggerDefault;
        }

        static MessageDebugger()
        {
            debuggerDefault = Kernel.Registry.Manager.Instance.GetValue("Framework.Debug.Messages",false );
        }
    }
}
