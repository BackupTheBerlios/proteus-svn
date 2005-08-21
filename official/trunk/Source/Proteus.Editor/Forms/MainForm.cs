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

            // Initialize menu and tool support.
            Manager.Instance.Initialize( this.toolStrip1,this.menuStrip1 );
        
            // Now add default items to the system.
            Manager.Instance.AddToolItem(string.Empty,Utility.Resource.GetIcon("Save.bmp"),"Saves the current document.",new EventHandler( this.Save_Click ) );
            Manager.Instance.AddMenuItem("File.Save",string.Empty,"Saves the current document.",new EventHandler(this.Save_Click) );
        }

        private void Save_Click(object sender, EventArgs e)
        {
            Documents.Manager.Instance.Save();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Framework.Hosting.Engine.Instance.RequestQuit();
        }
    }
}