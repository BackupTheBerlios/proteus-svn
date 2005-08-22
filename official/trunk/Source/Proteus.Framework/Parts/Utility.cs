using System;
using System.Collections.Generic;
using System.Text;

using Proteus.Kernel.Configuration;

namespace Proteus.Framework.Parts
{
    public class Utility
    {
        private static Kernel.Diagnostics.Log<Utility> log =
            new Kernel.Diagnostics.Log<Utility>();
        
        public static string GetTypeName(Type actorType)
        {
            ActorAttribute actorAttribute = Attribute.GetCustomAttribute(actorType, typeof(ActorAttribute),false) as ActorAttribute;
            if (actorAttribute != null)
            {
                return actorAttribute.Name;
            }
            else
            {
                return actorType.Name;
            }
        }

        public static string GetBaseType(Type actorType)
        {
            ActorAttribute actorAttribute = Attribute.GetCustomAttribute(actorType, typeof(ActorAttribute), false) as ActorAttribute;
            if (actorAttribute != null)
            {
                if ( actorAttribute.BaseName != string.Empty )
                    return actorAttribute.BaseName;
            }
           
            Type realBaseType = actorType.BaseType;
            if (realBaseType != null)
            {
                if (realBaseType.GetInterface(typeof(IActor).FullName) != null)
                {
                    if (!realBaseType.IsAbstract)
                    {
                        return Utility.GetTypeName(realBaseType);
                    }
                }
            }
            return string.Empty;          
        }

        public static string GetBaseType(IActor actor)
        {
            return GetBaseType( actor.GetType() );
        }

        public static string GetTypeName(IActor actor)
        {
            return GetTypeName(actor.GetType());
        }

        public static IActor ReadActor(Chunk actorChunk,IEnvironment environment )
        {
            if (actorChunk != null)
            {
                if (actorChunk.Name == "Actor")
                {
                    string typeName = actorChunk.GetValue("Type", string.Empty);

                    if (typeName != string.Empty)
                    {
                        // Create sub actor.
                        IActor newActor = Factory.Instance.Create(typeName);

                        if (newActor != null)
                        {
                            // Pass configuration on.
                            bool subSuccess = newActor.ReadConfiguration(actorChunk);

                            if (subSuccess)
                            {
                                if (!newActor.Initialize(environment))
                                {
                                    log.Warning("Actor could not be initialized: {0}:{1}", typeName, newActor.Name);
                                    // Error name twice.
                                    return null;
                                }

                                log.Debug("Actor created: {0}:{1}.", newActor.Name, newActor.TypeName);

                                return newActor;
                            }
                        }
                        else
                        {
                            log.Warning("Unable to create actor of type: {0}", typeName);
                        }
                    }
                    else
                    {
                        log.Warning("No actor type found.");
                    }
                }
                else
                {
                    log.Warning("Not an actor chunk, but [{0}] instead.", actorChunk.Name);
                }
            }
            else
            {
                log.Warning("No actor chunk given.");
            }

            return null;
        }

        public static Chunk WriteActor(IActor actor)
        {
            // Create chunk for it.
            Chunk actorChunk = new Chunk("Actor");

            actorChunk.SetValue("Type", actor.TypeName);
            actorChunk.SetValue("Name", actor.Name);
            bool subSuccess = actor.WriteConfiguration(actorChunk);

            if (!subSuccess)
            {
                return null;
            }

            return actorChunk;
        }
    }
}
