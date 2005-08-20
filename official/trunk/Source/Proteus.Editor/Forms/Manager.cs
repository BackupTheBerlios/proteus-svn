using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Editor.Forms
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        public void AddMenuItem(string name, string shortcut, string tooltip)
        {
        }

        public void AddToolItem(string name, System.Drawing.Image icon)
        {
        }
    }
}
