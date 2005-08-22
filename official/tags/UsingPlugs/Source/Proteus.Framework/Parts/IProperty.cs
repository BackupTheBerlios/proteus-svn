using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IProperty : IPart
    {
        IActor  Owner                   { get; }

        Type    Type                    { get; }
        Type    EditorType              { get; }
        string  Category                { get; }
        
        bool    IsConfigureable         { get; }
        bool    IsState                 { get; }
        bool    IsPlug                  { get; }
        
        object  CurrentValue            { get; set; }
        object  DefaultValue            { get; }
    }
}
