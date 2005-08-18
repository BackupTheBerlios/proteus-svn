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
        internal sealed class ActorNode : TreeNode
        {
            private Framework.Parts.IActor actor = null;

            public Framework.Parts.IActor Actor
            {
                set { actor = value; }
                get { return actor; }
            }
        }
        
        public virtual Framework.Parts.IActor Actor
        {
            set { }
        }

        private void Build()
        {
            treeView1.Nodes.Clear();
            ActorNode rootNode = BuildStep( Framework.Parts.Basic.RootActor.Instance );
            
            if ( rootNode != null )
                treeView1.Nodes.Add( rootNode );
        }

        private ActorNode BuildStep( Framework.Parts.IActor actor )
        {
            if (actor != null)
            {
                ActorNode newNode = new ActorNode();

                newNode.Text        = actor.Name;
                newNode.ToolTipText = actor.TypeName;
                newNode.Actor       = actor;

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