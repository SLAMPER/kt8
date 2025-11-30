/*
using System;

namespace kt8888
{
    public class Timer
    {
        private System.Timers.Timer _timer;

        public event EventHandler Tick;

        public Timer()
        {
            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += (s, e) => Tick?.Invoke(this, EventArgs.Empty);
        }

        public void Start() => _timer.Start();
        public void Stop() => _timer.Stop();
    }

    public class Clock
    {
        public Clock(Timer timer)
        {
            timer.Tick += (s, e) => Console.WriteLine($"time: {DateTime.Now:T}");
        }
    }

    public class Counter
    {
        private int _count = 0;

        public Counter(Timer timer)
        {
            timer.Tick += (s, e) => Console.WriteLine($"count: {++_count}");
        }
    }

    class Program
    {
        static void Main()
        {
            Timer timer1 = new Timer();
            Clock clock1 = new Clock(timer1);
            Counter counter1 = new Counter(timer1);

            timer1.Start();
            Console.WriteLine("timer started press key for stop");
            Console.ReadLine();
            timer1.Stop();
            Console.WriteLine("timer stopped");
        }
    }
}

*/

/*

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

*/

using System;
using System.Collections.Generic;

namespace kt8888
{
    public class Button
    {
        private string text;
        private EventHandler clickEvent;
        private List<EventHandler> subscribers = new List<EventHandler>();

        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        public event EventHandler Click
        {
            add
            {
                if (subscribers.Count >= 3)
                {
                    Console.WriteLine("cannot add more than 3 subscribers");
                    return;
                }

                if (subscribers.Contains(value))
                {
                    Console.WriteLine("this subscriber is already added");
                    return;
                }

                subscribers.Add(value);
                clickEvent += value;
                Console.WriteLine("subscriber added successfully");
            }
            remove
            {
                subscribers.Remove(value);
                clickEvent -= value;
                Console.WriteLine("subscriber removed successfully");
            }
        }

        public void SimulateClick()
        {
            Console.WriteLine($"\nbutton '{Text}' clicked");
            clickEvent?.Invoke(this, EventArgs.Empty);
        }
    }

    class Program
    {
        static void Main()
        {
            Button button1 = new Button();
            button1.Text = "button";

            void PrintText(object sender, EventArgs e)
            {
                Console.WriteLine("button text " + ((Button)sender).Text);
            }

            void ChangeColor(object sender, EventArgs e)
            {
                Console.WriteLine("changing button color to red");
            }

            void ShowMessage(object sender, EventArgs e)
            {
                Console.WriteLine("button was clicked");
            }

            void FourthSubscriber(object sender, EventArgs e)
            {
                Console.WriteLine("this is the fourth subscriber not work");
            }

            // подписка методы
            Console.WriteLine();
            button1.Click += PrintText;
            button1.Click += ChangeColor;
            button1.Click += ShowMessage;

            // четвертый подписчик
            Console.WriteLine();
            button1.Click += FourthSubscriber;

            // существующий подписчик
            Console.WriteLine();
            button1.Click += PrintText;

            // нажатие кнопки
            Console.WriteLine();
            button1.SimulateClick();

            // удаление и добавление подписчикв
            Console.WriteLine();
            button1.Click -= ShowMessage;
            button1.Click += FourthSubscriber;

            //нажатие кнопки
            Console.WriteLine();
            button1.SimulateClick();

        }
    }
}