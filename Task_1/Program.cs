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
        //Static variable holding total balance value
        public static int Balance = 10000;
        //Creating Lock for ATM methods
        static readonly object l1 = new object();
        //Static readonly Random is used to avoid same values in a row
        static readonly Random rnd = new Random();
        static void Main(string[] args)
        {            
            //Greeting message followed by user input for queue lenghts
            Console.WriteLine("\t\t\tWelcome to BankSimulation");
            int firstQueue;
            do
            {
                Console.WriteLine("Please enter the number of people in the first line");
            } while (int.TryParse(Console.ReadLine(),out firstQueue) == false);
            int secondQueue;
            do
            {
                Console.WriteLine("Please enter the number of people in the second line");
            } while (int.TryParse(Console.ReadLine(), out secondQueue) == false);            
            Thread t;
            //Creating two lists to hold threads for two different queues
            List<Thread> threadList1 = new List<Thread>();
            List<Thread> threadList2 = new List<Thread>();
            //Creating first queue of people using ATM1
            for (int i = 1; i <= firstQueue; i++)
            {
                t = new Thread(ATM1)
                {
                    Name = string.Format("Client " + i)
                };
                threadList1.Add(t);
            }
            //Creating second queue of people using ATM2
            for (int i = 1; i <= secondQueue; i++)
            {
                t = new Thread(ATM2)
                {
                    Name = string.Format("Client " + i)
                };
                threadList2.Add(t);
            }
            //Going through both lists and starting the threads almost instantly
            for (int i = 0; i < threadList1.Count + threadList2.Count; i++)
            {
                if (i<threadList1.Count)
                    threadList1[i].Start();
                if (i < threadList2.Count)
                    threadList2[i].Start();
            }
            Thread.Sleep(1);
            Console.WriteLine("All clients have been server. Remaining balance is " + Balance + " RSD.");
            Console.WriteLine("\t\t\tThank you for using BankSimulation!");
            Console.ReadLine();
        }
        public static void ATM1()
        {
            //Puting a lock so only one thread can access complex logic in this method
            lock (l1)
            {
                //If Balance is above zero, the method continiues
                if (Balance > 0)
                {
                    //Random number is generated and checked against the balance. If its posible, a whithdrawal is made, if not the client is notified
                    int temp = rnd.Next(100, 10000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is trying to withdraw " + temp + " RSD from ATM 1");
                    if (temp < Balance)
                    {
                        Balance -= temp;
                        Console.WriteLine("Money withdrawn successfully! Remaining bank balance is " + Balance);                        
                    }
                    else          
                        Console.WriteLine("Transaction terminated due to lack of balance. Current bank balance is " + Balance);
                }
                else
                    Console.WriteLine("Bank is currently out of money.");
            }   
        }
        public static void ATM2()
        {
            //Puting a lock so only one thread can access complex logic in this method
            lock (l1)
            {
                //If Balance is above zero, the method continiues
                if (Balance > 0)
                {
                    //Random number is generated and checked against the balance. If its posible, a whithdrawal is made, if not the client is notified
                    int temp = rnd.Next(100, 10000);
                    Console.WriteLine(Thread.CurrentThread.Name + " is trying to withdraw " + temp + " RSD from ATM 2");
                    if (temp < Balance)
                    {
                        Balance -= temp;
                        Console.WriteLine("Money withdrawn successfully! Remaining bank balance is " + Balance);                        
                    }
                    else
                        Console.WriteLine("Transaction terminated due to lack of balance. Current bank balance is " + Balance);
                }
                else
                    Console.WriteLine("Bank is currently out of money.");
            }
        }
    }
}
