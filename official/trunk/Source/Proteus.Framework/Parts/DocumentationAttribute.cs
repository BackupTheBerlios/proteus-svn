using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    [AttributeUsage(AttributeTargets.Class | 
                    AttributeTargets.Method | 
                    AttributeTargets.Property |
                    AttributeTargets.Parameter |
                    AttributeTargets.ReturnValue )]
    public sealed class DocumentationAttribute : System.Attribute
    {
        private string description      = string.Empty;
        private string documentation    = string.Empty;

        public string Description
        {
            get { return description; }
        }
      
        public string Documentation
        {
            get { return documentation; }
        }

        public DocumentationAttribute(string _description)
        {
            description = _description;
        }

        public DocumentationAttribute(string _description, string _documentation)
            : this(_description)
        {
            documentation = _documentation;
        }

        public DocumentationAttribute()
        {
        }
    }
}
