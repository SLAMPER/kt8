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