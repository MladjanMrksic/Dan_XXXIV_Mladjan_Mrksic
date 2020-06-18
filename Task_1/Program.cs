using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task_1
{
    class Program
    {
        public static int Balance = 10000;
        static readonly object l = new object();
        static readonly Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("/t/t/tWelcome to BankSimulation");
            int firstLine;
            do
            {
                Console.WriteLine("Please enter the number of people in the first line");
            } while (int.TryParse(Console.ReadLine(),out firstLine) == false);
            int secondLine;
            do
            {
                Console.WriteLine("Please enter the number of people in the second line");
            } while (int.TryParse(Console.ReadLine(), out secondLine) == false);
            Thread t;
            List<Thread> threadList = new List<Thread>();
            for (int i = 1; i <= firstLine+secondLine; i++)
            {
                t = new Thread(DoSmth);
                t.Name = string.Format("Client " + i);
                threadList.Add(t);
            }

            for (int i = 0; i < threadList.Count; i++)
            {
                if (i%2==0)
                {
                    threadList[i].Start(1);
                }
                else
                {                    
                    threadList[i].Start(2);
                }
                
            }
            Console.ReadLine();
        }
        public static void DoSmth(object ATM)
        {
            lock (l)
            {
                if (Balance > 0)
                {
                    
                    int temp = rnd.Next(100, 10000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is trying to withdraw " + temp + " RSD from ATM " + (int)ATM);
                    if (temp < Balance)
                    {
                        Console.WriteLine("Money withdrawn successfully!");
                        Balance -= temp;
                    }
                    else
                    {                        
                        Console.WriteLine("Transaction terminated due to lack of balance");
                    }
                }
            }
            
            
        }
    }
}
