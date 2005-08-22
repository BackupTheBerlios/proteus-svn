using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public enum LogLevel
    {
        Debug       = 0,
        Info        = 1,
        Warning     = 2,
        Error       = 3,
    }

    public sealed class Log<TargetType>
    {
        private StringBuilder       builder     = new StringBuilder();
        private Type                type        = typeof(TargetType);
        private Assert<TargetType>  logAssert   = new Assert<TargetType>();

        public Assert<TargetType> Assert
        {
            get { return logAssert; }
        }

        public void BeginRegion( string name )
        {
            Manager.Instance.BeginRegion(name);
        }

        public void EndRegion()
        {
            Manager.Instance.EndRegion();
        }

        public void BeginMessage( LogLevel level )
        {
            Manager.Instance.BeginMessage( level );
        }

        public void MessageContent(string format, params object[] plist)
        {
            builder.AppendFormat(format, plist);

            Manager.Instance.MessageContent(builder.ToString());

            builder.Remove(0, builder.Length);
        }

        public void EndMessage()
        {
            Manager.Instance.EndMessage();
        }

        private void Message(LogLevel type, string format, params object[] plist)
        {
            this.BeginMessage(type);
            this.MessageContent(format, plist);
            this.EndMessage();
        }

        public void Debug(string format, params object[] plist)
        {
            this.Message(LogLevel.Debug, format, plist);
        }

        public void Info(string format, params object[] plist)
        {
            this.Message(LogLevel.Info, format, plist);
        }

        public void Warning(string format, params object[] plist)
        {
            this.Message(LogLevel.Warning, format, plist);
        }

        public void Error(string format, params object[] plist)
        {
            this.Message(LogLevel.Error, format, plist);
        }

        public void Exception(System.Exception exception)
        {
            this.Exception(exception, true);
        }

        public void Exception(System.Exception exception, bool mainThread)
        {
            this.Exception(exception, mainThread, false);
        }

        public void Exception(System.Exception exception, bool mainThread, bool isTerminating)
        {
            Manager.Instance.Exception(exception, mainThread, isTerminating);
        }
    }
}
