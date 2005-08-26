using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Kernel.Resource;
using Proteus.Kernel.Configuration;

namespace Proteus.Framework.Parts.Basic
{
    [Actor("ConfigFile","Group")]
    [Documentation("Configuration file","An automatic reference to a secondary configuration file.")]
    public class ConfigFileActor : GroupActor
    {
        protected       Handle<XmlDocument> configFile =
                  new   Handle<XmlDocument>();

        private static  Kernel.Diagnostics.Log<ConfigFileActor> log =
                  new   Kernel.Diagnostics.Log<ConfigFileActor>();

        [Parts.Property()]
        [Parts.Documentation("The url of the configuration file to work with.","The url to the .actor configuration file to read/write")]
        public string Url
        {
            get { return configFile.Url; }
            set { configFile.Url = value; }
        }

        private void configFile_ResourceChanged(Handle<XmlDocument> handle)
        {
            this.ReadFile();
        }

        public void ReadFile()
        {
            this.Clear();

            if (configFile.IsValid)
            {
                IActor newActor = Utility.ReadActor(configFile.Resource.RootChunk, this.collectionEnvironment);
                if (newActor == null)
                {
                    log.Warning("Unable to create configuration file root actor.");
                }
            }
            else
            {
                log.Warning("No valid configuration file attached: {0}", this.Url);
            }
        }

        public override bool ReadConfiguration(Chunk chunk)
        {
            this.Clear();

            this.actorName = chunk.GetValue("Name", "Unknown");
            Configuration.Broker.ReadConfiguration(this, chunk);
          
            // Now we have the url loaded, read the file.
            ReadFile();

            return true;
        }

        public override bool WriteConfiguration(Chunk chunk)
        {
            Configuration.Broker.WriteConfiguration(this, chunk);
            
            return WriteFile();
        }

        public bool WriteFile()
        {
            // Write out file.
            if (this.collectionEnvironment.Count == 1)
            {
                IActor fileActor = this.collectionEnvironment[0];

                // Create new file with it.
                Chunk actorChunk = Utility.WriteActor(fileActor);

                XmlDocument document = new XmlDocument();
                document.RootChunk = actorChunk;
                document.Write(this.Url);
                return true;
            }

            return true;
        }

        public ConfigFileActor()
        {
            configFile.ResourceChanged += new Handle<XmlDocument>.ResourceChangedDelegate<XmlDocument>(configFile_ResourceChanged);
        }      
    }
}
