using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace Proteus.Framework.Parts.Default
{
    public sealed class PropertyPlug : OutputPlug
    {
        private IProperty property = null;

        public IProperty Property
        {
            get { return property; }
        }

        public override bool IsCompatible(IInputPlug inputPlug)
        {
            InterfacePlug plug = inputPlug as InterfacePlug;

            if (plug != null)
            {
                if (plug.InterfaceType.Equals(Property.Type))
                    return true;
            }

            return false;
        }

        public override IConnection Connect(IInputPlug inputPlug)
        {
            if (IsCompatible(inputPlug))
            {
                if (property.CurrentValue == null)
                {
                    InterfacePlug plug = (InterfacePlug)inputPlug;

                    property.CurrentValue = plug.Owner.QueryInterface(plug.InterfaceType);

                    PropertyInterfaceConnection connection = new PropertyInterfaceConnection(this, inputPlug);

                    if (!inputPlug.OnConnection(connection))
                    {
                        connection.Dispose();
                        return null;
                    }

                    this.connections.Add(connection);
                    return connection;
                }
            }

            return null;
        }

        public static PropertyPlug Create(IProperty property )
        {
            if ( property.IsPlug )
            {
                return new PropertyPlug(property);
            }

            return null;
        }

        public static PropertyPlug[] Enumerate(IActor owner)
        {
            List<PropertyPlug> list = new List<PropertyPlug>();

            foreach (IProperty p in owner.Properties )
            {
                PropertyPlug newPlug = Create(p);
                if (newPlug != null)
                    list.Add(newPlug);
            }

            return list.ToArray();
        }

        private PropertyPlug(IProperty _property)
            : base( _property.Name,_property.Description,_property.Documentation,false,_property.Owner )
        {
            property = _property;
        }
    }
}
