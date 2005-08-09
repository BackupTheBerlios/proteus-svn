using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public sealed class PlugCollection
    {
        private List<IInputPlug>    inputPlugs  = new List<IInputPlug>();
        private List<IOutputPlug>   outputPlugs = new List<IOutputPlug>();

        public void Add(IInputPlug plug)
        {
            inputPlugs.Add(plug);
        }

        public void Add(IOutputPlug plug)
        {
            outputPlugs.Add(plug);
        }

        public void Remove(IInputPlug plug)
        {
            inputPlugs.Remove(plug);
        }

        public void Remove(IOutputPlug plug)
        {
            outputPlugs.Remove(plug);
        }

        public IInputPlug[] GetInputPlugs(IActor actor, IOutputPlug plug)
        {
            List<IInputPlug> tempList = new List<IInputPlug>();

            foreach (IInputPlug p in inputPlugs)
            {
                // Cases to add.
                if (plug != null)
                {
                    if (plug.IsCompatible(p))
                        tempList.Add(p);
                }
                else if (actor != null)
                {
                    foreach (IOutputPlug op in actor.GetOutputPlugs(null, null))
                    {
                        if (op.IsCompatible(p) )
                        {
                            tempList.Add(p);
                            break;
                        }
                    }
                }
                else
                {
                    tempList.Add(p);
                }
            }

            return tempList.ToArray();
        }

        public IOutputPlug[] GetOutputPlugs(IActor actor, IInputPlug plug)
        {
            List<IOutputPlug> tempList = new List<IOutputPlug>();

            foreach (IOutputPlug p in outputPlugs)
            {
                // Cases to add.
                if (plug != null)
                {
                    if (p.IsCompatible(plug))
                        tempList.Add(p);
                }
                else if (actor != null)
                {
                    foreach (IInputPlug ip in actor.GetInputPlugs(null, null))
                    {
                        if (p.IsCompatible(ip))
                        {
                            tempList.Add(p);
                            break;
                        }
                    }
                }
                else
                {
                    tempList.Add(p);
                }
            }

            return tempList.ToArray();
        }

        public PlugCollection()
        {
        }

        public PlugCollection(IActor owner)
        {
            inputPlugs.AddRange(Default.MethodPlug.Enumerate(owner));
            inputPlugs.AddRange(Default.InterfacePlug.Enumerate(owner));

            outputPlugs.AddRange(Default.PropertyPlug.Enumerate(owner));
            outputPlugs.AddRange(Default.EventPlug.Enumerate(owner));
        }
    }
}
