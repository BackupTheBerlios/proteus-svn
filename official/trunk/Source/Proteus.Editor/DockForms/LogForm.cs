using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class LogForm : DockableForm
    {
        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockBottomAutoHide; }
        }

        public LogForm()
        {
            InitializeComponent();
            controlSink1.Capture = true;
        }
    }
}