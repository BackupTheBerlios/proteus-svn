using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Proteus.Framework.Parts;

namespace Proteus.Editor.DockForms
{
    public partial class ActorTypeBrowserForm : DockableForm
    {
        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockRight; }
        }

        private int GetBaseCount(string name)
        {
            IActor topActor = Factory.Instance.Create(name);
            int baseCount = 0;
            if (topActor != null)
            {
                while (topActor.BaseType != string.Empty)
                {
                    string baseName = topActor.BaseType;
                    baseCount ++;
                    topActor.Dispose();
                    topActor = Factory.Instance.Create( baseName );
                    if ( topActor == null )
                        break;
                }
            }

            if( topActor != null)
                topActor.Dispose();

            return baseCount;
        }

        private string GetBaseName(string name)
        {
            IActor topActor = Factory.Instance.Create(name);
            string baseName = topActor.BaseType;
            topActor.Dispose();
            return baseName;
        }

        private string[] GetAllWithBaseCount(int baseCount)
        {
            string[] actorTypeNames = Framework.Parts.Factory.Instance.AllTypes;
            List<string> tempList = new List<string>();

            foreach (string s in actorTypeNames)
            {
                if ( GetBaseCount( s ) == baseCount )
                    tempList.Add( s );
            }

            return tempList.ToArray();
        }

        private void Build()
        {
            TreeNode rootNode = new TreeNode();

            BuildStep( rootNode,0 );

            foreach( TreeNode n in rootNode.Nodes )
                treeView1.Nodes.Add(n);
        }

        private void BuildStep(TreeNode node, int baseCount)
        {
            // Get the actors.
            string[] typeNames = GetAllWithBaseCount(baseCount);

            foreach (string s in typeNames)
            {
                IActor topActor = Factory.Instance.Create(s);
                
                string description = topActor.Description;
                string documentation = topActor.Documentation;
                
                topActor.Dispose();

                if (baseCount == 0)
                {
                    TreeNode subNode = node.Nodes.Add(s);
                    subNode.ToolTipText = description;

                    BuildStep( subNode,1 );
                }
                else
                {
                    // Get the base type names
                    if (GetBaseName(s) == node.Text)
                    {
                        TreeNode subNode = node.Nodes.Add(s);
                        subNode.ToolTipText = description;
                        
                        BuildStep(subNode,baseCount + 1);
                    }
                }
            }
        }

        protected override object OnDragRequest(int x, int y, MouseButtons buttons)
        {
            if (treeView1.SelectedNode != null)
            {
                // Create an actor.
                IActor newActor = Framework.Parts.Factory.Instance.Create( treeView1.SelectedNode.Text );

                if (newActor != null)
                {
                    newActor.Name = "Unknown" + newActor.TypeName; 
                    Kernel.Configuration.Chunk actorChunk = Framework.Parts.Utility.WriteActor(newActor);
                    string content = Kernel.Configuration.XmlDocument.GetChunkXml(actorChunk);

                    newActor.Dispose();

                    return content;
                }
            }
            return null;
        }

        public ActorTypeBrowserForm()
        {
            InitializeComponent();
            ActivateDrag();
            Build();
        }
    }
}