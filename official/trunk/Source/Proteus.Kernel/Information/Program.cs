using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Kernel.Information
{
    public static class Program
    {
        public static string Name
        {
            get
            {
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public static string FileName
        {
            get
            {
                return System.IO.Path.GetFileName(Assembly.GetEntryAssembly().CodeBase);
            }
        }

        public static string Path
        {
            get
            {
                return System.IO.Path.GetFullPath( AppDomain.CurrentDomain.BaseDirectory );
            }
        }
    }
}
