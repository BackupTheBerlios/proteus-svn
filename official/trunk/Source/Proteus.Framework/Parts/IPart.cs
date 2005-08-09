using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public interface IPart
    {
        string Name             { get; }
        string Description      { get; }
        string Documentation    { get; }
    }
}
