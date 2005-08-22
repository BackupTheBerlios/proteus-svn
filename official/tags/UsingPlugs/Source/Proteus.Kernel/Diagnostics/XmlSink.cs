using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Proteus.Kernel.Diagnostics
{
    public class XmlSink : Pattern.Disposable,ISink        
    {
        private XmlTextWriter   writer      = null;
        private int             indent      = 0;
        private StringBuilder   builder     = new StringBuilder();
        private bool            inMessage;
        private bool            flush;

        public bool AutoFlush
        {
            set { flush = value; }
        }

        #region ISink Members

        public void BeginRegion(string name)
        {
            if (!inMessage)
            {
                indent++;
                writer.WriteStartElement(name);
                Flush();
            }
        }

        public void EndRegion()
        {
            if (!inMessage)
            {
                if (indent > 0)
                {
                    writer.WriteEndElement();
                    Flush();
                    indent--;
                }
            }
        }

        public void BeginMessage(LogLevel level, Context context)
        {
            if (!inMessage)
            {
                writer.WriteStartElement(level.ToString());

                foreach (IContextInfo c in context)
                {
                    writer.WriteAttributeString(c.Name, c.Text);
                }

                Flush();

                inMessage = true;
            }
        }

        public void MessageContent(string content)
        {
            if (inMessage)
            {
                writer.WriteValue(content);
                writer.WriteWhitespace(Environment.NewLine);
                Flush();
            }
        }

        public void EndMessage()
        {
            if (inMessage)
            {
                writer.WriteEndElement();
                Flush();
                inMessage = false;
            }
        }

        public void Exception(Exception exception, bool mainThread,bool isTerminating,Context context )
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void Assert(string condition, string message,Context context )
        {
            writer.WriteStartElement("AssertionFailed");

            foreach (IContextInfo c in context)
            {
                writer.WriteAttributeString(c.Name, c.Text);
            }

            writer.WriteElementString("Condition", condition);
            writer.WriteElementString("Message", message);

            writer.WriteEndElement();

            Flush();
        }

        public bool Initialize(string initParam)
        {
            try
            {
                writer = new XmlTextWriter(initParam, Encoding.UTF8);
                writer.Formatting = Formatting.Indented;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        #endregion

        private void Flush()
        {
            if (flush)
                writer.Flush();
        }

        protected override void ReleaseManaged()
        {
            writer.Flush();
        }

        protected override void ReleaseUnmanaged()
        {
            
        }

        public XmlSink()
        {       
        }    
    }
}
