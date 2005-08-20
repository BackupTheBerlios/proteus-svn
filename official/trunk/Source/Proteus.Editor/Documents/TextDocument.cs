using System;
using System.Collections.Generic;
using System.Text;

using Ted = ICSharpCode.TextEditor.Document;

namespace Proteus.Editor.Documents
{
    public class TextDocument : Document
    {
        private string                                                      textContent     = string.Empty;
        private Ted.IDocument                                               textDocument    = null;
        private Kernel.Resource.Handle<Kernel.Io.TextFile>                  textHandle      = 
            new Kernel.Resource.Handle<Kernel.Io.TextFile>();
        private ICSharpCode.TextEditor.TextEditorControl                    textEditor      = null;

        private static Ted.DocumentFactory  documentFactory = new Ted.DocumentFactory();
       

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
                        textDocument = documentFactory.CreateDocument();
                        textDocument.TextContentChanged += new EventHandler(textDocument_TextContentChanged);
                        textEditor.Document = textDocument;

                        // Now we can load it again.
                        if (textHandle.Resource != null)
                        {
                            // Get it.
                            textContent = textHandle.Resource.Content;

                            // Insert the content.
                            textDocument.TextContent = textContent;
                        }
                        else
                        {
                            // We have to create a new one.
                            Kernel.Io.TextFile textWriter = new Kernel.Io.TextFile();
                            textWriter.Content = "<NewFile>";
                            textWriter.Write( textHandle.Url );
                            this.Actor = value;
                        }
                    }
                }
            }
        }

        private void textDocument_TextContentChanged(object sender, EventArgs e)
        {
            textContent = textDocument.TextContent;
            MakeDirty();
        }
        
        protected override bool Save(System.IO.Stream stream)
        {
            System.IO.StreamWriter writer = new System.IO.StreamWriter( stream );
            writer.Write( textContent );
            writer.Flush();
            writer.Close();

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
