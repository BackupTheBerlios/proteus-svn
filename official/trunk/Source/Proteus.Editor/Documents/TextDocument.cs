using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Documents
{
    public class TextDocument : Document
    {
        private ICSharpCode.TextEditor.TextEditorControl                    textEditor      = null;       

        public override Proteus.Framework.Parts.IActor Actor
        {
            set 
            {
                if (value != null)
                {
                    // Configuration file actor settings.
                    if (value is Framework.Parts.Basic.ConfigFileActor)
                    {
                        Framework.Parts.Basic.ConfigFileActor configFile = (Framework.Parts.Basic.ConfigFileActor)value;

                        textEditor.Text = Kernel.Io.TextFile.ReadFile( configFile.Url );
                        documentUrl = configFile.Url;

                        // Get it
                        textEditor.Document.DocumentChanged += new ICSharpCode.TextEditor.Document.DocumentEventHandler(Document_DocumentChanged);                            
                        textEditor.SetHighlighting("XML");
                        this.documentHost.Text = documentUrl;
                    }
                }
            }
        }

        private void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
        {
            MakeDirty();
            this.documentHost.Text = documentUrl + "*";
        }
        
        protected override bool Save(System.IO.Stream stream)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter( stream );
            writer.Write( textEditor.Text );
            writer.Flush();
            writer.Close();

            this.documentHost.Text = documentUrl;

            return true;
        }

        public override bool IsCompatible(Proteus.Framework.Parts.IActor actor)
        {
            if (actor is Framework.Parts.Basic.ConfigFileActor)
            {
                return true;
            }

            return false;
        }

        public TextDocument(ICSharpCode.TextEditor.TextEditorControl control)
        {
            textEditor = control;
        }
    }
}
