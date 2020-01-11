using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BackUpAccount
    {
      
        public int accountNumber;
        public BackUpCustomer accountOwner;
        public int maxMinusAllowed;
        public int Balance;
        public BackUpAccount()
        {

        }

        public BackUpAccount(int accountNumber, BackUpCustomer accountOwner, int maxMinusAllowed, int balance)
        {
        
            this.accountNumber = accountNumber;
            this.accountOwner = accountOwner;
            this.maxMinusAllowed = maxMinusAllowed;
            Balance = balance;
        }

        public override string ToString()
        {

            return $"AccountNumber:{accountNumber} Customer:{accountOwner.ToString()} Balance:{Balance} maxMinusAllowed:{maxMinusAllowed}";

        }
    }
}
