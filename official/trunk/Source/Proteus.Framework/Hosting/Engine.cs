using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace Proteus.Framework.Hosting
{
    public sealed class Engine : Kernel.Pattern.Disposable
    {
        private Kernel.Diagnostics.Catcher          catcher         = new Kernel.Diagnostics.Catcher();
        private Queue<ITask>                        tasks           = new Queue<ITask>();
        private Input                               input           = new Input();
        private bool                                quitRequested   = false;
        private Stopwatch                           timer           = null;
        private Kernel.Extension.PluginLoader       loader          = new Kernel.Extension.PluginLoader();
        private Kernel.Configuration.CommandLine    commandLine     = new Kernel.Configuration.CommandLine();

        private static Engine                       instance        = null;

        public static Engine Instance
        {
            get { return instance; }
        }

        public Queue<ITask> Tasks
        {
            get { return tasks; }
        }

        public Input Input
        {
            get { return input; }     
        }

        public Kernel.Extension.PluginLoader PluginLoader
        {
            get { return loader; }
        }

        public void Run()
        {             
            timer = new Stopwatch();
            timer.Start();

            while (!quitRequested)
            {
                timer.Stop();

                double deltaTime = (double)timer.ElapsedMilliseconds / 1000.0;

                timer.Reset();
                timer.Start();

                foreach (ITask t in tasks)
                {
                    if (t.Update(deltaTime) == false)
                    {
                        quitRequested = true;
                    }
                }            
            }

            timer.Stop();
        }

        public void RequestQuit()
        {
            quitRequested = true;
        }

        protected override void ReleaseManaged()
        {
            Shutdown();
        }

        protected override void ReleaseUnmanaged()
        {
        }

        public bool Initialize()
        {
            commandLine.AddOption("w", "WorkingDirectory", "The working directory for the engine.");
            commandLine.AddOption("r", "Registry", "The registry file to load.");
            
            if (!commandLine.Parse())
            {
                System.Windows.Forms.MessageBox.Show(commandLine.ToString());
                return false;
            }

            // Setup working path to counter any strange invocations.
            Environment.CurrentDirectory = commandLine.GetOption("w", Kernel.Information.Program.Path);

            // Setup settings registry.
            Kernel.Registry.Manager.Instance.Url = commandLine.GetOption("r", (string)this.Input[Input.InputType.Name] + ".registry");

            // Load any defined plugins.
            this.Tasks.Enqueue(new Tasks.EventTask());
            loader.Load();
            this.Tasks.Enqueue(new Tasks.ActorTask());

            // Initialize.
            foreach (ITask t in tasks)
            {
                if (!t.Initialize(this))
                {
                    return false;
                }
            }

            return true;
        }

        private void Shutdown()
        {
            foreach (ITask t in tasks)
            {
                t.Dispose();
            }

            loader.Dispose();
        }

        public Engine()
        {
            instance = this;
        }
    }
}
