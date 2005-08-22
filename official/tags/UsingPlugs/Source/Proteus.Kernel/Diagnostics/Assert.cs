using System;
using System.Collections.Generic;
using System.Text;

namespace Proteus.Kernel.Diagnostics
{
    public sealed class Assert<TargetType>
    {
        private StringBuilder   builder = new StringBuilder();
        private Type            type    = typeof(TargetType);

        private string GetMessage(string message, params object[] plist)
        {
            builder.Remove(0, builder.Length);
            builder.AppendFormat(message, plist);
            return builder.ToString();
        }

        public void IsTrue(bool condition)
        {
            this.IsTrue(condition, string.Empty);
        }
    
        public void IsTrue(bool condition, string message, params object[] plist)
        {
            if ( !condition )
            {
                Manager.Instance.Assert("Condition is was not true.", GetMessage(message, plist));
            }
        }

        public void IsFalse(bool condition)
        {
            this.IsFalse(condition, string.Empty);
        }

        public void IsFalse(bool condition, string message, params object[] plist)
        {
            if (condition)
            {
                Manager.Instance.Assert("Condition is was not false.", GetMessage(message, plist));
            }
        }

        public void IsEmpty<T>(ICollection<T> collection, string message, params object[] plist)
        {
        }

        public void IsNotEmpty<T>(ICollection<T> collection, string message, params object[] plist)
        {
        }

        public void IsNull(object obj,string message,params object[] plist )
        {
        }

        public void IsNotNull(object obj,params object[] plist )
        {
        }

        public void AreSame(object a, object b)
        {
            this.AreSame(a, b, string.Empty);
        }

        public void AreSame(object a, object b,string message,params object[] plist )
        {
            if (!Object.ReferenceEquals(a, b))
            {
                string condition = GetMessage("Object references are not the same: [{0} != {1}]", a, b);

                Manager.Instance.Assert(condition, GetMessage(message, plist));
            }
        }

        public void AreEqual(object a, object b,string message,params object[] plist )
        {  
        }
    }
}
