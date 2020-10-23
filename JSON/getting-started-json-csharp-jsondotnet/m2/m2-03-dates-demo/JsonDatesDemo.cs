using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace m2_05_dates_demo
{
    public static class JsonDatesDemo
    {
        /// <summary>
        /// In this demo I will show you how to deal with date formats
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Dates demo ***");

            List<DateTime> dates = new List<DateTime>() {
                new DateTime(2009, 07, 11, 23, 00, 00),
                new DateTime(2020, 01, 15),
                new DateTime(637143211000000000)                
            };

            Console.WriteLine("- Serialize object without specifying any date format (default)");
            string dateDefault = JsonConvert.SerializeObject(dates, Formatting.Indented);
            Console.WriteLine(dateDefault);

            // From Json.NET 4.5 and onwards dates are written using the ISO 8601            
            Console.WriteLine("- Serialize object specifying to use Iso DateTime converter");
            string dateIso8601 = JsonConvert.SerializeObject(
                dates,
                Formatting.Indented,
                new IsoDateTimeConverter()
            );

            Console.WriteLine(dateIso8601);

            Console.WriteLine("- Serialize object specifying Microsoft Date - Default pre .NET 4.5");
            JsonSerializerSettings settingsMicrosoftDate = new JsonSerializerSettings
            {
                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
            };

            string dateMicrosoftOldDefault = JsonConvert.SerializeObject(
                dates, 
                Formatting.Indented, 
                settingsMicrosoftDate
            );
            Console.WriteLine(dateMicrosoftOldDefault);

            Console.WriteLine("- Serialize object specifying custom date format");
            JsonSerializerSettings settingsCustomDate = new JsonSerializerSettings
            {
                DateFormatString = "dd/MM/yyyy"
            };

            string dateCustom = JsonConvert.SerializeObject(dates, Formatting.Indented, settingsCustomDate);
            Console.WriteLine(dateCustom);

            Console.WriteLine("- Serialize object specifying to use the JavaScript DateTime converter");
            string dateJavaScript = JsonConvert.SerializeObject(
                dates, 
                Formatting.Indented, 
                new JavaScriptDateTimeConverter()
            );
            Console.WriteLine(dateJavaScript);

            Console.WriteLine("- Serialize object specifying to use the UnixDateTimeConverter DateTime converter");
            string dateUnixDateTimeConverter = JsonConvert.SerializeObject(
                dates,
                Formatting.Indented,
                new UnixDateTimeConverter()
            );
            Console.WriteLine(dateUnixDateTimeConverter);
        }
    }
}
