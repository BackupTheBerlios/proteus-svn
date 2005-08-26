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

        private ActorNode FindNode(Framework.Parts.IActor actor)
        {
            return FindStep( treeView1.Nodes,actor );
        }

        private ActorNode FindStep(TreeNodeCollection nodes, Framework.Parts.IActor actor)
        {
            foreach (ActorNode n in nodes)
            {
                if ( n.Actor == actor )
                    return n;

                ActorNode subNode = FindStep( n.Nodes,actor );

                if ( subNode != null )
                    return subNode;
            }

            return null;
        }

        private void treeView1_AfterSelect(object sender, System.Windows.Forms.TreeViewEventArgs e)
        {
            ActorNode node = (ActorNode)e.Node;
            Manipulation.Manager.Instance.SelectedActor = node.Actor;
        }

        private void treeView1_NodeMouseDoubleClick(object sender, System.Windows.Forms.TreeNodeMouseClickEventArgs e)
        {
            Manipulation.Manager.Instance.PerformDefaultAction();
        }

        private void Instance_ActorModified(Proteus.Framework.Parts.IActor actor)
        {
            ActorNode newActorNode = BuildStep( actor );

            // Find the actor node that corresponds to the old actor.
            ActorNode oldNode = FindNode( actor );
            if (oldNode != null)
            {
                ActorNode parentNode = (ActorNode)oldNode.Parent;
                parentNode.Nodes.Remove( oldNode );
                parentNode.Nodes.Add( newActorNode );
            }
        }

        public ActorBrowserForm()
        {
            InitializeComponent();
            Manipulation.Manager.Instance.ActorModified += new Proteus.Editor.Manipulation.Manager.ActorDelegate(Instance_ActorModified);
            Build();
        }     
    }
}