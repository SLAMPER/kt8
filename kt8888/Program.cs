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