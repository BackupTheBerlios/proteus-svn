using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public sealed class EventPlug : OutputPlug
    {
        private EventInfo eventInfo = null;

        public EventInfo EventInfo
        {
            get { return eventInfo; }
        }

        public override bool IsCompatible(IInputPlug inputPlug)
        {
            MethodPlug methodPlug = inputPlug as MethodPlug;

            if (methodPlug != null)
            {
                if (Equals(eventInfo.GetRaiseMethod(), methodPlug.MethodInfo))
                    return true;
            }

            return false;
        }

        public override IConnection Connect(IInputPlug inputPlug)
        {
            if (inputPlug is MethodPlug)
            {
                MethodPlug methodPlug = (MethodPlug)inputPlug;
                MethodInfo methodInfo = methodPlug.MethodInfo;

                Delegate connectionDelegate = null;

                try
                {
                    connectionDelegate = Delegate.CreateDelegate(eventInfo.EventHandlerType, inputPlug.Owner, methodInfo);
                    eventInfo.AddEventHandler(this.Owner, connectionDelegate);
                }
                catch (System.Exception)
                {
                    return null;
                }

                // Create connection.
                EventMethodConnection connection = new EventMethodConnection(this,inputPlug,connectionDelegate);

                if (!inputPlug.OnConnection(connection))
                {
                    // Disconnect.
                    connection.Dispose();
                    return null;
                }
                this.connections.Add(connection);
                return connection;
            }
            return null;
        }
        
        public static EventPlug Create(EventInfo info,IActor owner)
        {
            if ( Attribute.GetCustomAttribute(info, typeof(PlugAttribute)) as PlugAttribute != null )
            {
                return new EventPlug(info,owner);
            }

            return null;
        }

        public static EventPlug[] Enumerate(IActor actor)
        {
            List<EventPlug> list = new List<EventPlug>();

            foreach (EventInfo e in actor.GetType().GetEvents())
            {
                EventPlug plug = Create(e,actor);
                if (plug != null)
                {
                    list.Add(plug);
                }
            }

            return list.ToArray();
        }

        private bool Equals(MethodInfo m1, MethodInfo m2)
        {
            if (m1.GetParameters().Length == m2.GetParameters().Length)
            {
                if (m1.ReturnType.Equals(m2.ReturnType))
                {
                    for (int i = 0; i < m1.GetParameters().Length; i++)
                    {
                        ParameterInfo p1 = m1.GetParameters()[i];
                        ParameterInfo p2 = m2.GetParameters()[i];

                        if (!p1.ParameterType.Equals(p2.ParameterType))
                        {
                            return false;
                        }

                        if (p1.IsIn != p2.IsIn || p1.IsOut != p2.IsOut)
                            return false;

                    }

                    return true;
                }
                return false;
            }
            return false;
        }

        private EventPlug(EventInfo info,IActor owner )
            : base( info,true,owner )
        {
            eventInfo = info;
        }
    }
}
