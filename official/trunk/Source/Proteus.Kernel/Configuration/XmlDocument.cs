using System;
using System.Collections.Generic;
using System.Text;

using Xml = System.Xml;

namespace Proteus.Kernel.Configuration
{
    public class XmlDocument : Document
    {
        private static Diagnostics.Log<XmlDocument> log = new Diagnostics.Log<XmlDocument>();

        public override void Write(System.IO.Stream stream)
        {     
            if (this.RootChunk != null)
            {
                Xml.XmlTextWriter writer = new Xml.XmlTextWriter(stream, Encoding.UTF8);
                writer.Formatting = Xml.Formatting.Indented;

                writer.WriteStartDocument();

                WriteStep(writer, this.RootChunk);

                writer.WriteEndDocument();

                writer.Close();
            }
        }

        private void WriteStep(Xml.XmlTextWriter writer, Chunk chunk)
        {
            // First write out the chunk itself.
            writer.WriteStartElement(chunk.Name);

            // Write out any values.
            foreach (string key in chunk.Values.Keys )
            {
                writer.WriteStartElement("Value");
                writer.WriteAttributeString("Name", key);
                writer.WriteString( chunk.Values[key] );
                writer.WriteEndElement();
            }

            // Recurse children.
            foreach (Chunk c in chunk)
            {
                WriteStep(writer, c);
            }

            writer.WriteEndElement();
        }

        private string GetElementText(Xml.XmlElement element)
        {
            foreach (Xml.XmlNode n in element.ChildNodes)
            {
                if (n is Xml.XmlText)
                {
                    Xml.XmlText textNode = (Xml.XmlText)n;
                    return textNode.Value;
                }
            }

            return string.Empty;
        }

        private Chunk ReadStep(Xml.XmlElement element)
        {
            Chunk newChunk = new Chunk(element.Name);

            // Read its values.
            Xml.XmlNodeList valueNodes = element.GetElementsByTagName("Value");

            foreach (Xml.XmlElement e in valueNodes)
            {
                string valueName = e.GetAttribute("Name");
                newChunk.Values[valueName] = this.GetElementText(e);
            }

            // Recurse.
            foreach (Xml.XmlNode n in element.ChildNodes)
            {
                if (n is Xml.XmlElement && n.Name != "Value")
                {
                    Xml.XmlElement subElement = (Xml.XmlElement)n;
                    newChunk.Add(ReadStep(subElement));
                }
            }

            return newChunk;
        }

        public override bool Read(System.IO.Stream stream)
        {
            Xml.XmlDocument xmlDocument = new Xml.XmlDocument();

            try
            {
                xmlDocument.Load(stream);
                this.RootChunk = ReadStep(xmlDocument.DocumentElement);
                if (this.RootChunk != null)
                    return true;
            }
            catch (Xml.XmlException e)
            {
                log.Warning(e.ToString());
            }

            return false;
        }
    }
}
