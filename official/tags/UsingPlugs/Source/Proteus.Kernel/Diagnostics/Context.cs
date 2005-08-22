using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public sealed class Context
    {
        private StringBuilder       builder = 
            new StringBuilder();

        private List<IContextInfo>  contextInfo =
            new List<IContextInfo>();

        public IEnumerator<IContextInfo> GetEnumerator()
        {
            foreach (IContextInfo i in contextInfo)
                yield return i;
        }

        public void Clear()
        {
            contextInfo.Clear();
        }

        public void Add(IContextInfo info)
        {
            contextInfo.Add(info);
        }

        public void Remove(IContextInfo info)
        {
            contextInfo.Remove(info);
        }

        public override string ToString()
        {
            builder.Remove(0, builder.Length);

            foreach (IContextInfo c in contextInfo)
            {
                builder.AppendFormat("[{0} : {1}]", c.Name, c.Text);
            }

            return builder.ToString();
        }
    }
}
