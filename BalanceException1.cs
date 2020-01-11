using System;
using System.Runtime.Serialization;

namespace Bank
{
    [Serializable]
    internal class BalanceException1 : Exception
    {
        public BalanceException1()
        {
        }

        public BalanceException1(string message) : base(message)
        {
        }

        public BalanceException1(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected BalanceException1(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}