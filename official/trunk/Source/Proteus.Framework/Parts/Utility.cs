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

        public static IConnection ReadConnection( Chunk connectionChunk,IEnvironment environment )
        {
            if (connectionChunk.Name == "Connection")
            {
                string sourceActor = connectionChunk.GetValue("SourceActor", string.Empty);
                string targetActor = connectionChunk.GetValue("TargetActor", string.Empty);
                string sourcePlug = connectionChunk.GetValue("SourcePlug", string.Empty);
                string targetPlug = connectionChunk.GetValue("TargetPlug", string.Empty);

                if (sourceActor != string.Empty &&
                        targetActor != string.Empty &&
                        sourcePlug != string.Empty &&
                        targetPlug != string.Empty)
                {
                    IConnection newConnection = Utility.Connect(environment,
                                            sourceActor,
                                            sourcePlug,
                                            targetActor,
                                            targetPlug);

                    if (newConnection == null)
                    {
                        // Error connection not possible.
                        return null;
                    }
                    else
                    {
                        environment.Add(newConnection);
                        return newConnection;
                    }
                }
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

        public static Chunk WriteConnection(IConnection connection)
        {
            Chunk conChunk = new Chunk("Connection");

            conChunk.SetValue("SourceActor", connection.Source.Owner.Name);
            conChunk.SetValue("SourcePlug", connection.Source.Name);

            conChunk.SetValue("TargetActor", connection.Target.Owner.Name);
            conChunk.SetValue("TargetPlug", connection.Target.Name);

            return conChunk;
        }

        public static IConnection Connect(  IEnvironment environment,
                                            string sourceActorName,
                                            string sourcePlugName,
                                            string targetActorName,
                                            string targetPlugName )
        {
            IActor sourceActor = environment[sourceActorName];
            IActor targetActor = environment[targetActorName];

            if (sourceActor != null && targetActor != null)
            {
                IOutputPlug[] outputPlugs = sourceActor.GetOutputPlugs(targetActor, null);
            
                IOutputPlug sourcePlug = null;

                foreach (IOutputPlug op in outputPlugs)
                {
                    if (op.Name == sourcePlugName)
                    {
                        sourcePlug = op;
                        break;
                    }
                }

                if ( sourcePlug != null )
                {
                    IInputPlug[] inputPlugs = targetActor.GetInputPlugs(sourceActor, sourcePlug);
                    foreach (IInputPlug ip in inputPlugs)
                    {
                        if (ip.Name == targetPlugName)
                        {
                            // Found everything.
                            IConnection connection = sourcePlug.Connect(ip);

                            if (connection != null)
                            {
                                environment.Add(connection);
                                return connection;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }
}
