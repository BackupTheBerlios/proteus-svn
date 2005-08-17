using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Dp = WeifenLuo.WinFormsUI;

namespace Proteus.Editor.DockForms
{
    public partial class DockableForm : Dp.DockContent
    {
        public virtual Dp.DockState DefaultDockState
        {
            get { return Dp.DockState.DockLeft; }
        }

        public virtual bool IsDocumentHost
        {
            get { return false; }
        }

        public virtual Framework.Parts.IActor Actor
        {
            set { }
        }

        public DockableForm()
        {
            InitializeComponent();
        }
    }
}