using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LanguageFeatures.Models
{
    public static class MyExtensionMethods
    {
        public static decimal TotalPrices(this IEnumerable<Product> productEnumerable)
        {
            decimal total = 0;

            foreach (Product product in productEnumerable)
            {
                total += product.Price;
            }

            return total;
        }

        public static IEnumerable<Product> FilterByCategory(
            this IEnumerable<Product> productEnum, string categoryParam)
        {
            foreach (Product prod in productEnum)
            {
                if (prod.Category == categoryParam)
                {
                    yield return prod;
                }
            }
        }

        public static IEnumerable<Product> Filter(
        this IEnumerable<Product> productEnum, Func<Product, bool> selectorFunc)
        {
            foreach (Product prod in productEnum)
            {
                if (selectorFunc(prod))
                {
                    yield return prod;
                }
            }
        }

        //public static decimal TotalPrices(this ShoppingCart cartParam)
        //{
        //    decimal total = 0;

        //    foreach (Product product in cartParam.Products)
        //    {
        //        total += product.Price;
        //    }

        //    return total;
        //}
    }
}