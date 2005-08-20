using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Documents
{
    public abstract class Document : Kernel.Pattern.Disposable
    {
        protected bool                      documentIsDirty = false;
        protected string                    documentUrl     = string.Empty;
        protected Framework.Parts.IActor    documentActor   = null;
        protected DockForms.DocumentForm    documentHost    = null;

        public virtual DockForms.DocumentForm Host
        {
            set { documentHost = value; }
        }

        public virtual bool IsDirty
        {
            get { return documentIsDirty; }
        }

        public virtual void MakeDirty()
        {
            documentIsDirty = true;
        }

        public abstract Framework.Parts.IActor Actor
        {
            set;
        }

        public virtual void Save()
        {
            if (IsDirty && documentUrl != string.Empty )
            {
                System.IO.Stream writeStream = Kernel.Io.Manager.Instance.Open(documentUrl,true);

                if (writeStream != null)
                {
                    if (Save(writeStream))
                    {
                        documentIsDirty = false;
                    }

                    writeStream.Close();               
                }           
            }
        }

        protected override void ReleaseManaged()
        {
            Manager.Instance.Unregister(this);
        }

        protected override void ReleaseUnmanaged()
        {
            
        }
      
        protected   abstract bool Save( System.IO.Stream stream );
        public      abstract bool IsCompatible( Framework.Parts.IActor actor );

        protected Document()
        {
            Manager.Instance.Register( this );
        }
    }
}
