using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Framework.Parts;
using Proteus.Framework.Parts.Basic;

namespace Proteus.Editor.Utility
{
    public static class Actor
    {
        public static ConfigFileActor FindConfigFile(IActor actor)
        {
            if (actor.Environment != null)
            {
                IActor currentActor = actor.Environment.Owner;

                if (currentActor != null)
                {
                    while (!(currentActor is ConfigFileActor))
                    {
                        currentActor = currentActor.Environment.Owner;

                        if (currentActor == null)
                            break;
                    }

                    if (currentActor != null)
                    {
                        if (currentActor is ConfigFileActor)
                        {
                            return (ConfigFileActor)currentActor;
                        }
                    }
                }
            }

            return null;
        }
    }
}
