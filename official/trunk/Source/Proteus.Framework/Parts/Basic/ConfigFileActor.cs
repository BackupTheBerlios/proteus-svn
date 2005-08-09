using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Kernel.Resource;
using Proteus.Kernel.Configuration;

namespace Proteus.Framework.Parts.Basic
{
    [Actor("ConfigFile")]
    [Documentation("Configuration file","An automatic reference to a secondary configuration file.")]
    public class ConfigFileActor : Default.CollectionActor
    {
        protected   Handle<XmlDocument> configFile =
                new Handle<XmlDocument>();

        [Parts.Property()]
        public string Url
        {
            get { return configFile.Url; }
            set { configFile.Url = value; }
        }

        private void configFile_ResourceChanged(Handle<XmlDocument> handle)
        {
            this.LoadFile();
        }

        protected void LoadFile()
        {
            this.Clear();

            if (configFile.IsValid)
            {
                IActor newActor = Utility.ReadActor(configFile.Resource.RootChunk, this.collectionEnvironment);
                if (newActor != null)
                {
                    if (!newActor.ReadConfiguration(configFile.Resource.RootChunk))
                    {
                    }
                }
            }
        }

        public override bool ReadConfiguration(Chunk chunk)
        {
            this.Clear();

            this.actorName = chunk.GetValue("Name", "Unknown");
            Configuration.Broker.ReadConfiguration(this, chunk);
          
            // Now we have the url loaded, read the file.
            LoadFile();

            return true;
        }

        public override bool WriteConfiguration(Chunk chunk)
        {
            Configuration.Broker.WriteConfiguration(this, chunk);
            
            // Write out file.
            if (this.collectionEnvironment.Count == 1)
            {
                IActor fileActor = this.collectionEnvironment.Actors[0];

                // Create new file with it.
                Chunk actorChunk = Utility.WriteActor(fileActor);

                XmlDocument document = new XmlDocument();
                document.RootChunk = actorChunk;
                document.Write(this.Url);
                return true;
            }

            return false;
        }

        public ConfigFileActor()
        {
            configFile.ResourceChanged += new Handle<XmlDocument>.ResourceChangedDelegate<XmlDocument>(configFile_ResourceChanged);
        }      
    }
}
