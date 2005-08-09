using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public interface ISink : IDisposable
    {
        bool Initialize(string initParam);

        void BeginRegion(string name);
        void EndRegion();

        void BeginMessage( LogLevel level,Context context );
        void MessageContent(string content);
        void EndMessage();

        void Exception(System.Exception exception, bool mainThread,bool isTerminating,Context context );
        void Assert(string condition, string message,Context context );
    }
}
