using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using Features.Linq;

namespace Features
{
    class Program
    {
        static void Main(string[] args)
        {
            // Lambda with one parameter does not need to surround with parenthesis

            Func<int, int> square = x => x * x;

            // Lambda with two parameters does need parenthesis

            Func<int, int, int> add = (x, y) => x + y;

            // Can use body, but then need to explicity return 

            Func<int, int, int> add2 = (x, y) =>
            {
                int temp = x + y;
                return temp;
            };

            // Can declare types but compiler tries to infer and checks

            Func<int, int, int> add3 = (int x, int y) =>
            {
                int temp = x + y;
                return temp;
            };


            Action<string> writeS =  x => Console.WriteLine(x);
            Action<int> writeI = x => Console.WriteLine(x);

            writeI(square(add(3, 5)));
            writeS($"square(add(3, 5)) = {square(add(3, 5))}");

            var developers = new Employee[]
            {
                new Employee { Id = 1, Name= "Scott" },
                new Employee { Id = 2, Name= "Chris" }
            };

            // NOTE(crhodes)
            // If declare as IEnumerable<employee>
            // Can use same as below

            IEnumerable<Employee> developersIE = new Employee[]
{
                new Employee { Id = 1, Name= "Scott" },
                new Employee { Id = 2, Name= "Chris" }
};

            var sales = new List<Employee>()
            {
                new Employee { Id = 3, Name = "Alex" }
            };

            Console.WriteLine("foreach (var person in developers)");
            foreach (var person in developers)
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine("foreach (var person in developersIE)");
            foreach (var person in developers)
            {
                Console.WriteLine(person.Name);
            }

            Console.WriteLine("foreach (var person in sales)");
            foreach (var person in sales)
            {
                Console.WriteLine(person.Name);
            }

            // foreach the hard way.  foreach hides a log of IEnumerable details :)

            var enumerator = developers.GetEnumerator();

            // NOTE(crhodes)
            // The type of enumerator is System.Array.SZArrayEnumerator

            Console.WriteLine("var enumerator = developers.GetEnumerator();");

            while (enumerator.MoveNext())
            {
                Console.WriteLine(((Employee)enumerator.Current).Name);
            }

            IEnumerator<Employee> enumeratorIE = developersIE.GetEnumerator();

            Console.WriteLine("IEnumerator<Employee> enumerator2 = developersIE.GetEnumerator();");

            while (enumeratorIE.MoveNext())
            {
                Console.WriteLine(enumeratorIE.Current.Name);
            }

            IEnumerator<Employee> enumerator2 = sales.GetEnumerator(); ;

            Console.WriteLine("IEnumerator<Employee> enumerator2 = sales.GetEnumerator()");

            while (enumerator2.MoveNext())
            {
                Console.WriteLine(enumerator2.Current.Name);
            }

            // Method Syntax
            // NB.  do not need to add, but can
            // .Select( e => e)

            var query = developers.Where(e => e.Name.Length == 5)
                                  .OrderBy(e => e.Name);

            // Query Syntax

            var query2 = from developer in developers
                         where developer.Name.Length == 5
                         orderby developer.Name
                         select developer;

            Console.WriteLine("Method Syntax");

            foreach (var employee in query)
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("Query Syntax");

            foreach (var employee in query2)
            {
                Console.WriteLine(employee.Name);
            }

            // Use Named Method

            Console.WriteLine("foreach (var employee in developers.Where(NameStartsWithS))");

            foreach (var employee in developers.Where
                (NameStartsWithS)
            )
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("foreach (var employee in developers.Where(delegate ...))");

            foreach (var employee in developers.Where
                (delegate (Employee employee)
                    {
                        return employee.Name.StartsWith("S");
                    }
                )
            )
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("foreach (var employee in developers.Where(lambda ...))");

            foreach (var employee in developers.Where
                (e => e.Name.StartsWith("S"))
            )
            {
                Console.WriteLine(employee.Name);
            }

            Console.WriteLine("\nEnter to Exit");
            Console.ReadLine();
        }

        private static bool NameStartsWithS(Employee employee)
        {
            return employee.Name.StartsWith("S");
        }
    }
}
