using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m2_04_error_handling_demo
{
    public static class ConversionErrorsDemo
    {
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Conversion Errors with Delegate ***");

            var jsonDates = Generate.DatesJson();

            List<string> errors = new List<string>();

            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs errorArgs)
                {
                    errors.Add(errorArgs.ErrorContext.Error.Message);
                    errorArgs.ErrorContext.Handled = true;
                },
                Converters = { new IsoDateTimeConverter() }
            };            

            List<DateTime> deserializedDates;
            deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(jsonDates, settings);

            Console.WriteLine("Dates:");
            foreach (DateTime date in deserializedDates)
            {
                Console.WriteLine(date.ToShortDateString());
            }

            Console.WriteLine("Errors:");
            foreach (var err in errors)
            {
                Console.WriteLine(err);
            }
        }

        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            //Test if data in other format
            errorArgs.ErrorContext.Handled = true;
        }

    }
}
