using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Registry
{
    public class Manager : Pattern.Singleton<Manager>
    {
        public delegate void RegistryChangedDelegate();

        public event RegistryChangedDelegate RegistryChanged;

        private Resource.Handle<Configuration.XmlDocument> documentHandle =
            new Resource.Handle<Configuration.XmlDocument>();

        private static Diagnostics.Log<Manager> log = new Diagnostics.Log<Manager>();

        public string Url
        {
            get { return documentHandle.Url; }
            set 
            { 
                documentHandle.Url = value; 
            }
        }

        public ValueType GetValue<ValueType>(string name, ValueType def)
        {
            string valueName = string.Empty;
            Configuration.Chunk chunk = FindSectionChunk(name, ref valueName);

            if (valueName != string.Empty && chunk != null)
            {
                return chunk.GetValue(valueName, def);
            }

            return def;
        }

        public void SetValue(string name, object value)
        {
            string subChunkName = string.Empty;
            Configuration.Chunk sectionChunk = FindSectionChunk(name, ref subChunkName);

            if (sectionChunk != null && subChunkName != string.Empty)
            {
                sectionChunk.SetValue(subChunkName, value);
            }
            else
            {
                // Create the chunk now and recurse.
            }
        }

        public Configuration.Chunk[] GetMultipleEntry(string name)
        {
            string subChunkName = string.Empty;
            Configuration.Chunk sectionChunk = FindSectionChunk(name, ref subChunkName);

            if (sectionChunk != null && subChunkName != string.Empty)
            {
                return sectionChunk.GetChildrenByName(subChunkName);
            }

            return new Configuration.Chunk[0];
        }

        private Configuration.Chunk FindSectionChunk(string name,ref string valueName )
        {
            // First check that the resource is really here.
            if (documentHandle.IsValid)
            {
                if ( documentHandle.Resource.RootChunk != null )
                {
                    if (documentHandle.Resource.RootChunk.Name == "Registry")
                    {
                        // Really search for it.
                        string[] parts = name.Split(new char[] { '.' });
                        if (parts.Length > 1)
                        {
                            string[] sectionParts = new string[parts.Length - 1];
                            for (int i = 0; i < parts.Length - 1; i++)
                            {
                                sectionParts[i] = parts[i];
                            }

                            valueName = parts[parts.Length - 1];
                            Configuration.Chunk currentChunk = documentHandle.Resource.RootChunk;

                            // Now recurse.
                            for (int i = 0; i < sectionParts.Length; i++)
                            {
                                currentChunk = currentChunk[sectionParts[i]];

                                if (currentChunk == null)
                                    break;
                            }

                            return currentChunk;
                        }
                    }
                }
            }
            else
            {
                log.Warning("Registry file not found.");
                
                if ( documentHandle.Url != string.Empty )
                {
                    log.Info("Creating new registry file [{0}]", documentHandle.Url);

                    // Create an empty document.
                    Configuration.XmlDocument newDocument =
                        new Configuration.XmlDocument();

                    newDocument.RootChunk = new Configuration.Chunk("Registry");

                    // Write it out.
                    newDocument.Write(documentHandle.Url);
                }
               
            }
            return null;
        }

        public void Write()
        {
            string writeUrl = documentHandle.Url;

            // Create new document.
        }

        private void documentHandle_ResourceChanged(Proteus.Kernel.Resource.Handle<Proteus.Kernel.Configuration.XmlDocument> handle)
        {
            if (RegistryChanged != null)
                RegistryChanged();
        }

        public Manager()
        {
            documentHandle.ResourceChanged += new Proteus.Kernel.Resource.Handle<Proteus.Kernel.Configuration.XmlDocument>.ResourceChangedDelegate<Proteus.Kernel.Configuration.XmlDocument>(documentHandle_ResourceChanged);
        }      
    }
}
