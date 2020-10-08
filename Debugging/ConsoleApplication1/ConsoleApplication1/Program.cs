using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            int j = 2;
            Greeting();
            string message = Inquiry();
            Console.WriteLine(string.Format("I am interested in {0}, too.", message));
            SayGoodBye();
        }

        private static void Greeting()
        {
            Console.WriteLine("Hello Mr. Phelps");
        }

        private static string Inquiry()
        {
            Console.WriteLine("What interests you?");
            string message = Console.ReadLine();
            return message;
        }
        private static void SayGoodBye()
        {
            Console.WriteLine("Good Bye\n");
            Console.WriteLine("(return to exit)");
            Console.ReadLine();
        }
    }
}
