using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml;
using System.Xml.Linq;

namespace m7_01_json_xml_demo
{
    public static class ToAndFromDemo
    {
        /// <summary>
        /// Convert from XML to JSON and back
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** XML and JSON ***");

            string authorsSourceXml = File.ReadAllText("SampleAuthors.xml");
            Console.WriteLine("- String with source XML");
            Console.WriteLine(authorsSourceXml);

            // Load XML document from file
            XmlDocument xDoc = new XmlDocument();
            xDoc.LoadXml(authorsSourceXml);

            // Convert XML to JSON 
            string authorsJSON = JsonConvert.SerializeXmlNode(xDoc, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine("- JSON text");
            Console.WriteLine(authorsJSON);

            // Convert JSON to XML using XmlDocument
            XmlDocument authorAgainXMLDocument = JsonConvert.DeserializeXmlNode(authorsJSON);
            Console.WriteLine("- JSON to XML using DeserializeXmlNode");
            Console.WriteLine(authorAgainXMLDocument.InnerXml);

            // Convert JSON to XML using XDocument
            XDocument authorAgainXMLDoc = JsonConvert.DeserializeXNode(authorsJSON);
            Console.WriteLine("- JSON to XML using DeserializeXNode");
            Console.WriteLine(authorAgainXMLDoc.ToString());
        }
    }
}
