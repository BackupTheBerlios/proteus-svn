using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Kernel.Diagnostics
{
    public partial class ControlSink : UserControl,ISink
    {
        private TreeNode    currentNode = null;
        private bool        registered  = false;

        #region ISink Members

        public bool CaptureLog
        {
            set
            {
                if (value == true && !registered)
                {
                    // Auto register when placed on form.
                    Manager.Instance.AddSink(this);
                    registered = true;
                }
                else if ( value == false && registered )
                {
                    Manager.Instance.RemoveSink(this);
                    registered = false;
                }
            }
        }

        public bool Initialize(string initParam)
        {
            return true;
        }

        public void BeginRegion(string name)
        {
            currentNode = currentNode.Nodes.Add(name);
        }

        public void EndRegion()
        {
            currentNode = currentNode.Parent;
        }

        public void BeginMessage(LogLevel level, Context context)
        {
            currentNode = currentNode.Nodes.Add(level.ToString());

            if (level == LogLevel.Warning)
            {
                currentNode.BackColor = Color.Yellow;
            }
            else if (level == LogLevel.Error)
            {
                currentNode.BackColor = Color.Red;
            }
            
            foreach (IContextInfo c in context)
            {
                currentNode.Nodes.Add(c.Name + " : " + c.Text);
            }
        }

        public void MessageContent(string content)
        {
            currentNode.Nodes.Add(content);
        }

        public void EndMessage()
        {
            currentNode = currentNode.Parent;
        }

        public void Exception(Exception exception, bool mainThread, bool isTerminating, Context context)
        {
        }

        public void Assert(string condition, string message, Context context)
        {
        }

        #endregion

        public ControlSink()
        {
            InitializeComponent();

            // Create root node.
            currentNode = treeView1.Nodes.Add("Log:");
        }
    }
}
