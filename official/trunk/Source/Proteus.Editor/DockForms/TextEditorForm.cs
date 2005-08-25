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
            currentDocument = (Documents.Document)new Documents.TextDocument( textEditorControl1 );
            base.CreateDocument();
        }

        protected override void OnDropReceived(object data, int x, int y, int keystate)
        {
            string actorName = (string)data;
            textEditorControl1.Text += "<Actor> <Value Name=\"Type\">" +actorName;
        }

        public TextEditorForm()
        {
            InitializeComponent();
            CreateDocument();
            ActivateDrop( typeof(string),DragDropEffects.Copy );
            textEditorControl1.AllowDrop = false;
        }
    }
}