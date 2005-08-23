using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Proteus.Framework.Parts
{
    public sealed class MessageRouter
    {
        public sealed class Parameter
        {
            private int     sourcePosition  = 0;
            private int     targetPosition  = 0;
            private Type    targetType      = null;
            private object  targetValue     = null;
            private bool    makeNull        = false;

            public void Remap(object[] inputParameters, object[] outputParameters)
            {
                if (makeNull)
                {
                    outputParameters[targetPosition] = null;
                }
                else
                {
                    if (targetValue != null)
                    {
                        outputParameters[targetPosition] = targetValue;
                    }
                    else
                    {
                        // Perform complex remapping.
                        outputParameters[targetPosition] = inputParameters[sourcePosition];

                        if (targetType != null)
                        {
                            outputParameters[targetPosition] = Kernel.Reflection.Converter.ConvertObject( outputParameters[targetPosition],targetType );
                        }
                    }
                }
            }
        }

        public sealed class Remapper
        {
            private List<Parameter>    remappers    = new List<Parameter>();
            private List<object>       adders       = new List<object>();
            private List<int>          removers     = new List<int>();
            private string             name         = string.Empty;

            public string Remap(string messageName,ref object[] parameters)
            {
                // First add any parameters.
                if (adders.Count > 0)
                {
                    object[] newParameters = new object[ parameters.Length + adders.Count ];
                    parameters.CopyTo(newParameters,0);

                    for (int i = 0; i < adders.Count; i++)
                    {
                        newParameters[i+parameters.Length] = adders[i];
                    }

                    parameters = newParameters;
                }

                // Removers, the most costly operation.
                if (removers.Count > 0)
                {
                    List<object> newParameters = new List<object>();
                    newParameters.AddRange( parameters );
                    
                    for (int i = 0; i < removers.Count; i++)
                    {
                        newParameters.RemoveAt(removers[i]);
                    }

                    parameters = newParameters.ToArray();
                }

                // And now perform remapping
                if (remappers.Count > 0)
                {
                    object[] newParameters = new object[parameters.Length];
                    foreach( Parameter m in remappers )
                        m.Remap( parameters,newParameters );

                    parameters = newParameters;
                }

                // Message name
                if ( name != string.Empty )
                    return name;

                return messageName;
            }
        }

        public class Route : IComparable<Route>
        {
            private Regex       nameRegex   = null;
            private IActor      targetActor = null;
            private Remapper    remapper    = null;

            public bool Matches(string name)
            {
                Match match = nameRegex.Match(name);

                if (match.Success && match.Length == name.Length)
                {
                    return true;
                }
                return false;
            }

            public object SendMessage(string name, IActor sender, params object[] parameters)
            {
                if (targetActor != null)
                {
                    if (remapper != null)
                    {
                        name = remapper.Remap( name,ref parameters );
                    }
                 
                    return targetActor.SendMessage(name, sender, parameters);
                }
                return null;
            }

            #region IComparable<Route> Members

            public int CompareTo(Route other)
            {
                return this.nameRegex.ToString().Length.CompareTo( other.nameRegex.ToString().Length );
            }

            #endregion

            public Route()
            {
                nameRegex = new Regex("/w*");
            }

            public Route(string pattern, IActor actor)
            {
                targetActor = actor;
                nameRegex = new Regex( pattern );
            }
        }
        
        private List<Route> routes = new List<Route>();

        public bool Add(string pattern, IActor target)
        {
            routes.Add( new Route( pattern,target ) );
            routes.Sort();
            return true;
        }

        public object SendMessage(string name,IActor sender,params object[] parameters)
        {
            foreach( Route r in routes )
            {
                if ( r.Matches(name) )
                {
                    r.SendMessage( name,sender,parameters );
                }
            }
            return null;
        }

        public MessageRouter()
        {
        }
    }
}
