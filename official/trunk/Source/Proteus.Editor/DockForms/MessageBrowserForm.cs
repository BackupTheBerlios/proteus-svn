using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class MessageBrowserForm : DockableForm
    {
        private Framework.Parts.IActor currentActor = null;

        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockRight; }
        }

        public Proteus.Framework.Parts.IActor Actor
        {
            set
            {
                if (value != null)
                {
                    currentActor = value;
                    Build(currentActor);
                }
            }
        }

        protected override object OnDragRequest(int x, int y, MouseButtons buttons)
        {
            return "Hello";
        }

        private void Build(Framework.Parts.IActor actor)
        {
        }

        private void Instance_SelectionChanged(Proteus.Framework.Parts.IActor selectedActor, List<Proteus.Framework.Parts.IActor> selectedActors)
        {
            this.Actor = selectedActor;
        }

        public MessageBrowserForm()
        {
            InitializeComponent();

            Manipulation.Manager.Instance.SelectionChanged += new Proteus.Editor.Manipulation.Manager.SelectionDelegate(Instance_SelectionChanged);
            this.Actor = Framework.Parts.Basic.RootActor.Instance;
        
            ActivateDrag();
        }
    }
}