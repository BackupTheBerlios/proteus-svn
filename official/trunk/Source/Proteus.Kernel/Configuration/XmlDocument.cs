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

        private static void WriteStep(Xml.XmlTextWriter writer, Chunk chunk)
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

        public static string GetChunkXml(Chunk chunk)
        {
            System.IO.StringWriter stringWriter = new
                System.IO.StringWriter();
            Xml.XmlTextWriter writer = new Xml.XmlTextWriter( stringWriter );
            writer.Formatting = Xml.Formatting.Indented;

            WriteStep( writer,chunk );
        
            string content = stringWriter.ToString();

            writer.Close();
            return content;
        }

        private static string GetElementText(Xml.XmlElement element)
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

        private static Chunk ReadStep(Xml.XmlElement element)
        {
            Chunk newChunk = new Chunk(element.Name);

            foreach (Xml.XmlNode n in element.ChildNodes)
            {
                if (n is Xml.XmlElement)
                {
                    Xml.XmlElement subElement = (Xml.XmlElement)n;

                    if (subElement.Name == "Value")
                    {
                        if (subElement.HasAttribute("Name"))
                        {
                            string valueName = subElement.GetAttribute("Name");
                            newChunk.Values[valueName] = GetElementText(subElement);
                        }
                    }
                }
            }

            // Recurse.
            foreach (Xml.XmlNode n in element.ChildNodes)
            {
                if (n is Xml.XmlElement )
                {
                    Xml.XmlElement subElement = (Xml.XmlElement)n;

                    if (subElement.Name != "Value")
                    {
                        newChunk.Add(ReadStep(subElement));
                    }
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
