using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Configuration
{
    public interface IConfigureable
    {
        bool ReadConfiguration(Chunk chunk);
        bool WriteConfiguration(Chunk chunk);
    }
}
