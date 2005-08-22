using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    public sealed class TypeFactory<ProductType> : ConcreteFactory<string,ProductType>
    {
        public void Register<ConcreteProductType>() where ConcreteProductType : ProductType,new()
        {
            base.Register<ConcreteProductType>(typeof(ConcreteProductType).FullName);
        }
    }
}
