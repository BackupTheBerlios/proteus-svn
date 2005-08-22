using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public class ConcreteFactory<IdType,ProductType> : AbstractFactory<IdType,ProductType> 
    {
        internal interface IConcreteCreator
        {
            Type Type { get; }
        }

        internal sealed class ConcreteCreator<ConcreteProductType> 
            : IAbstractCreator,IConcreteCreator where ConcreteProductType : ProductType,new()
        {            
            private Type createType = null;

            public Type Type
            {
                get { return createType; }
            }
            
            public object Create() 
            {
                return new ConcreteProductType();
            }

            public sealed override bool Equals(object obj)
            {
                return (obj is ConcreteCreator<ConcreteProductType>) ;
            }

            public sealed override int GetHashCode()
            {
                return createType.GetHashCode();
            }

            public ConcreteCreator() 
            {
                createType = typeof(ConcreteProductType);
            }
        }

        public Type GetType(IdType id)
        {
            IConcreteCreator creator = this[id] as IConcreteCreator;
            if (creator != null)
            {
                return creator.Type;
            }
            return null;
        }

        public void Register<ConcreteProductType>(IdType id) where ConcreteProductType : ProductType,new()
        {
            base.Register(id, new ConcreteCreator<ConcreteProductType>());
        }

        public void Unregister<ConcreteProductType>() where ConcreteProductType : ProductType,new()
        {
            base.Unregister(new ConcreteCreator<ConcreteProductType>());
        }
    }
}
