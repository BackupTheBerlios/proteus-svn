using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Proteus.Kernel.Diagnostics
{
    public sealed class TextSink : Pattern.Disposable,ISink
    {
        private const string    indentString    = "  ";

        private int             indentLevel     = 0;
        private StreamWriter    textWriter      = null;
        private StringBuilder   indentBuilder   = new StringBuilder();
        private StringBuilder   messageBuilder  = new StringBuilder();

        private string GetIndentString()
        {
            indentBuilder.Remove(0, indentBuilder.Length);
            for (int i = 0; i < indentLevel; i++)
                indentBuilder.Append(indentString);

            return indentBuilder.ToString();
        }

        #region ISink Members

        public void BeginRegion(string name)
        {
            textWriter.WriteLine("{0}{1}",GetIndentString(),name);
            indentLevel++;
        }

        public void EndRegion()
        {
            indentLevel--;
        }

        public void BeginMessage(LogLevel level, Context context)
        {
            textWriter.WriteLine("{0}{1}:{2}", GetIndentString(),level, context);
            indentLevel++;
        }

        public void MessageContent(string content)
        {
            textWriter.WriteLine("{0}{1}",GetIndentString(),content);
        }

        public void EndMessage()
        {
            indentLevel--;
        }

        public void Exception(Exception exception, bool mainThread, bool isTerminating, Context context)
        {
        }

        public void Assert(string condition, string message, Context context)
        {
        }

        public bool Initialize(string initParam)
        {
            try
            {
                if (File.Exists(initParam))
                {
                    File.Delete(initParam);
                }

                textWriter = new StreamWriter(initParam);
                textWriter.AutoFlush = true;
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        #endregion

        protected override void ReleaseManaged()
        {
            textWriter.Flush();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public TextSink()
        {       
        }
    }
}
