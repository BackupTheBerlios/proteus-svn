using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.Forms
{
    public sealed class Manager : Kernel.Pattern.Singleton<Manager>
    {
        private ToolStrip               toolBar  = null;
        private MenuStrip               mainMenu = null;

        public void AddMenuItem(string name, string shortcut, string tooltip,EventHandler clickHandler )
        {
            string[] parts = name.Split( new char[] {'.'} );

            if (parts.Length > 1)
            {
                AddMenuItemStep( mainMenu.Items,0,parts,shortcut,tooltip,clickHandler );       
            }
        }

        private void AddMenuItemStep(ToolStripItemCollection items, int index, string[] parts, string shortcut, string toolTip,EventHandler clickHandler )
        {
            string name = parts[index];

            ToolStripMenuItem foundItem = null;

            // Search for it in the item.
            foreach (ToolStripMenuItem m in items)
            {
                if (m.Name == name)
                {
                    foundItem = m;
                    break;
                }
            }

            if (foundItem == null)
            {
                foundItem = new ToolStripMenuItem( name );
                foundItem.Name = name;
                items.Add( foundItem );
            }

            if (index < parts.Length - 1)
            {
                // Recurse further.
                AddMenuItemStep( foundItem.DropDownItems,index +1,parts,shortcut,toolTip,clickHandler );
            }
            else
            {
                // Store and terminate.
                foundItem.ToolTipText = toolTip;
                //foundItem.ShortcutKeys = shortcut;
                foundItem.Click += clickHandler;
            }
        }

        public void AddToolSeperator()
        {
            ToolStripSeparator sep = new ToolStripSeparator();
            toolBar.Items.Add( sep );
        }

        public void AddToolItem(string name, System.Drawing.Image icon,string toolTip,EventHandler clickHandler )
        {
            ToolStripButton button = new ToolStripButton( name,icon,clickHandler );
            button.ToolTipText = toolTip;
            button.AutoToolTip = false;
            if ( button.ToolTipText != string.Empty )
                button.AutoToolTip = true;

            toolBar.Items.Add( button );
        }

        public void Initialize(ToolStrip _toolBar,MenuStrip _mainMenu )
        {
            toolBar = _toolBar;
            mainMenu = _mainMenu;
        }

        public Manager()
        {
        }
    }
}
