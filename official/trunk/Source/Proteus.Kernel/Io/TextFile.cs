using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Kernel.Io
{
    public class TextFile : Resource.Item
    {
        protected string textContent = string.Empty;

        public string Content
        {
            get { return textContent; }
            set { textContent = value; }
        }

        public void Write(string url)
        {
            if (textContent.Length > 0)
            {
                Stream writeStream = Io.Manager.Instance.Open(url, true);
                Write( writeStream );
            }
        }

        public void Write(Stream stream)
        {
            if (stream != null && textContent.Length > 0 )
            {
                StreamWriter streamWriter = new StreamWriter(stream);
                streamWriter.Write( textContent);
                streamWriter.Flush();
                streamWriter.Close();
            }
        }

        public override int OnLoad(string url)
        {
            Stream loadStream = Io.Manager.Instance.Open(url);
            int size = 0;
            if (loadStream != null)
            {
                size = (int)loadStream.Length;
               
                StreamReader streamReader = new StreamReader( loadStream );
                textContent = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else
            {
                size = -1;
            }

            return size;
        }

        public override int OnUnload()
        {
            textContent = string.Empty;
            return 0;
        }
    }
}
