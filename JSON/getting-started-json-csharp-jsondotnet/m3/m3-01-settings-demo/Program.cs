using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m3_01_settings_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            // IMPORTANT: 
            // You need to use the same settings for deserialization as you did for serialization.

            // MissingMemberHandling
            DeserializationMissingMemberDemo.Show();

            // ReferenceLoopHandling
            SerializationCircularReferencesDemo.Show();

            // NullValueHandling
            SerializationNullValueHandlingDemo.Show();

            // DefaultValueHandling
            DefaultValueHandlingDemo.Show();

            // ObjectCreationHandling
            ObjectCreationHandlingDemo.Show();

            // TypeNameHandling
            TypeNameHandlingDemo.Show();

            // TypeNameAssemblyFormatHandling
            TypeNameAssemblyFormatHandlingDemo.Show();  

            // SerializationBinder 
            BinderDemo.Show();

            // MetadataPropertyHandling
            MetadataHandlingDemo.Show();

            // ConstructorHandling
            ConstructorHandlingDemo.Show();
        }
    }
}
