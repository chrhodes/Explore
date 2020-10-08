using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunWithDelegatesEventsLambdas
{
    class Program
    {
        public delegate int BinaryOp(int x, int y);

        static void Main(string[] args)
        {
            //SimpleDelegateExample();

            CarAccelerationExample();
        }

        private static void SimpleDelegateExample()
        {
            Console.WriteLine("** Simple Delegate Example ");

            BinaryOp b = new BinaryOp(SimpleMath.Add);

            Console.WriteLine("10 + 10 is {0}", b(10, 10));

            Console.ReadLine();
        }

        private static void CarAccelerationExample()
        {
            Console.WriteLine("*** Delegates as event enablers ***");
            Car c1 = new Car("ZoomZoom", 120, 10);

            c1.RegisterWithCarEngine(new Car.CarEngineHandler(OnCarEngineEvent));

            for (int i = 0; i < 7; i++)
            {
                Console.WriteLine("*** Accelerating ***");
                c1.Accelerate(20);
                Console.ReadLine();
            }

            Console.ReadLine();
        }

        private static void OnCarEngineEvent(string message)
        {
            Console.WriteLine("\n*** Message from Car ***");
            Console.WriteLine(" => {0}", message);
            Console.WriteLine("**********************");
        }
    }
}
