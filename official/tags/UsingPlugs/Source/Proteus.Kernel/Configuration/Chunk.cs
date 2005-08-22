using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Configuration
{
    /// <summary>
    /// The basic node in a configuration document.
    /// </summary>
    public sealed class Chunk
    {
        private SortedList<string, string>  chunkValues     =
            new SortedList<string, string>();
        
        private List<Chunk>                 chunkChildren   =
            new List<Chunk>();

        private string                      chunkName       = string.Empty;

        /// <summary>
        /// Returns the first chunk of the given name or null if not found.
        /// </summary>
        /// <param name="name">The name of the subchunk to retrieve.</param>
        /// <returns>The subchunk or null on error.</returns>
        public Chunk this[string name]
        {
            get
            {
                foreach (Chunk c in chunkChildren)
                {
                    if (c.Name == name)
                    {
                        return c;
                    }
                }

                return null;
            }
        }
        
        /// <summary>
        /// The name of this chunk.
        /// </summary>
        public string Name
        {
            get { return chunkName; }
        }

        /// <summary>
        /// Access to a sorted list of all chunk values.
        /// </summary>
        public SortedList<string, string> Values
        {
            get { return chunkValues; }
        }

        /// <summary>
        /// Enumerator for sub chunks.
        /// </summary>
        /// <returns></returns>
        public IEnumerator<Chunk> GetEnumerator()
        {
            foreach (Chunk c in chunkChildren)
                yield return c;
        }

        public Chunk[] GetChildrenByName(string name)
        {
            List<Chunk> temp = new List<Chunk>();
            foreach (Chunk c in chunkChildren)
            {
                if (c.Name == name)
                    temp.Add(c);
            }

            return temp.ToArray();
        }

        public object GetValueObject(string name,Type t)
        {
            if (chunkValues.ContainsKey(name))
            {
                return Reflection.Converter.ConvertObject(chunkValues[name], t);
            }
            return null;
        }

        public object GetValueObject(string name,object def)
        {
            if (chunkValues.ContainsKey(name))
            {
                return Reflection.Converter.ConvertObject(chunkValues[name], def);
            }
            return def;
        }

        public ValueType GetValue<ValueType>(string name)
        {
            if (chunkValues.ContainsKey(name))
            {
                return Reflection.Converter.Convert<string, ValueType>(chunkValues[name]);
            }
            return default(ValueType);
        }

        public ValueType GetValue<ValueType>(string name, ValueType def)
        {
            if (chunkValues.ContainsKey(name))
            {
                return Reflection.Converter.Convert( chunkValues[name],def );
            }
            return def;
        }

        public bool SetValue(string name, object val)
        {
            if (val.GetType() == typeof(string))
            {
                chunkValues[name] = (string)val;
                return true;
            }
            else
            {
                if (Reflection.Converter.IsObjectConvertible(typeof(string), val.GetType()))
                {
                    chunkValues[name] = val.ToString();
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Adds a new sub chunk to this chunk.
        /// </summary>
        /// <param name="chunk">The chunk to add.</param>
        public void Add(Chunk chunk)
        {
            chunkChildren.Add( chunk );
        }

        /// <summary>
        /// Removes a chunk again from this one.
        /// </summary>
        /// <param name="chunk">The chunk to remove.</param>
        public void Remove(Chunk chunk)
        {
            chunkChildren.Remove(chunk);
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public Chunk()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="name">The name of the chunk.</param>
        public Chunk(string name)
        {
            chunkName = name;
        }
    }
}
