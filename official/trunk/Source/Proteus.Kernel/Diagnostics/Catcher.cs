using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public sealed class Catcher : Pattern.Disposable
    {
        private static Log<Catcher> log = new Log<Catcher>();

        private void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            OnException(e.Exception, false, Environment.HasShutdownStarted );
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            OnException((System.Exception)e.ExceptionObject, true, e.IsTerminating); 
        }

        private void OnException(System.Exception exception, bool mainThread,bool isTerminating )
        {
            log.Exception(exception, mainThread, isTerminating);
        }

        protected override void ReleaseManaged()
        {
            AppDomain.CurrentDomain.UnhandledException -= new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            System.Windows.Forms.Application.ThreadException -= new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

        }

        protected override void ReleaseUnmanaged()
        {
        }
        
        public Catcher()
        {
            if ( !System.Diagnostics.Debugger.IsAttached)
            {
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
                System.Windows.Forms.Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);
            }
        }
     }
}
