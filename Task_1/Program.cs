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
        static readonly object l1 = new object();
        static readonly object l2 = new object();
        static readonly Random rnd = new Random();
        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tWelcome to BankSimulation");
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
            List<Thread> threadList1 = new List<Thread>();
            List<Thread> threadList2 = new List<Thread>();

            for (int i = 1; i <= firstLine; i++)
            {
                t = new Thread(ATM1);
                t.Name = string.Format("Client " + i);
                threadList1.Add(t);
            }
            for (int i = 1; i <= secondLine; i++)
            {
                t = new Thread(ATM2);
                t.Name = string.Format("Client " + i);
                threadList2.Add(t);
            }

            for (int i = 0; i < threadList1.Count+threadList2.Count; i++)
            {
                try
                {
                    threadList1[i].Start();
                    threadList1[i].Join();
                }
                catch { }
                try
                {
                    threadList2[i].Start();
                    threadList2[i].Join();
                }
                catch { }
            }
            Console.ReadLine();
        }
        public static void ATM1()
        {
            lock (l1)
            {
                if (Balance > 0)
                {
                    
                    int temp = rnd.Next(100, 10000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is trying to withdraw " + temp + " RSD from ATM 1");
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
        public static void ATM2()
        {
            lock (l2)
            {
                if (Balance > 0)
                {

                    int temp = rnd.Next(100, 10000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is trying to withdraw " + temp + " RSD from ATM 2");
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
