using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Proteus.Host.Editor
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
                if (engine.Initialize())
                {
                    // Create main form.
                    Proteus.Editor.Forms.MainForm mainForm = Kernel.Ui.Form.Create<Proteus.Editor.Forms.MainForm>();

                    // Pass input parameters to engine to use.
                    engine.Input[Proteus.Framework.Hosting.Input.InputType.MainWindow] = mainForm.MainWindow;
                    engine.Input[Proteus.Framework.Hosting.Input.InputType.Name] = Kernel.Information.Program.Name;

                    // Finally run the engine.
                    engine.Run();
                }
            }
        }
    }
}