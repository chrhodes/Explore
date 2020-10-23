using JsonSamples;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m4_04_tracewriter
{
    public static class MemoryTraceWriterDemo
    {
        /// <summary>
        /// TraceWriter
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Memory Trace Writer ***");
            string xavierJson = Generate.SingleJsonError();

            Console.WriteLine("- Where does this error come from?");
            try
            {
                Author xavierAuthor = JsonConvert.DeserializeObject<Author>(xavierJson);
                Console.WriteLine(xavierAuthor.car.model);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("- Memory TraceWriter will tell you step by step");
            ITraceWriter traceWriter = new MemoryTraceWriter();
            Author xavierAuthorTraceWriter = JsonConvert.DeserializeObject<Author>(xavierJson, new JsonSerializerSettings
            {
                TraceWriter = traceWriter
            });
            Console.WriteLine(traceWriter);
        }
    }
}
