using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public enum DockPosition
    {
        Left,
        Right,
        Top,
        Bottom,
        Document,
    }

    public partial class DockableForm : Form
    {
        public virtual DockPosition DockPosition
        {
        }

        public DockableForm()
        {
            InitializeComponent();
        }
    }
}