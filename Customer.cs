using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Customer
    {
        private static int numberOfCust;
        private readonly int customerID;
        private readonly int customerNumber;
        public string Name { get; private set; }
        public string PhNumber { get; private set; }
        public int CustomerID
        {  
            get { return customerID; }
        }
        public int CustomerNumber
        {
            get { return customerNumber; }
        }

        public Customer(int Id,string name, string phNumber)
        {
            customerID = Id;
            PhNumber = phNumber;
            Name = name;
            numberOfCust++;
            customerNumber = numberOfCust;
            
        }
        public override string ToString()
        {
            return $"ID:{CustomerID} Name:{Name} Phone Number:{PhNumber} CustomerNumber:{CustomerNumber}";
        }
        public override bool Equals(object obj)
        {
            var cust = obj as Customer;
            return this==cust;
        }
        public static bool operator == (Customer c1,Customer c2)
        {
            if (ReferenceEquals(c1, null) && ReferenceEquals(c2, null))
            {
                return true;
            }
            if (ReferenceEquals(c1, null) || ReferenceEquals(c2, null))
            {
                return false;
            }
            return c1.CustomerID == c2.CustomerID;
        }
        public static bool operator !=(Customer c1, Customer c2)
        {
            return !(c1 == c2);
        }
        public override int GetHashCode()
        {
            return this.CustomerID;
        }
    }
}
