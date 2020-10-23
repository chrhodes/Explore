using Newtonsoft.Json;
using System;
using System.IO;
using System.Xml.Linq;

namespace m7_01_json_xml_demo
{
    public static class ComprehensiveDemo
    {
        /// <summary>
        /// Show some incompatibilities between xml and json
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** A more comprehensive demo ***");

            string moreComprehensiveXml = File.ReadAllText("SampleComprehensive.xml");

            XDocument xDoc = XDocument.Parse(moreComprehensiveXml);
            Console.WriteLine(xDoc.ToString());

            string authorsJSON = JsonConvert.SerializeXNode(xDoc, Newtonsoft.Json.Formatting.Indented);
            Console.WriteLine(authorsJSON);

            XDocument xDocFromJson = JsonConvert.DeserializeXNode(authorsJSON);
            Console.WriteLine(xDocFromJson.ToString());
        }
    }
}
