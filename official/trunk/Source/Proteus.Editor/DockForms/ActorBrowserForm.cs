using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class ActorBrowserForm : DockableForm
    {
        public virtual Framework.Parts.IActor Actor
        {
            set { }
        }

        private void Build()
        {
            treeView1.Nodes.Clear();
            TreeNode rootNode = BuildStep( Framework.Parts.Basic.RootActor.Instance );
            
            if ( rootNode != null )
                treeView1.Nodes.Add( rootNode );
        }

        private TreeNode BuildStep( Framework.Parts.IActor actor )
        {
            if (actor != null)
            {
                TreeNode newNode = new TreeNode();

                newNode.Text = actor.Name;
                newNode.ToolTipText = actor.TypeName;

                Framework.Parts.IActorCollection collection = actor.QueryInterface<Framework.Parts.IActorCollection>();

                if (collection != null)
                {
                    foreach (Framework.Parts.IActor a in collection)
                    {
                        newNode.Nodes.Add(BuildStep(a));
                    }
                }

                return newNode;
            }
            return null;
        }

        public ActorBrowserForm()
        {
            InitializeComponent();
            Build();
        }
    }
}