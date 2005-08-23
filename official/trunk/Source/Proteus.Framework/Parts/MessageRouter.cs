using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Proteus.Framework.Parts
{
    public sealed class MessageRouter
    {
        public class Route : IComparable<Route>
        {
            private Regex   nameRegex   = null;
            private IActor  targetActor = null;

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
                if ( targetActor != null )
                    return targetActor.SendMessage( name,sender,parameters );
                
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
