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
        private Utility.PropertyTable       displayProperties   = null;
        private Framework.Parts.IActor      currentActor        = null;

        public override WeifenLuo.WinFormsUI.DockState DefaultDockState
        {
            get { return WeifenLuo.WinFormsUI.DockState.DockRight; }
        }

        public Proteus.Framework.Parts.IActor Actor
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
                displayProperties[ p.Name ] = p.CurrentValue;
            }

            propertyGrid1.SelectedObject = displayProperties;
            displayProperties.SetValue += new Proteus.Editor.Utility.PropertySpecEventHandler(displayProperties_SetValue);
        }

        private void displayProperties_SetValue(object sender, Proteus.Editor.Utility.PropertySpecEventArgs e)
        {
            // Find the corresponding property on the actor and transfer.
            Framework.Parts.IProperty[] properties = currentActor.Properties;

            foreach (Framework.Parts.IProperty p in properties)
            {
                if (p.Name == e.Property.Name)
                {
                    // Transfer value.
                    p.CurrentValue = e.Value;

                    // Walk up the stack.
                    Framework.Parts.Basic.ConfigFileActor configActor = Utility.Actor.FindConfigFile(currentActor);
                    if (configActor != null)
                    {
                        configActor.WriteFile();
                        configActor.ReadFile();
                        Manipulation.Manager.Instance.AfterActorModified( configActor );
                    }

                    break;
                }
            }
        }

        private void Instance_SelectionChanged(Proteus.Framework.Parts.IActor selectedActor, List<Proteus.Framework.Parts.IActor> selectedActors)
        {
            this.Actor = selectedActor;
        }

        public PropertyBrowserForm()
        {
            InitializeComponent();
            Manipulation.Manager.Instance.SelectionChanged += new Proteus.Editor.Manipulation.Manager.SelectionDelegate(Instance_SelectionChanged);
            this.Actor = Framework.Parts.Basic.RootActor.Instance;
        }
    }
}