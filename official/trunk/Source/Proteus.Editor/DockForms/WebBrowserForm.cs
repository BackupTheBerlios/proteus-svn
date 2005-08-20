using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class WebBrowserForm : DockableForm
    {
        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.Document; }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == System.Windows.Forms.Keys.Enter)
            {
                webBrowser1.Navigate(this.textBox1.Text);
            }
        }

        public WebBrowserForm()
        {
            InitializeComponent();
            webBrowser1.Navigate( textBox1.Text );
        }
    }
}