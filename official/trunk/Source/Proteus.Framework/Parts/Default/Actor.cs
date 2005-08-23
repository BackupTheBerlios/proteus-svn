using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts.Default
{
    public abstract class Actor : Kernel.Pattern.Disposable,IActor
    {
        protected string            actorName               = string.Empty;
        protected bool              actorActive             = true;
        protected IEnvironment      actorEnvironment        = null;
        protected MessageTable      actorMessageTable       = new MessageTable();
        protected MessageDebugger   actorMessageDebugger    = null;

        private static Kernel.Diagnostics.Log<Actor> log =
            new Kernel.Diagnostics.Log<Actor>();

        #region IActor Members

        public virtual bool Active
        {
            get
            {
                return actorActive;
            }
            set
            {
                actorActive = value;
            }
        }

        public virtual string TypeName
        {
            get
            {
                return Utility.GetTypeName(this);
            }
        }

        public virtual string BaseType
        {
            get
            {
                return Utility.GetBaseType(this);
            }
        }

        public virtual IEnvironment Environment
        {
            get { return actorEnvironment; }
        }

        public virtual IProperty[] Properties
        {
            get { return Default.Property.Enumerate(this); }
        }

        public virtual bool Update(double deltaTime)
        {
            if (actorActive)
            {
                return this.OnUpdate(deltaTime);
            }
            return true;
        }

        protected virtual bool OnUpdate(double deltaTime)
        {
            return true;
        }

        public virtual bool Initialize(IEnvironment environment)
        {
            if (environment != null)
            {
                bool success = environment.Add(this);
                if (success)
                {
                    this.actorEnvironment = environment;
                    return true;
                }
                else
                {
                    log.Warning("Actor name already in environment: {0}", this.Name);
                }
            }
            else
            {
                log.Warning("No environment to register this actor [{0}].", this.Name);
            }
            return false;
        }

        public virtual object SendMessage(string name,IActor sender, params object[] parameters)
        {
            if (actorMessageDebugger.Active)
            {
                return actorMessageDebugger.InterceptMessage( this,name,sender,parameters );
            }
            else
            {
                return actorMessageTable.SendMessage(name, sender, parameters);
            }
        }

        public virtual InterfaceType QueryInterface<InterfaceType>() where InterfaceType : class
        {
            return this as InterfaceType;
        }

        #endregion

        #region IConfigureable Members

        public virtual bool ReadConfiguration(Proteus.Kernel.Configuration.Chunk chunk)
        {
            // Read name first.
            this.actorName = chunk.GetValue("Name", "Unknown");

            Configuration.Broker.ReadConfiguration(this, chunk);
            return true;
        }

        public virtual bool WriteConfiguration(Proteus.Kernel.Configuration.Chunk chunk)
        {
            Configuration.Broker.WriteConfiguration(this, chunk);
            return true;
        }

        #endregion

        #region IPart Members

        [Property()]
        [Documentation("The name of the actor instance.","The name of this actor instance, has to be unique in its environment.")]
        public virtual string Name
        {
            get { return actorName; }
            set
            {
                if (actorEnvironment != null && value != actorName )
                {
                    if (!actorName.Contains(value))
                    {
                        actorEnvironment.Remove( this );
                        actorName = value;
                        actorEnvironment.Add(this);
                    }
                }
            }
        }

        public virtual string Description
        {
            get 
            {
                return Utility.GetDescription(this);
            }
        }

        public virtual string Documentation
        {
            get
            {
               return Utility.GetDocumentation(this);
            }
        }

        #endregion

        protected override void ReleaseManaged()
        {
            if (this.Environment != null)
            {
                this.Environment.Remove(this);
            }
        }

        protected override void ReleaseUnmanaged()
        {
        }

        protected void InitializeDebugServices()
        {
            actorMessageDebugger = new MessageDebugger( new MessageDebugger.DebugMessageDelegate(actorMessageTable.SendMessage) );
        }

        public override string ToString()
        {
            return this.TypeName + ":" + this.Name;
        }

        protected Actor()
        {
            InitializeDebugServices();
        }  
    }
}
