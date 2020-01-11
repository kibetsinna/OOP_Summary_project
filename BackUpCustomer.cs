using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BackUpCustomer
    {
        public int customerID;
        public int customerNumber;
        public string Name;
        public string PhNumber;
        public BackUpCustomer()
        {

        }

        public BackUpCustomer(int customerID, int customerNumber,string name,string phone)
        {
            this.customerID = customerID;
            this.customerNumber = customerNumber;
            this.Name = name;
            this.PhNumber = phone;
        }
        public override string ToString()
        {
            return $"ID:{customerID} Name:{Name} Phone Number:{PhNumber} CustomerNumber:{customerNumber}";
        }
    }
}
