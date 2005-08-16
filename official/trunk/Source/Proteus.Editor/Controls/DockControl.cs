using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.Controls
{
    public enum DockPosition
    {
        Top,
        Bottom,
        Left,
        Right,
        Document,
    }

    public partial class DockControl : UserControl
    {
        public virtual bool AllowsMultiple
        {
            get { return false; }
        }

        public virtual string Name
        {
            get { return string.Empty; }
        }

        public virtual DockPosition DockPosition
        {
            get { return DockPosition.Document; }
        }

        public DockControl()
        {
            InitializeComponent();
        }
    }
}
