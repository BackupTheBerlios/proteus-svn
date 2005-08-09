using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Kernel.Extension
{
    public sealed class PluginLoader
    {
        private SortedList<string,Assembly>             loadedAssemblies    = new SortedList<string,Assembly>();
        private static Diagnostics.Log<PluginLoader>    log 
                 = new Diagnostics.Log<PluginLoader>();

        public IEnumerator<IPlugin> GetEnumerator()
        {
            foreach (Assembly a in loadedAssemblies.Values)
            {
                yield return GetPluginInterface(a);
            }
        }

        private Assembly LoadAssembly(System.IO.Stream stream)
        {
            long streamLength = stream.Length;
            byte[] assemblyBuffer = new byte[streamLength];
            stream.Read(assemblyBuffer, 0, (int)streamLength);

            return Assembly.Load(assemblyBuffer);
        }

        private IPlugin GetPluginInterface(Assembly assembly)
        {
            if (assembly != null)
            {
                PluginAttribute attribute = System.Attribute.GetCustomAttribute(assembly, typeof(PluginAttribute)) as PluginAttribute;

                if (attribute != null)
                {
                    if (attribute.Type != null)
                    {
                        // Walk it.
                        IPlugin pluginInterface = (IPlugin)Activator.CreateInstance(attribute.Type);
                        return pluginInterface;
                    }
                }
            }

            return null;
        }

        public void Load()
        {
            Configuration.Chunk[] pluginChunks = Registry.Manager.Instance.GetMultipleEntry("Framework.Plugins.Plugin");
            foreach (Configuration.Chunk p in pluginChunks)
            {
                string pluginUrl = p.GetValue("Url", string.Empty);
                if (pluginUrl != string.Empty)
                {
                    Load(pluginUrl);
                }
            }
        }

        public void Load(string url)
        {
            System.IO.Stream loadStream = Io.Manager.Instance.Open(url);

            if (loadStream != null)
            {
                Assembly pluginAssembly = LoadAssembly(loadStream);
                IPlugin pluginInterface = GetPluginInterface(pluginAssembly);

                if (pluginInterface != null)
                {
                    if (!loadedAssemblies.ContainsKey(pluginInterface.Name))
                    {
                        if (pluginInterface.OnLoad(new Information.License(), new Information.Version(), new Information.Platform()))
                        {
                            loadedAssemblies.Add(pluginInterface.Name, pluginAssembly);

                            log.BeginMessage(Proteus.Kernel.Diagnostics.LogLevel.Info);
                            log.MessageContent("Plugin [{0}] loaded successfully.", url);
                            log.MessageContent("Name: {0}", pluginInterface.Name);
                            log.MessageContent("Description: {0}", pluginInterface.Description);
                            log.MessageContent("Copyright: {0}", pluginInterface.Copyright);
                            log.EndMessage();
                        }
                    }
                }
            }
        }

        public void Unload(string name)
        {
            if (loadedAssemblies.ContainsKey(name))
            {
                IPlugin pluginInterface = GetPluginInterface(loadedAssemblies[name]);

                if (pluginInterface.OnUnload(new Information.License(), new Information.Version(), new Information.Platform()))
                {
                    loadedAssemblies.Remove(name);
                }
            }
        }
    }
}
