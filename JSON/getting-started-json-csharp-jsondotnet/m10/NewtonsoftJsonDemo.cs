using Newtonsoft.Json;
using System;

namespace m10_01_future_json
{
    public static class NewtonsoftJsonDemo
    {
        public static void Show ()
        {
            string xavierJSON = @"
                {
                    name: 'Xavier Morera',
                    'courses': [
                        'Solr',
                        'Jira',
                        'dotTrace'
                    ],
                    'since': '2014-01-14T00:00:00',
                    happy: true,
                    'issues': null,
                    'car': {
                        'model': 'Land Rover Series III',
                        'year': 1976
                    },
                    'authorRelationship': 1
                }";

            /* Newtonsoft.Json */
            Author xavierAuthor = Newtonsoft.Json.JsonConvert.DeserializeObject<Author>(xavierJSON);
            System.Console.WriteLine(xavierAuthor.name);

            string xavierString = JsonConvert.SerializeObject(xavierAuthor, Formatting.Indented);
            System.Console.WriteLine(xavierString);

            Console.WriteLine(Environment.Version.ToString());

        }
    }
}
