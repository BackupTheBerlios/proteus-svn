using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class DocumentForm : DockableForm
    {
        protected Documents.Document        currentDocument = null;
        protected Framework.Parts.IActor    currentActor    = null;
       
        public virtual Proteus.Framework.Parts.IActor Actor
        {
            set 
            {
                if (currentActor == null)
                {
                    currentActor = value;
                    currentDocument.Actor = currentActor;
                }
            }
        }

        protected virtual void CreateDocument()
        {
            currentDocument = null;
        }

        public virtual bool IsCompatible(Framework.Parts.IActor actor)
        {
            if (currentDocument != null)
            {
                return currentDocument.IsCompatible( actor );
            }

            return false;
        }

        private void DocumentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            CloseDocument();
        }

        private void Instance_ActorRemoved(Proteus.Framework.Parts.IActor actor)
        {
            if (actor == currentActor)
            {
                this.Close();
            }
        }

        private void CloseDocument()
        {
            if (currentDocument != null && currentActor != null )
            {
                currentDocument.Dispose();
            }
        }

        public DocumentForm()
        {
            InitializeComponent();

            // Watch out for closing the form.
            this.FormClosing += new FormClosingEventHandler(DocumentForm_FormClosing);
            Manipulation.Manager.Instance.ActorRemoved += new Proteus.Editor.Manipulation.Manager.ActorDelegate(Instance_ActorRemoved);

            // Create document.
            CreateDocument();
            if ( currentDocument != null )
                currentDocument.Host = this;
        }     
    }
}