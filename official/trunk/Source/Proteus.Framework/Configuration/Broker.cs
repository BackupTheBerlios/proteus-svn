using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

using Kc = Proteus.Kernel.Configuration;

namespace Proteus.Framework.Configuration
{
    public static class Broker
    {
        public static void ReadConfiguration(Parts.IActor actor, Kc.Chunk inputChunk)
        {
            Parts.IProperty[] properties = actor.Properties;

            foreach (Parts.IProperty p in properties)
            {
                ReadValue(inputChunk, p);
            }
        }

        public static void WriteConfiguration(Parts.IActor actor, Kc.Chunk outputChunk)
        {
            Parts.IProperty[] properties = actor.Properties;

            foreach (Parts.IProperty p in properties)
            {
                WriteValue(outputChunk, p);
            }
        }

        public static bool ReadValue(Kc.Chunk inputChunk,Parts.IProperty property )
        {                
            if ( property.DefaultValue != null)
            {
                object valueObject = inputChunk.GetValueObject(property.Name, property.DefaultValue );
                property.CurrentValue = valueObject;
                return true;
            }
            return false;
        }

        public static bool WriteValue( Kc.Chunk outputChunk,Parts.IProperty property)
        {         
            if ( property.CurrentValue != null)
            {
                outputChunk.SetValue(property.Name, property.CurrentValue );
                return true;
            }
     
            return false;
        }
    }
}
