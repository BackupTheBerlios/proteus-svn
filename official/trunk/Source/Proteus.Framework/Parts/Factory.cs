using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Framework.Parts
{
    public sealed class Factory : Kernel.Pattern.Singleton<Factory>
    {
        private Kernel.Pattern.TypeFactory<IActor> typeFactory =
            new Kernel.Pattern.TypeFactory<IActor>();

        public Kernel.Pattern.TypeFactory<IActor> TypeFactory
        {
            get { return typeFactory; }
        }

        public IActor Create(string name)
        {
            return typeFactory.Create(name);
        }

        public void Register<ActorType>() where ActorType : IActor,new()
        {
            typeFactory.Register<ActorType>(Utility.GetTypeName(typeof(ActorType)));
        }

        public void Unregister<ActorType>() where ActorType : IActor, new()
        {
            typeFactory.Unregister<ActorType>();
        }

        /// <summary>
        /// Registers all default actors provided by the framework
        /// module.
        /// </summary>
        public Factory()
        {
            Register<Parts.Basic.ConfigFileActor>();
            Register<Parts.Basic.GroupActor>();
        }
    }
}
