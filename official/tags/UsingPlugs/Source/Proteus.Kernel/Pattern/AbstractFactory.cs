using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    /// <summary>
    /// Loosly typed creator pattern. Used by the 
    /// factory family of classes. This has to be loosly
    /// typed since NET 2.0 does not allow generic generics 
    /// parameters.
    /// </summary>
    public interface IAbstractCreator
    {
        /// <summary>
        /// Create an instance of the type associated.
        /// </summary>
        /// <returns>The new instance or null on error.</returns>
        object Create();
    }

    public class AbstractFactory<IdType,ProductType> 
    {      
        public delegate void RegistrationDelegate( IdType id );

        public event RegistrationDelegate Registered;
        public event RegistrationDelegate Unregistered;
        
        protected    SortedList<IdType, IAbstractCreator> factoryCreators =
                new  SortedList<IdType, IAbstractCreator>();

        public IAbstractCreator this[IdType id]
        {
            get
            {
                if (factoryCreators.ContainsKey(id))
                {
                    return factoryCreators[id];
                }

                return null;
            }
            set
            {
                this.Register(id, value);
            }
        }

        public IEnumerator<IdType> GetEnumerator()
        {
            foreach (IdType id in factoryCreators.Keys)
                yield return id;
        }

        public void Register(IdType id,IAbstractCreator creator)
        {
            if (creator != null)
            {
                factoryCreators[id] = creator;
                if ( Registered != null )
                    Registered( id );
            }
        }

        public void Unregister(IdType id)
        {
            factoryCreators.Remove(id);
            if ( Unregistered != null )
                Unregistered(id);
        }

        public void Unregister(IAbstractCreator creator)
        {
            int index = factoryCreators.IndexOfValue(creator);
            if (index != -1)
            {
                factoryCreators.RemoveAt(index);
                if (Unregistered != null)
                {
                    IdType id = factoryCreators.Keys[index];

                    Unregistered(id);
                }
            }
        }

        public ProductType Create(IdType id)
        {
            if (factoryCreators.ContainsKey(id))
            {
                IAbstractCreator creator = factoryCreators[id];             
                return (ProductType)creator.Create();
            }

            return default(ProductType);
        }
    }
}
