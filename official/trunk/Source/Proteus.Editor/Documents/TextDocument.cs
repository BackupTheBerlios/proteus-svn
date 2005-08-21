using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Documents
{
    public class TextDocument : Document
    {
        private string                                                      textContent     = string.Empty;
        private Kernel.Resource.Handle<Kernel.Io.TextFile>                  textHandle      = 
            new Kernel.Resource.Handle<Kernel.Io.TextFile>();
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

                        textHandle = new Kernel.Resource.Handle<Kernel.Io.TextFile>( configFile.Url );

                        // Now we can load it again.
                        if (textHandle.Resource != null)
                        {
                            // Get it
                            textEditor.Text = textHandle.Resource.Content;
                            textEditor.Document.DocumentChanged += new ICSharpCode.TextEditor.Document.DocumentEventHandler(Document_DocumentChanged);                            
                            textEditor.SetHighlighting("XML");
                            this.documentHost.Text = textHandle.Url;
                        }
                        else
                        {
                            // We have to create a new one.
                            Kernel.Io.TextFile textWriter = new Kernel.Io.TextFile();
                            textWriter.Content = "<Actor>\n</Actor>";
                            textWriter.Write( textHandle.Url );
                            this.Actor = value;
                        }
                    }
                }
            }
        }

        private void Document_DocumentChanged(object sender, ICSharpCode.TextEditor.Document.DocumentEventArgs e)
        {
            MakeDirty();
            this.documentHost.Text = textHandle.Url + "*";
        }
        
        protected override bool Save(System.IO.Stream stream)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter( stream );
            writer.Write( textEditor.Text );
            writer.Flush();
            writer.Close();

            this.documentHost.Text = textHandle.Url;

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
