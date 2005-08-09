using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Pattern
{
    /// <summary>
    /// Phoenix singleton class. Derives from this with
    /// your new type as parameter to implement automatic
    /// singleton behaviour.
    /// </summary>
    /// <typeparam name="SingletonType">The derived type.</typeparam>
    public class Singleton<SingletonType> 
        where SingletonType : Singleton<SingletonType>,new()
    {
        private static SingletonType singletonInstance = null;

        /// <summary>
        /// Access to the one and only singleton instance which
        /// is created on demand only.
        /// </summary>
        public static SingletonType Instance
        {
            get
            {
                if (singletonInstance == null)
                {
                    singletonInstance = new SingletonType();
                }

                return singletonInstance;
            }
        }

        /// <summary>
        /// Manually deletes the singleton instance, further
        /// access to the Instance member will recreate it though.
        /// </summary>
        public static void Release()
        {
            if (singletonInstance != null)
            {
                // Check for a disposable implementation.
                IDisposable disposable = singletonInstance as IDisposable;

                if (disposable != null)
                {
                    disposable.Dispose();
                }

                singletonInstance = null;
            }
        }
    }
}
