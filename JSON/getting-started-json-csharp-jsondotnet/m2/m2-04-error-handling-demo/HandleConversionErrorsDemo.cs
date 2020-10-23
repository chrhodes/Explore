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
    public static class HandleConversionErrorsDemo
    {
        public static void Show ()
        {
            Console.Clear();
            Console.WriteLine("*** HandleConversionErrors Demo ***");

            var jsonDates = Generate.DatesJson();
            List<string> errors = new List<string>();
         
            JsonSerializerSettings settings = new JsonSerializerSettings
            {
                Error = HandleDeserializationError,
                Converters = { new IsoDateTimeConverter() }
            };

            List<DateTime> deserializedDates;
            deserializedDates = JsonConvert.DeserializeObject<List<DateTime>>(jsonDates, settings);

            Console.WriteLine("Dates:");
            foreach (DateTime date in deserializedDates)
            {
                Console.WriteLine(date.ToShortDateString());
            }
        }

        private static void HandleDeserializationError(object sender, ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            Console.WriteLine(currentError);
            errorArgs.ErrorContext.Handled = true;
        }

    }
}
