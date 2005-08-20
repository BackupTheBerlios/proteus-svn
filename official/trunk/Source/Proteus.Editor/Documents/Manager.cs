using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Documents
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private List<Document> openDocuments = new List<Document>();

        public void SaveAll()
        {
            foreach (Document d in openDocuments)
            {
                d.Save();
            }
        }

        public void Register(Document d)
        {
            if (d != null)
            {
                openDocuments.Add(d);
            }
        }

        public void Unregister(Document d)
        {
            openDocuments.Remove(d);
        }
   
        public Manager()
        {
        }      
    }
}
