using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace m4_03_callbacks_demo
{
    class Author
    {
        private Stopwatch timer = new Stopwatch();
        public string name { get; set; }
        public string[] courses { get; set; }
        public DateTime since { get; set; }
        public bool happy { get; set; }
        public object issues { get; set; }

        [OnSerializing]
        internal void OnSerializingMethod(StreamingContext context)
        {
            timer.Reset();
            timer.Start();
            Console.WriteLine("OnSerializing: started serialization process");
        }

        [OnSerialized]
        internal void OnSerializedMethod(StreamingContext context)
        {
            timer.Stop();
            Console.WriteLine("OnSerialized: finished serialization in " + timer.ElapsedMilliseconds + " ms");
        }

        [OnDeserializing]
        internal void OnDeserializingMethod(StreamingContext context)
        {
            timer.Reset();
            timer.Start();
            Console.WriteLine("OnDeserializing: started deserialization process");
        }

        [OnDeserialized]
        internal void OnDeserializedMethod(StreamingContext context)
        {
            timer.Stop();
            Console.WriteLine("OnDeserialized: finished deserialization in " + timer.ElapsedMilliseconds + " ms");
        }
    }
}
