using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public sealed class Property : IProperty
    {
        private PropertyInfo            propertyInfo            = null;
        private PropertyAttribute       propertyAttribute       = null;
        private DocumentationAttribute  propertyDocumentation   = null;
        private IActor                  propertyOwner           = null;

        #region IProperty Members

        public IActor Owner
        {
            get { return propertyOwner; }
        }

        public Type Type
        {
            get { return propertyInfo.PropertyType; }
        }

        public Type EditorType
        {
            get { return propertyAttribute.EditorType;  }
        }

        public string Category
        {
            get { return propertyAttribute.Category; }
        }

        public bool IsConfigureable
        {
            get { return propertyAttribute.IsConfigureable; }
        }

        public bool IsState
        {
            get { return propertyAttribute.IsState; }
        }

        public bool IsPlug
        {
            get { return propertyAttribute.IsPlug; }
        }

        public object CurrentValue
        {
            get
            {
                return propertyInfo.GetValue(propertyOwner, null);
            }
            set
            {
                propertyInfo.SetValue(propertyOwner, value, null);
            }
        }

        public object DefaultValue
        {
            get
            {
                if (propertyAttribute.DefaultValue != null)
                    return propertyAttribute.DefaultValue;

                return this.CurrentValue;
            }
        }

        #endregion

        #region IPart Members

        public string Name
        {
            get 
            {
                if (propertyAttribute.Name != string.Empty)
                    return propertyAttribute.Name;

                return propertyInfo.Name;
            }
            set { }
        }

        public string Description
        {
            get { return propertyDocumentation.Description; }
        }

        public string Documentation
        {
            get { return propertyDocumentation.Documentation; }
        }

        #endregion

        public static Property Create(PropertyInfo info,IActor owner)
        {
            if (Attribute.GetCustomAttribute(info, typeof(PropertyAttribute)) != null)
            {
                return new Property(info,owner);
            }
            return null;
        }

        public static IProperty[] Enumerate(IActor actor)
        {
            PropertyInfo[] properties = actor.GetType().GetProperties();
            List<IProperty> propertyList = new List<IProperty>();

            foreach (PropertyInfo p in properties)
            {
                Property currentProperty = Create(p,actor);
                if (currentProperty != null)
                    propertyList.Add(currentProperty);
            }

            return propertyList.ToArray();
        }

        private Property( PropertyInfo info,IActor actor )
        {
            // Get all attributes we need.
            propertyInfo            = info;
            propertyAttribute       = Attribute.GetCustomAttribute(info, typeof(PropertyAttribute)) as PropertyAttribute;
            propertyDocumentation   = Attribute.GetCustomAttribute(info, typeof(DocumentationAttribute)) as DocumentationAttribute;
            propertyOwner           = actor;

            if (propertyDocumentation == null)
            {
                propertyDocumentation = new DocumentationAttribute();
            }
        }   
    }
}
