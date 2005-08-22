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
            WriteFile( url,textContent);
        }

        public override int OnLoad(string url)
        {
            bool success = false;
            textContent = ReadFile( url,ref success );

            if ( success )
                return textContent.Length * 2;
            return -1;
        }

        public override int OnUnload()
        {
            textContent = string.Empty;
            return 0;
        }

        public static string ReadFile(string url)
        {
            bool success = false;
            return ReadFile( url,ref success );
        }

        public static string ReadFile(string url,ref bool success )
        {
            Stream loadStream = Io.Manager.Instance.Open(url);
            string text = string.Empty;

            if (loadStream != null)
            {
                StreamReader streamReader = new StreamReader(loadStream);
                text = streamReader.ReadToEnd();
                streamReader.Close();
                success = true;
            }
            else
            {
                success = false;
            }

            return text;
        }

        public static void WriteFile(string url, string text)
        {
            if (text.Length > 0)
            {
                Stream writeStream = Io.Manager.Instance.Open(url,true);

                if (writeStream != null)
                {
                    StreamWriter writer = new StreamWriter( writeStream );
                    writer.Write( text );
                    writer.Close();
                }
            }
        }

        public TextFile()
        {
        }
    }
}
