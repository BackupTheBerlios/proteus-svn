using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Extension
{
    [AttributeUsage( AttributeTargets.Assembly,AllowMultiple = false )]
    public class PluginAttribute : System.Attribute 
    {
        private Type pluginType;

        public Type Type
        {
            get { return pluginType; }
        }

        public PluginAttribute( Type _pluginType )
        {
            // Check correct type requirements, no generics possible.
            if (_pluginType.GetInterface(typeof(IPlugin).FullName) != null)
            {
                if (_pluginType.GetConstructor(System.Type.EmptyTypes) != null)
                {
                    pluginType = _pluginType;
                }
            }
        }
    }
}
