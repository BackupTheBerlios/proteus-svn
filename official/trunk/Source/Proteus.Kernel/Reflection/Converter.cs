using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System.ComponentModel;

namespace Proteus.Kernel.Reflection
{
    public static class Converter
    {
        public static bool IsObjectConvertible(Type sourceType, Type targetType)
        {   
            // Special case for string conversion.
            if (targetType == typeof(string))
                return true;

            // Primtive types.
            if (sourceType.IsPrimitive && targetType.IsPrimitive)
            {
                return true;
            }

            // Check for IConvertible interface ( performance optimization )

            // Use generic reflection.
            TypeConverter sourceConverter = TypeDescriptor.GetConverter(sourceType);
            TypeConverter targetConverter = TypeDescriptor.GetConverter(targetType);

            if (targetConverter != null)
            {
                if (targetConverter.CanConvertFrom(sourceType))
                {
                    return true;
                }
            }
            if (sourceConverter != null)
            {
                if (sourceConverter.CanConvertTo(targetType))
                {
                    return true;
                }
            }
                        
            return false;
        }

        public static bool IsObjectConvertible(object source, Type targetType)
        {
            return IsObjectConvertible(source.GetType(), targetType);
        }

        public static object ConvertObject(object val, Type targetType)
        {
            try
            {
                Type sourceType = val.GetType();

                // Special case for string conversion.
                if (targetType == typeof(string))
                    return val.ToString();

                // Primtive types.
                if (sourceType.IsPrimitive && targetType.IsPrimitive)
                {
                    IFormatProvider provider = CultureInfo.CurrentCulture.NumberFormat;

                    if ( targetType == typeof(DateTime) || sourceType == typeof(DateTime) )
                    {
                        provider = CultureInfo.CurrentCulture.DateTimeFormat;
                    }
                   
                    return System.Convert.ChangeType(val, targetType,provider );
                }

                // Check for IConvertible interface ( performance optimization )

                // Use generic reflection.
                TypeConverter sourceConverter = TypeDescriptor.GetConverter(sourceType);
                TypeConverter targetConverter = TypeDescriptor.GetConverter(targetType);

                if (targetConverter != null)
                {
                    if (targetConverter.CanConvertFrom(sourceType))
                    {
                        return targetConverter.ConvertFrom(val);
                    }
                }
                if (sourceConverter != null)
                {
                    if (sourceConverter.CanConvertTo(targetType))
                    {
                        return sourceConverter.ConvertTo(val, targetType);
                    }
                }
            }
            catch (System.InvalidCastException)
            {
            }
            catch (System.ArgumentException)
            {
            }
            
            return null;
        }

        public static object ConvertObject(object val, object def)
        {
            object result = ConvertObject(val, def.GetType());
            if (result == null)
                result = def;

            return result;
        }

        public static TargetType Convert<SourceType, TargetType>(SourceType val)
        {
            object result = ConvertObject(val, typeof(TargetType));
            if (result == null)
                return default(TargetType);

            return (TargetType)result;
        }

        public static TargetType Convert<SourceType, TargetType>(SourceType val, TargetType def)
        {
            return (TargetType)ConvertObject(val, def);
        }
    }
}
