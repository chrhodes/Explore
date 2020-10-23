using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Linq;

namespace m7_01_json_xml_demo
{
    public static class ForceArrayDemo
    {
        /// <summary>
        /// Force array
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** Force Array ***");

            string authorsSourceXmlForceArray = File.ReadAllText("SampleForceArray.xml");
            Console.WriteLine("- Source XML");
            Console.WriteLine(authorsSourceXmlForceArray);

            XDocument xDoc = XDocument.Parse(authorsSourceXmlForceArray);

            string authorsJSON = JsonConvert.SerializeXNode(xDoc, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine("- JSON");
            Console.WriteLine(authorsJSON);


            XDocument xDocFromJson = JsonConvert.DeserializeXNode(authorsJSON);
            Console.WriteLine("- XML using DeserializeXNode");
            Console.WriteLine(xDocFromJson.ToString());            

            XDocument xDocWithProperties = JsonConvert.DeserializeXNode(authorsJSON, "TestJsonXMLConversion");
            Console.WriteLine("- XML with a different root node name");
            Console.WriteLine(xDocWithProperties.ToString());
        }
    }
}
