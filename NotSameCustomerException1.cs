using System;
using System.Runtime.Serialization;

namespace Bank
{
    [Serializable]
    internal class NotSameCustomerException1 : Exception
    {
        public NotSameCustomerException1()
        {
        }

        public NotSameCustomerException1(string message) : base(message)
        {
        }

        public NotSameCustomerException1(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSameCustomerException1(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}