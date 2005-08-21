using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

using Dp = WeifenLuo.WinFormsUI;

namespace Proteus.Editor.DockForms
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private Kernel.Pattern.TypeFactory<DockableForm>        formFactory = 
            new Kernel.Pattern.TypeFactory<DockableForm> ();

        private List<string>                                    documentFormTypes = 
            new List<string>();
    
        private Dp.DockPanel                                    dockPanel   = null;
        private List<DockableForm>                              dockedForms = 
            new List<DockableForm>();

        private DocumentForm                                    topDocumentForm = null;

        public void Add(string name)
        {
            DockableForm newForm = Create(name,true);
        }

        private DockableForm Create(string name,bool show )
        {
            // First try to create it.
            DockableForm newForm = formFactory.Create( name );

            if (newForm != null)
            {
                // Assure only one instance.
                if ( !(newForm is DocumentForm) )
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
                newForm.Text = name;

                if (show)
                {
                    newForm.Show(dockPanel, newForm.DefaultDockState);
                }

                return newForm;
            }

            return null;
        }

        public void Register<FormType>(string name) where FormType : DockableForm,new()
        {
            formFactory.Register<FormType>( name );

            DockableForm testForm = formFactory.Create(name);
            if (testForm != null)
            {
                if ( testForm is DocumentForm )
                    documentFormTypes.Add( name );

                dockedForms.Remove( testForm );
                testForm.Dispose();
            }
        }

        public void Remove(DockableForm form)
        {
            dockedForms.Remove(form);
        }

        public void Initialize(Dp.DockPanel _dockPanel)
        {
            dockPanel = _dockPanel;

            // Register default windows.
            this.Register<ActorBrowserForm>("Actor browser");
            this.Register<ActorTypeBrowserForm>("Actor type browser");
            this.Register<WebBrowserForm>("Web browser");
            this.Register<LogForm>("Log");
            this.Register<PropertyBrowserForm>("Property browser");
            this.Register<TextEditorForm>("Text editor");

            // Create default windows.
            Add("Actor browser");
            Add("Actor type browser");
            Add("Web browser");
            Add("Property browser");
            Add("Log");
        }

        private void CreateDocumentHosts(Framework.Parts.IActor actor)
        {
            foreach (string n in documentFormTypes )
            {
                DocumentForm newForm = (DocumentForm)Create( n,false );

                if (newForm.IsCompatible(actor))
                {
                    bool alreadyExisting = false;

                    // Search existing forms for this combination
                    foreach( DockableForm f in dockedForms )
                    {
                        if ( f is DocumentForm )
                        {
                            DocumentForm docForm = f as DocumentForm;

                            if ( object.ReferenceEquals(docForm.Actor,actor) )
                            {
                                alreadyExisting = true;

                                // Push to front.
                                docForm.Show();
                                topDocumentForm = docForm;
                                Documents.Manager.Instance.TopDocument = docForm.CurrentDocument;
                            }
                        }
                    }
                    
                    if( !alreadyExisting )
                    {
                        newForm.Actor = actor;
                        newForm.Show(dockPanel, newForm.DefaultDockState);
                        topDocumentForm = newForm;
                        Documents.Manager.Instance.TopDocument = newForm.CurrentDocument;
                    }
                    else
                    {
                        dockedForms.Remove( newForm );
                        newForm.Dispose();                        
                    }
                }
                else
                {
                    dockedForms.Remove( newForm );
                    newForm.Dispose();
                }
            }
        }

        private void Instance_DefaultAction(Proteus.Framework.Parts.IActor selectedActor)
        {
            CreateDocumentHosts( selectedActor );
        }

        public Manager()
        {
            Manipulation.Manager.Instance.DefaultAction += new Proteus.Editor.Manipulation.Manager.ActionDelegate(Instance_DefaultAction);
        }
    }
}
