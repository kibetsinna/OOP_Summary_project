using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer one = new Customer(1, "inna", "054-32");
            Customer two = new Customer(2, "yana", "054-32");
           
            Console.WriteLine(one);
            Console.WriteLine(two);
            Account first = new Account(one, 3000);
            Account second = new Account(one,4000);
            Account third = new Account(two, 3000);
            Account newOne = new Account(one, 200);
            Bank myBank = new Bank("The best bank in this street", "Lod", 10000000);
            myBank.AddNewCustomer(one);
            myBank.AddNewCustomer(two);
            
            try
            {
                myBank.AddNewCustomer(two);//error
            }
            catch(CustomerAlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            myBank.OpenNewAccount(first);
            myBank.OpenNewAccount(second);
            myBank.OpenNewAccount(third);
            myBank.OpenNewAccount(newOne);
            try
            {
                myBank.OpenNewAccount(third);//error
            }
            catch (AccountAlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            // third.Add(5000);
            //  int sum1 = first + 1000;
            //   int sum2 = second + 2000;
            myBank.Deposit(first, 1000);
            myBank.Deposit(second, 2000);
            myBank.Deposit(third, 3000);
            myBank.Withdraw(first, 500);
            myBank.Withdraw(first, 50000);
            Console.WriteLine(first);
            Console.WriteLine(second);
            Console.WriteLine(third);
            Console.WriteLine("One. Customer's balance:"+myBank.GetCustomerTotalBalance(one));
            Console.WriteLine("Two. Customer's balance:" + myBank.GetCustomerTotalBalance(two));
            myBank.ChargeAnnualCommission(10);
            Console.WriteLine("One. Customer's balance after commission:" + myBank.GetCustomerTotalBalance(one));
            Console.WriteLine("Two. Customer's balance after commission:" + myBank.GetCustomerTotalBalance(two));
            Account newAcc;
            newAcc = myBank.JoinAccounts(first,second);
            Console.WriteLine(newAcc);
            Console.WriteLine("GetAccountByNumber(4)");
            Console.WriteLine("GetAccountByNumber(4)" + myBank.GetAccountByNumber(4));
            try//closed account
            {
                Console.WriteLine("GetAccountByNumber(1)" );
                Console.WriteLine("GetAccountByNumber(1)" + myBank.GetAccountByNumber(1));
            }
            catch(AccountNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.WriteLine("Total: " + myBank.TotalMoneyInBank);
            Console.WriteLine("Profits: "+myBank.Profits);
            myBank.PrintAllAccounts();
            myBank.SaveToXML(@"c:\temp\BackUpSave.xml");
            BackUpBank newBackUp = myBank.LoadFromXMLToBackUp(@"c:\temp\BackUpLoad.xml");
            newBackUp.PrintAllAccounts();
            Console.ReadLine();



        }
    }
}
