using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public static class MessageRegistry
    {
        private static      SortedList<string,MessageTable> messageTables = 
                        new SortedList<string,MessageTable>();

        private static      MessageDebugger                 messageDebugger = 
                        new MessageDebugger();

        public static MessageDebugger Debugger
        {
            get { return messageDebugger; }
        }

        public static MessageTable Initialize(IActor actor)
        {
            if (messageTables.ContainsKey(actor.TypeName))
            {
                return messageTables[actor.TypeName];
            }

            // Create a new one.
            MessageTable newTable = new MessageTable( actor );
            messageTables.Add( actor.TypeName,newTable );

            return newTable;
        }
    }
}
