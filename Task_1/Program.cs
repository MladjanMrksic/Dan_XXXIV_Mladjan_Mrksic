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
                t.Name = string.Format("Thread_" + i);
                threadList.Add(t);
            }
        }
        public static void DoSmth() { }
    }
}
