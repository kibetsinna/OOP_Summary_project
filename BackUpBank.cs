using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    public class BackUpBank
    {
       
        public List<BackUpAccount> accounts;
        public List<BackUpCustomer> customers;
        public BackUpBank ()
        {
            accounts = new List<BackUpAccount>();
            customers = new List<BackUpCustomer>();

        }
        internal void PrintAllAccounts()
        {
            Console.WriteLine("*************************MYCopyPrintAllAccounts*************************************");
            int i = 0;
            foreach (BackUpAccount a in accounts)
            {
                Console.WriteLine(a);
                i++;
            }
            if(i==0)
            {
                Console.WriteLine("All Temporary Accounts were deleted");
            }
        }
    }
}
