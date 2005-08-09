using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Resource
{
    public sealed class Handle<ResourceType> where ResourceType : Item,new()
    {
        public delegate void ResourceChangedDelegate<ChangedType>(Handle<ChangedType> handle) where ChangedType : Item, new();

        public event ResourceChangedDelegate<ResourceType> ResourceChanged;

        private string      handleUrl       = string.Empty;
        private Priority    handlePriority  = Priority.Medium;

        public Priority Priority
        {
            get { return handlePriority; }
            set { handlePriority = value; }
        }

        public bool IsValid
        {
            get { return (this.Resource != null); }
        }

        public string Url
        {
            get { return handleUrl; }
            set 
            { 
                // Remove old one.
                if (handleUrl != string.Empty)
                {
                    Manager.Instance.RemoveChangeNotification( handleUrl,new Manager.ChangeNotificationDelegate(this.OnResourceChanged));
                }

                handleUrl = value;
                Manager.Instance.AddChangeNotification(handleUrl,new Manager.ChangeNotificationDelegate(this.OnResourceChanged));
            }
        }

        public ResourceType Resource
        {
            get 
            {
                if (handleUrl != string.Empty)
                {
                    return Manager.Instance.Require<ResourceType>(handleUrl,handlePriority );
                }
                return null;
            }
        }

        private void OnResourceChanged()
        {
            if (this.ResourceChanged != null)
            {
                this.ResourceChanged(this);
            }
        }

        public Handle()
        {
        }

        public Handle(string url) 
        {
            handleUrl = url;
        }

        public Handle(string url, Priority priority) : this( url )
        {
            handlePriority = priority;
        }
    }
}
