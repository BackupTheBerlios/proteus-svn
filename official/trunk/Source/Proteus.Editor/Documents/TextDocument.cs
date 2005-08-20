using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Documents
{
    public class TextDocument : Document
    {
        private string content = string.Empty;

        public override Proteus.Framework.Parts.IActor Actor
        {
            set 
            { 
            }
        }
        
        protected override bool Save(System.IO.Stream stream)
        {
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
    }
}
