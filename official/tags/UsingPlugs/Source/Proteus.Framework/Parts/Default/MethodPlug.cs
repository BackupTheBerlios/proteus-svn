using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public sealed class MethodPlug : InputPlug
    {
        private MethodInfo methodInfo = null;

        public MethodInfo MethodInfo
        {
            get { return methodInfo; }
        }

        public static MethodPlug Create(MethodInfo info,IActor owner)
        {
            if (Attribute.GetCustomAttribute(info, typeof(PlugAttribute)) as PlugAttribute != null)
            {
                return new MethodPlug(info,owner);
            }

            return null;
        }

        public static MethodPlug[] Enumerate(IActor actor)
        {
            List<MethodPlug> list = new List<MethodPlug>();

            foreach (MethodInfo m in actor.GetType().GetMethods())
            {
                MethodPlug methodPlug = Create(m,actor);
                if (methodPlug != null)
                    list.Add(methodPlug);
            }

            return list.ToArray();
        }

        private MethodPlug(MethodInfo info,IActor owner )
            : base( info,true,owner )
        {
            methodInfo = info;
        }
    }
}
