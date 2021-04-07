using System.Linq;
using System.Collections.Generic;
using System;

namespace Queries
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Use LINQ to produce Random Numbers - Streaming Operator");

            var numbers = MyLinq.Random().Where(n => n > 0.5).Take(10).OrderBy(n => n);

            foreach (var number in numbers)
            {
                Console.WriteLine(number);
            }

            var movies = new List<Movie>
            {
                new Movie { Title = "The Dark Knight",   Rating = 8.9f, Year = 2008 },
                new Movie { Title = "The King's Speech", Rating = 8.0f, Year = 2010 },
                new Movie { Title = "Casablanca",        Rating = 8.5f, Year = 1942 },
                new Movie { Title = "Star Wars V",       Rating = 8.7f, Year = 1980 }                        
            };

            Console.WriteLine("\nLINQ Where\n");

            var query1 = movies.Where(m => m.Year > 2000);

            foreach (var movie in query1)
            {
                Console.WriteLine(movie.Title);
            }

            Console.WriteLine("\nLINQ Where OrderBy\n");

            var query1ob = movies.Where(m => m.Year > 2000)
                .OrderByDescending(m => m.Title);

            foreach (var movie in query1ob)
            {
                Console.WriteLine(movie.Title);
            }

            Console.WriteLine("\nMyFilter\n");

            var query2 = movies.MyFilter(m => m.Year > 2000);

            foreach (var movie in query2)
            {
                Console.WriteLine(movie.Title);
            }

            Console.WriteLine("\nMyFilter OrderBy\n");

            var query2ob = movies.MyFilter(m => m.Year > 2000)
                .OrderByDescending(m => m.Title);

            foreach (var movie in query2ob)
            {
                Console.WriteLine(movie.Title);
            }

            Console.WriteLine("\nMyFilterYield\n");

            var query3 = movies.MyFilterYield(m => m.Year > 2000);

            foreach (var movie in query3)
            {
                Console.WriteLine(movie.Title);
            }

            Console.WriteLine("\nMyFilterYield OrderBy\n");

            var query3ob = movies.MyFilterYield(m => m.Year > 2000)
                .OrderByDescending(m => m.Title);

            foreach (var movie in query3ob)
            {
                Console.WriteLine(movie.Title);
            }

            // NOTE(crhodes)
            // Step through this to see when filter is and is not getting called

            var queryDbg = movies.MyFilter(m => m.Year > 2000);

            var enumerator = queryDbg.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current.Title);
            }

            Console.WriteLine("\nQuery Syntax\n");

            var query = from movie in movies
                        where movie.Year > 2000
                        orderby movie.Rating descending
                        select movie;

            var enumerator2 = query.GetEnumerator();

            while (enumerator2.MoveNext())
            {
                Console.WriteLine(enumerator2.Current.Title);
            }

            Console.WriteLine("\nEnter to Exit");
            Console.ReadLine();
        }
    }
}
