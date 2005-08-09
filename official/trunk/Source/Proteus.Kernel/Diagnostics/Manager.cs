using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public class Manager : Pattern.Singleton<Manager>
    {
        private Context                             logContext      = new Context();
        private List<ISink>                         logSinks        = new List<ISink>();
        
        private Pattern.TypeFactory<ISink>          logSinkFactory
          = new Pattern.TypeFactory<ISink>();

        private Pattern.TypeFactory<IContextInfo>   logContextFactory
         = new  Pattern.TypeFactory<IContextInfo>();

        public Pattern.TypeFactory<ISink> SinkFactory
        {
            get { return logSinkFactory; }
        }
     
        public Pattern.TypeFactory<IContextInfo> ContextFactory
        {
            get { return logContextFactory; }
        }

        public void BeginRegion(string name)
        {
            foreach (ISink s in logSinks)
            {
                s.BeginRegion(name);
            }
        }

        public void EndRegion()
        {
            foreach (ISink s in logSinks)
            {
                s.EndRegion();
            }
        }

        public void BeginMessage(LogLevel level)
        {
            foreach (ISink s in logSinks)
            {
                s.BeginMessage(level, logContext );
            }
        }
        
        public void MessageContent(string content)
        {
            foreach (ISink s in logSinks)
            {
                s.MessageContent(content);
            }
        }

        public void EndMessage()
        {
            foreach (ISink s in logSinks)
            {
                s.EndMessage();
            }
        }

        public void Exception(System.Exception exception, bool mainThread,bool isTerminating )
        {
            foreach (ISink s in logSinks)
            {
                s.Exception(exception, mainThread,isTerminating,logContext);
            }
        }

        public void Assert(string condition, string message)
        {
            foreach (ISink s in logSinks)
            {
                s.Assert(condition, message, logContext);
            }
        }

        public void AddSink(string sinkType, string sinkInit)
        {
            ISink newSink = logSinkFactory.Create(sinkType);
            if (newSink != null)
            {
                if (newSink.Initialize(sinkInit))
                {
                    logSinks.Add(newSink);
                }
            }
        }

        public void AddSink(ISink sink)
        {
            if (sink != null)
            {
                logSinks.Add(sink);
            }
        }

        public void RemoveSink(ISink sink)
        {
            logSinks.Remove(sink);
        }

        public void AddContext(string contextType)
        {
            IContextInfo newContext = logContextFactory.Create(contextType);
            if (newContext != null)
            {
                logContext.Add(newContext);
            }
        }

        private void Configure()
        {
            // First clear it.
            logContext.Clear();
            logSinks.Clear();

            // Add default ones.
            this.AddSink("TextSink", Information.Program.Name + ".log");
            this.AddContext("Timestamp");

            // Read from registry.
            /*Configuration.Chunk[] sinkChunks = Registry.Manager.Instance.GetMultipleEntry("Diagnostics.Sinks.Sink");
            foreach (Configuration.Chunk c in sinkChunks)
            {
                string sinkType = c.GetValue("Type", string.Empty);
                string sinkParam = c.GetValue("Param", string.Empty);

                if (sinkType != string.Empty && sinkParam != string.Empty)
                {
                    this.AddSink(sinkType, sinkParam);
                }
            }

            Configuration.Chunk[] contextChunks = Registry.Manager.Instance.GetMultipleEntry("Diagnostics.Context.Context");
            foreach (Configuration.Chunk c in contextChunks)
            {
                string contextType = c.GetValue("Type", string.Empty);
                if (contextType != string.Empty)
                {
                    this.AddContext(contextType);
                }
            }*/
        }   

        private void Instance_RegistryChanged()
        {
            Configure();
        }

        public Manager()
        {
            Registry.Manager.Instance.RegistryChanged += new Proteus.Kernel.Registry.Manager.RegistryChangedDelegate(Instance_RegistryChanged);

            // Add default types.
            SinkFactory.Register<XmlSink>("XmlSink");
            SinkFactory.Register<TextSink>("TextSink");

            ContextFactory.Register<TimestampInfo>("Timestamp");

            Configure();
        }      
    }
}
