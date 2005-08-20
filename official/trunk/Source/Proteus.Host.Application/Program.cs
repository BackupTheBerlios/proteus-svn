using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proteus.Host.Application
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            using (Framework.Hosting.Engine engine = new Framework.Hosting.Engine())
            {
                engine.Input[Proteus.Framework.Hosting.Input.InputType.Name] = Kernel.Information.Program.Name;

                if (engine.Initialize())
                {
                    // Create main form.
                    MainForm mainForm = Kernel.Ui.Form.Create<MainForm>();
                 
                    // Pass input parameters to engine to use.
                    engine.Input[Proteus.Framework.Hosting.Input.InputType.MainWindow] = mainForm.MainWindow;

                    // Finally run the engine.
                    engine.Run();
                }
            }
        }
    }
}