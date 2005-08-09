using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Extension
{
    public interface IPlugin
    {
        string  Name         { get; }
        string  Description  { get; }
        string  Copyright    { get; }

        bool    OnLoad(     Information.License license,
                            Information.Version version,
                            Information.Platform platform);

        bool    OnUnload(   Information.License license,
                            Information.Version version,
                            Information.Platform platform);
    }
}
