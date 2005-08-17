using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.Forms
{
    public partial class MainForm : Form
    {
        public Control MainWindow
        {
            get { return this; }
        }

        public MainForm()
        {
            InitializeComponent();

            // Initialize docking support.
            DockForms.Manager.Instance.Initialize( dockPanel1 );
        }
    }
}