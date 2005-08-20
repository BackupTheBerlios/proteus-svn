using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public abstract class Plug : IPlug
    {
        protected   List<IConnection>       connections             = new List<IConnection>();
        protected   bool                    isMultiplex             = false;
        protected   IActor                  owner                   = null;
        protected   DocumentationAttribute  documentationAttribute  = null;
        protected   string                  name                    = string.Empty;

        #region IPlug Members

        public virtual bool IsMultiplex
        {
            get { return isMultiplex; }
        }

        public virtual IConnection[] Connections
        {
            get { return connections.ToArray(); }
        }

        public virtual IActor Owner
        {
            get { return owner; }
        }

        #endregion

        #region IPart Members

        public virtual string Name
        {
            get { return name; }
            set { }
        }

        public virtual string Description
        {
            get { return documentationAttribute.Description; }
        }

        public virtual string Documentation
        {
            get { return documentationAttribute.Documentation; }
        }

        #endregion

        private void Initialize(    DocumentationAttribute _documentation,
                                    PlugAttribute _plug,
                                    bool _isMultiplex, 
                                    IActor actor )
        {
            owner = actor;
            isMultiplex = _isMultiplex;

            if (_plug != null)
            {
                if (_plug.Name != string.Empty)
                    name = _plug.Name;
            }

            // Store the documentation.
            documentationAttribute = _documentation;

            if (documentationAttribute == null)
                documentationAttribute = new DocumentationAttribute();
        }

        protected Plug(string _name, string _description, string _documentation, bool _isMultiplex, IActor actor)
        {
            DocumentationAttribute docAttribute = new DocumentationAttribute(_description, _documentation);
            PlugAttribute plugAttribute = new PlugAttribute(_name);

            Initialize(docAttribute, plugAttribute, _isMultiplex, actor);
        }

        protected Plug(Type type, bool _isMultiplex, IActor actor)
        {
            name = type.Name;

            DocumentationAttribute _documentation = Attribute.GetCustomAttribute(type, typeof(DocumentationAttribute)) as DocumentationAttribute;
            PlugAttribute _plug = Attribute.GetCustomAttribute(type, typeof(PlugAttribute)) as PlugAttribute;

            Initialize(_documentation, _plug, _isMultiplex, actor);
        }

        protected Plug(MemberInfo info,bool _isMultiplex,IActor actor )
        {
            // Store the name
            name = info.Name;
            
            DocumentationAttribute  _documentation  = Attribute.GetCustomAttribute(info, typeof(DocumentationAttribute)) as DocumentationAttribute;
            PlugAttribute           _plug           = Attribute.GetCustomAttribute(info, typeof(PlugAttribute)) as PlugAttribute;

            Initialize(_documentation, _plug, _isMultiplex, actor);
        }
    }
}
