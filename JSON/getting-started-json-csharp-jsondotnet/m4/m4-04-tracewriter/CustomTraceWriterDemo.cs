using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_04_tracewriter
{
    public static class CustomTraceWriterDemo
    {
        /// <summary>
        /// Custom TraceWriter 
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Custom Trace Writer ***");

            string xavierJson = Generate.SingleJsonError();

            // No output
            Author xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson);
            Console.WriteLine(xavierAuthor.car);

            // Off
            ITraceWriter traceWriterOff = new FileLogTraceWriter(TraceLevel.Off, "TraceLevel.Off");
            xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
            {
                TraceWriter = traceWriterOff
            });

            // Verbose
            ITraceWriter traceWriterVerbose = new FileLogTraceWriter(TraceLevel.Verbose, "TraceLevel.Verbose");
            xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
            {
                TraceWriter = traceWriterVerbose
            });

            // Error
            ITraceWriter traceWriterErrors = new FileLogTraceWriter(TraceLevel.Error, "TraceLevel.Error");
            xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
            {
                TraceWriter = traceWriterErrors
            });

            // Error with MissingMemberHandling.Error
            ITraceWriter traceWriterErrorMissingMember = new FileLogTraceWriter(TraceLevel.Error, "TraceLevel.Error with MissingMemberHandling.Error");
            try
            {
                xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
                {
                    TraceWriter = traceWriterErrorMissingMember,
                    MissingMemberHandling = MissingMemberHandling.Error
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Error has been logged");
            }
        }
    }
}
