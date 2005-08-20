using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class TextEditorForm : DocumentForm
    {
        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.Document; }
        }

        protected override void CreateDocument()
        {
            currentDocument = (Documents.Document)new Documents.TextDocument();
        }

        public TextEditorForm()
        {
            InitializeComponent();
        }
    }
}