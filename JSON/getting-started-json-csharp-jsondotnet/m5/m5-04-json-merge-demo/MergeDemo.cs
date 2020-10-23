using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m5_04_json_merge_demo
{
    public static class MergeDemo
    {
        /// <summary>
        /// JSON Merge
        /// </summary>
        public static void Show()
        {
            Console.Clear();
            Console.WriteLine("*** MergeArrayHandling ***");

            // Two JSON objects
            string authorComments = @"{
                'name': 'Xavier Morera',
                'comments': ['Helpful Solr course(1)', 'Please provide better Jira samples(2),', 'Python(4)'],
                'upVotes': 5
            }";

            string newComment = @"{
                'name': 'Xavier Morera',
                'comments': ['Quick dotTrace tutorials!(3)', 'Helpful Solr course(1)', 'python(4)'],
                'upVotes': 1
            }";

            // Concatenate  
            Console.WriteLine("- Concat");
            JObject authorConcat = JObject.Parse(authorComments);
            JObject authorConcatMerge = JObject.Parse(newComment);
            authorConcat.Merge(authorConcatMerge, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Concat
            });
            Console.WriteLine(authorConcat.ToString());

            // Union  
            Console.WriteLine("- Union");
            JObject authorUnion = JObject.Parse(authorComments);
            JObject authorUnionMerge = JObject.Parse(newComment);
            authorUnion.Merge(authorUnionMerge, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Union
            });
            Console.WriteLine(authorUnion.ToString());

            // Replace  
            Console.WriteLine("- Replace");
            JObject authorReplace = JObject.Parse(authorComments);
            JObject authorReplaceMerge = JObject.Parse(newComment);
            authorReplace.Merge(authorReplaceMerge, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Replace
            });
            Console.WriteLine(authorReplace.ToString());

            // Merge  
            Console.WriteLine("- Merge");
            JObject authorMerge = JObject.Parse(authorComments);
            JObject authorMergeMerge = JObject.Parse(newComment);
            authorMerge.Merge(authorMergeMerge, new JsonMergeSettings
            {
                MergeArrayHandling = MergeArrayHandling.Merge
            });
            Console.WriteLine(authorMerge.ToString());
        }
    }
}
