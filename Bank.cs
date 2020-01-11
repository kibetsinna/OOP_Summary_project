using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Bank
{
    internal class Bank : IBank
    {
        private string name;
        private string address;
        private int customerCount;
        private Dictionary<int, Customer> mapCustID;
        private Dictionary<int, Customer> mapCustNum;
        private Dictionary<int, Account> mapAccNum;
        private Dictionary<Customer, List<Account>> mapCustListAcc;
        private int totalMoneyInBank;
        private int profits;
        private List<Account> accounts;
        private List<Customer> customers;
        public string Name
        {
            get { return name; }
        }

        public string Address
        {
            get { return address; }
        }
        public int CustomerCount
        {
            get { return customerCount; }
        }
        public int TotalMoneyInBank
        {
            get { return totalMoneyInBank; }
        }
        public int Profits
        {
            get { return profits; }
        }
        internal Bank(string nameBank, string addressBank, int startTotalMoney)
        {
            name = nameBank;
            address = addressBank;
            customerCount = 0;
            totalMoneyInBank = startTotalMoney;
            profits = 0; ;
            accounts = new List<Account>();
            customers = new List<Customer>();
            mapCustID = new Dictionary<int, Customer>();
            mapCustNum = new Dictionary<int, Customer>();
            mapAccNum = new Dictionary<int, Account>();
            mapCustListAcc = new Dictionary<Customer, List<Account>>();
        }

        internal Customer GetCustomerByID(int customerID)
        {
            if (mapCustID.TryGetValue(customerID, out Customer c))
            {
                return c;
            }
            else
            {
                throw new CustomerNotFoundException($"Customer ID {customerID} was not found");
            }

        }
        internal Customer GetCustomerByNumber(int customerNum)
        {
            if (mapCustNum.TryGetValue(customerNum, out Customer c))
            {
                return c;
            }
            else
            {
                throw new CustomerNotFoundException($"Customer Number {customerNum} was not found");
            }
        }
        internal Account GetAccountByNumber(int accountNum)
        {
            if (mapAccNum.TryGetValue(accountNum, out Account a))
            {
                return a;
            }
            else
            {
                throw new AccountNotFoundException($"Account Number {accountNum} was not found");
            }

        }
        internal List<Account> GetAccountsByCustomer(Customer cust)
        {
            GetCustomerByID(cust.CustomerID);
            List<Account> acc = new List<Account>();
            if (mapCustListAcc.TryGetValue(cust, out acc))
            {
                return acc;

            }
            else
            {
                throw new AccountNotFoundException($"Customer  {cust.CustomerID} account numbers  were not found");
            }
        }
        internal void AddNewCustomer(Customer newCust)
        {
            if (mapCustID.TryGetValue(newCust.CustomerID, out Customer c) == false)
            {
                customers.Add(newCust);
                mapCustID[newCust.CustomerID] = newCust;
                mapCustNum[newCust.CustomerNumber] = newCust;
                mapCustListAcc[newCust] = new List<Account>();
                customerCount++;
            }
            else
            {
                throw new CustomerAlreadyExistException($"Customer ID {newCust.CustomerID} already exist");
            }

        }
        //internal void OpenNewAccount(Account newAcc, Customer cust)//redundant variable
        internal void OpenNewAccount(Account newAcc)
        {
            if (mapAccNum.TryGetValue(newAcc.AccountNumber, out Account a) == false)
            {
                accounts.Add(newAcc);
                mapAccNum[newAcc.AccountNumber] = newAcc;
                mapCustListAcc[newAcc.AccountOwner].Add(newAcc);
            }
            else
            {
                throw new AccountAlreadyExistException($"Account Number ID {newAcc.AccountNumber} already exist");
            }
        }
        internal int Deposit(Account account, int amount)
        {
            try
            {
                Account a = GetAccountByNumber(account.AccountNumber);
                a.Add(amount);
                totalMoneyInBank += amount;
                return a.Balance;
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine("Exception in Func.Deposit" + ex.Message);
                return 0;
            }
        }
        internal int Withdraw(Account account, int amount)
        {

            try
            {
                Account a = GetAccountByNumber(account.AccountNumber);
                try
                {
                    a.Subtract(amount);
                    totalMoneyInBank -= amount;
                }
                catch (BalanceException1 ex)
                {
                    Console.WriteLine("Exception in Func.Withdraw" + ex.Message);
                }
                return a.Balance;
            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine("Exception in Func.Withdraw" + ex.Message);
                return 0;
            }
        }
        internal int GetCustomerTotalBalance(Customer c)
        {
            int result = 0;
            try
            {

                if (mapCustID.TryGetValue(c.CustomerID, out Customer cust) == true)
                {
                    List<Account> acc = mapCustListAcc[cust];
                    if (acc.Count > 0)
                    {
                        foreach (Account a in acc)
                        {
                            result += a.Balance;
                        }

                    }

                }
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine("Exception in Func.GetCustomerTotalBalance" + ex.Message);
            }
            return result;
        }
        internal void CloseAccount(Account account)
        {
            try
            {
                Account a = GetAccountByNumber(account.AccountNumber);
                Customer c = a.AccountOwner;
                mapAccNum.Remove(account.AccountNumber);
                List<Account> acc = mapCustListAcc[c];
                acc.Remove(account);
                accounts.Remove(account);

            }
            catch (AccountNotFoundException ex)
            {
                Console.WriteLine("Exception in Func.CloseAccount" + ex.Message);

            }


        }
        internal void ChargeAnnualCommission(float percentage)
        {
            foreach (Account a in accounts)
            {
                int pr = (int)(a.Balance * percentage / 100);
                if (a.Balance < 0)
                    pr = pr * 2;

                Withdraw(a, pr);
                profits += pr;


            }

        }
        internal Account JoinAccounts(Account a1, Account a2)
        {
            try
            {
                if (a1 == null || a2 == null)
                    return null;
                Account newAcc = a1 + a2;
                CloseAccount(a1);
                CloseAccount(a2);
                OpenNewAccount(newAcc);
                return newAcc;
            }
            catch (NotSameCustomerException1 ex)
            {
                Console.WriteLine("Exception in Func:JoinAccount" + ex.Message);
                return null;
            }
        }
        internal void PrintAllAccounts()
        {
            Console.WriteLine("*************************PrintAllAccounts*************************************");
            foreach (Account a in accounts)
            {
                Console.WriteLine(a);
            }
        }
        internal BackUpBank BackUp()
        {
            BackUpBank myCopy = new BackUpBank();

            foreach (Customer c in customers)
            {
                BackUpCustomer bc = new BackUpCustomer(c.CustomerID, c.CustomerNumber, c.Name, c.PhNumber);
                myCopy.customers.Add(bc);
            }
            foreach (Account a in accounts)
            {
                BackUpCustomer bc = new BackUpCustomer(a.AccountOwner.CustomerID, a.AccountOwner.CustomerNumber, a.AccountOwner.Name, a.AccountOwner.PhNumber);
                BackUpAccount ba = new BackUpAccount(a.AccountNumber, bc, a.MaxMinusAllowed, a.Balance);
                myCopy.accounts.Add(ba);
            }
            myCopy.PrintAllAccounts();
            return myCopy;

        }
        private static bool RemoveBackUpAccount(BackUpAccount a)
        {

            return (a != null);
        }
        private static bool RemoveBackUpCustomer(BackUpCustomer c)
        {

            return (c != null);
        }
        internal void SaveToXML(string pathFile)
        {
            BackUpBank myCopy = BackUp();
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(BackUpBank));

            using (Stream file = new FileStream(pathFile, FileMode.Create))
            {
                myXmlSerializer.Serialize(file, myCopy);

            }

            myCopy.accounts.RemoveAll(RemoveBackUpAccount);

            myCopy.customers.RemoveAll(RemoveBackUpCustomer);
            myCopy.PrintAllAccounts();


        }
        internal BackUpBank LoadFromXMLToBackUp(string pathFile)
        {
            BackUpBank backUp = null;
            XmlSerializer myXmlSerializer = new XmlSerializer(typeof(BackUpBank));
            using (Stream file = new FileStream(pathFile, FileMode.Open))
            {
               backUp = myXmlSerializer.Deserialize(file) as BackUpBank;

            }
            return backUp;
        }
    }
}

