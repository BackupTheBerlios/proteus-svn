using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Host.Application
{
    public partial class MainForm : Form
    {
        public Control MainWindow
        {
            get { return panel1; }
        }

        public MainForm()
        {
            InitializeComponent();
            controlSink1.CaptureLog = true;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Framework.Hosting.Engine.Instance.RequestQuit();
        }
    }
}