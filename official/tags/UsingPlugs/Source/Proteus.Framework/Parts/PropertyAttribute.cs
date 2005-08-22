using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class PropertyAttribute : System.Attribute
    {
        private string      category        = "General";
        private Type        editorType      = null;
        private string      name            = string.Empty;
        private bool        isState         = false;
        private bool        isConfigureable = true;
        private bool        isPlug          = false;
        private object      defaultValue    = null;

        public Type EditorType
        {
            get { return editorType; }
        }

        public string Category
        {
            get { return category; }
        }

        public string Name
        {
            get { return name; }
        }

        public bool IsState
        {
            get { return isState; }
        }

        public bool IsConfigureable
        {
            get { return isConfigureable; }
        }

        public bool IsPlug
        {
            get { return isPlug; }
        }

        public object DefaultValue
        {
            get { return defaultValue; }
        }
        
        public PropertyAttribute()
        {          
        }

        public PropertyAttribute( bool _isState )
        {
            isState = _isState;
        }

        public PropertyAttribute(bool _isState, bool _isPlug)
            : this( _isState )
        {
            isPlug = _isPlug;
        }

        public PropertyAttribute(bool _isState, bool _isPlug, bool _isConfigureable)
            : this( _isState,_isPlug )
        {
            isConfigureable = _isConfigureable;
        }

        public PropertyAttribute(string _name)
        {
            name = _name;
        }

        public PropertyAttribute(string _name, string _category)
            : this( _name )
        {
            category = _category;
        }

        public PropertyAttribute(object _default)
        {
            defaultValue = _default;
        }

        public PropertyAttribute(object _default, Type _editorType)
            : this ( _default )
        {
            editorType = _editorType;
        }

        public PropertyAttribute(string _name, string _category, object _default, Type _editorType)
            : this( _name,_category )
        {
            defaultValue = _default;
            editorType = _editorType;
        }
    }
}
