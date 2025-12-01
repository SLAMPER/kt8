using System;
using System.IO;

namespace kt8888
{
    public class BankAccount
    {
        private decimal balance;

        public decimal Balance
        {
            get { return balance; }
            private set
            {
                balance = value;
                BalanceChanged?.Invoke(balance);
            }
        }

        public event Action<decimal> BalanceChanged;

        public void Deposit(decimal amount)
        {
            Balance += amount;
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= Balance)
            {
                Balance -= amount;
            }
        }
    }

    public class Logger
    {
        public Logger(BankAccount account)
        {
            account.BalanceChanged += (balance) =>
            {
                File.AppendAllText("log.txt", $"balance {balance}\n");
            };
        }
    }

    class Program
    {
        static void Main()
        {
            BankAccount account1 = new BankAccount();
            Logger logger1 = new Logger(account1);

            account1.Deposit(1000);
            account1.Withdraw(500);
            account1.Deposit(300);

            Console.WriteLine("final balance " + account1.Balance);
            Console.ReadLine();
        }
    }
}