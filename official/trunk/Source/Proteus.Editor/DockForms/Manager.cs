using System;
using System.Collections.Generic;
using System.Text;

using Dp = WeifenLuo.WinFormsUI;

namespace Proteus.Editor.DockForms
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private Kernel.Pattern.TypeFactory<DockableForm> formFactory = 
            new Kernel.Pattern.TypeFactory<DockableForm> ();
    
        private Dp.DockPanel                             dockPanel   = null;
        private List<DockableForm>                       dockedForms = 
            new List<DockableForm>();

        public Kernel.Pattern.TypeFactory<DockableForm> Factory
        {
            get { return formFactory; }
        }

        public DockableForm Add(string name)
        {
            // First try to create it.
            DockableForm newForm = formFactory.Create( name );

            if (newForm != null)
            {
                if (!newForm.IsDocumentHost)
                {
                    // We only allow a single instance of this.
                    foreach (DockableForm f in dockedForms)
                    {
                        if (f.GetType().Equals(newForm.GetType()))
                            return null;
                    }
                }
            
                // Ok we can allow it being hosted so add it.
                dockedForms.Add( newForm );
                newForm.FormClosed += new System.Windows.Forms.FormClosedEventHandler(newForm_FormClosed);
                newForm.Show( dockPanel,newForm.DefaultDockState );

                return newForm;
            }

            return null;
        }

        public void Initialize(Dp.DockPanel _dockPanel)
        {
            dockPanel = _dockPanel;

            // Register default types.
            Factory.Register<DiagramForm>("ActorDiagram");
            Factory.Register<ActorBrowserForm>("ActorBrowser");

            // Add default forms.
            Add("ActorDiagram");
            Add("ActorBrowser");
        }

        private void newForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e)
        {
            dockedForms.Remove( (DockableForm)sender );
        }

        public Manager()
        {
        }
    }
}
