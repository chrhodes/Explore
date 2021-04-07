using System;
using System.Collections.Generic;

namespace Queries
{
    public static class MyLinq
    {          
        
        public static IEnumerable<double> Random()
        {
            var random = new Random();
            while (true)
            {
                yield return random.NextDouble();
            }
        }
                    
        public static IEnumerable<T> MyFilter<T>(this IEnumerable<T> source,
                                               Func<T, bool> predicate)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;

            // LINQ does it this way

            //foreach (var item in source)
            //{
            //    if (predicate(item))
            //    {
            //        yield return item;
            //    }
            //}
        }

        public static IEnumerable<T> MyFilterYield<T>(this IEnumerable<T> source,
                                               Func<T, bool> predicate)
        {
            //LINQ does it this way

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }
    }
}
