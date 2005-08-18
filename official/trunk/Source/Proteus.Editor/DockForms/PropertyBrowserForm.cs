using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Proteus.Editor.DockForms
{
    public partial class PropertyBrowserForm : DockableForm
    {
        private Utility.PropertyTable   displayProperties   = null;
        private Framework.Parts.IActor  currentActor        = null;

        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockRight; }
        }

        public override Proteus.Framework.Parts.IActor Actor
        {
            set 
            {
                if (value != null)
                {
                    currentActor = value;
                    Build(currentActor);
                }
            }
        }

        private void Build(Framework.Parts.IActor actor)
        {
            displayProperties = new Utility.PropertyTable();
            Proteus.Framework.Parts.IProperty[] properties = currentActor.Properties;

            foreach (Proteus.Framework.Parts.IProperty p in properties)
            {
                // Now set it.
                TypeConverter   converter   = TypeDescriptor.GetConverter( p.Type );
                Type            editorType  = p.EditorType;

                Utility.PropertySpec spec = null;

                if (converter != null && editorType != null)
                {
                    spec = new Utility.PropertySpec(p.Name, p.Type, p.Category, p.Description, p.DefaultValue,editorType,converter.GetType() );
                }
                else
                {
                    spec = new Utility.PropertySpec(p.Name, p.Type, p.Category, p.Description, p.DefaultValue);
                }

                // Store it.
                displayProperties.Properties.Add( spec );
            }

            propertyGrid1.SelectedObject = displayProperties;
        }

        private void Instance_SelectionChanged(Proteus.Framework.Parts.IActor selectedActor, List<Proteus.Framework.Parts.IActor> selectedActors)
        {
            this.Actor = selectedActor;
        }

        public PropertyBrowserForm()
        {
            InitializeComponent();
            Manipulation.Manager.Instance.SelectionChanged += new Proteus.Editor.Manipulation.Manager.SelectionDelegate(Instance_SelectionChanged);
        }
    }
}