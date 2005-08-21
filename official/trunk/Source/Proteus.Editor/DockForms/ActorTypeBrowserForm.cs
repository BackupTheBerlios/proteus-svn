using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class ActorTypeBrowserForm : DockableForm
    {
        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockRight; }
        }

        private void Build()
        {
            string[] actorTypeNames = Framework.Parts.Factory.Instance.AllTypes;

            // Foreach actor count its base types.
            foreach( string s in actorTypeNames )
                treeView1.Nodes.Add( s );
        }

        public ActorTypeBrowserForm()
        {
            InitializeComponent();
            Build();
        }
    }
}