using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    internal class Account
    {
        private static int numberOfAcc;
        private readonly int accountNumber;
        private readonly Customer accountOwner;
        private int maxMinusAllowed;

       
        public int AccountNumber 
        {
            get { return accountNumber; }
        }
        public int Balance { get; private set; }
        public Customer AccountOwner
        {
            get { return accountOwner; }
        }
        public int MaxMinusAllowed
        {
            get { return maxMinusAllowed; }
        }


        public Account(Customer customer, int monthlyIncome)
        {
            accountOwner = customer;
            maxMinusAllowed = 3*monthlyIncome;
            numberOfAcc++;
            accountNumber = numberOfAcc;
        }
        public void Add(int Amount)
        {
            Balance += Amount;
        }
        public void Subtract(int Amount)
        {
            if (Balance - Amount >=(-1)* MaxMinusAllowed)
            { 
                 Balance -= Amount;
            }
            else
            {
                string buf= "Balance Exception: The balance cannot be less than the allowed amount.";
                throw new BalanceException1($"{buf} AccountNumber:{ AccountNumber } Balance: { Balance} maxMinusAllowed: { maxMinusAllowed} ");
            }
        }

        public override string ToString()
        {

                return $"AccountNumber:{AccountNumber} Customer:{accountOwner.ToString()} Balance:{Balance} maxMinusAllowed:{maxMinusAllowed}";
       
        }
        public override bool Equals(object obj)
        {
            var acc = obj as Account;
            return this == acc;
        }
        public static bool operator == (Account a1, Account a2)
        {
            if (ReferenceEquals(a1, null) && ReferenceEquals(a2, null))
            {
                return true;
            }
            if (ReferenceEquals(a1, null) || ReferenceEquals(a2, null))
            {
                return false;
            }
            return a1.AccountNumber == a2.AccountNumber;
        }
        
        public static bool operator !=(Account a1, Account a2)
        {
            return !(a1 == a2);
        }
        public static Account operator + (Account a1,Account a2)
        {
            if (a1.accountOwner == a2.AccountOwner)
            {
                int newMax = a1.MaxMinusAllowed / 3 + a2.MaxMinusAllowed / 3;
                Account newAcc = new Account(a1.AccountOwner, newMax);
                newAcc.Balance = a1.Balance + a2.Balance;
                return newAcc;
            }
            else
            {
                string str = "NotSameCustomerException : Accounts must belong to the same customer. ";
                throw new NotSameCustomerException1($"{str} Customer 1:{a1.AccountOwner.Name} Customer 2:{a2.AccountOwner.Name}");

            }
         }
        public static int operator +(Account a1, int amount )
        {
            a1.Add(amount);
            return a1.Balance;


        }
        public static int operator -(Account a1, int amount)
        {
            a1.Subtract(amount);
            return a1.Balance;


        }
        public override int GetHashCode()
        {
            return this.AccountNumber;
        }
    }
}
