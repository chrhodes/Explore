using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.SharePoint.Client;

namespace SharePointConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            string url = @"http://communities.na.firstam.net/sites/ITMP";

            // Super simple example
            //using (var ctx = new ClientContext(url))
            //{
            //    var web = ctx.Web;

            //    ctx.Load(web);

            //    ctx.ExecuteQuery();

            //    Console.WriteLine(web.Title);
            //    Console.ReadLine();
            //}


            //using (var ctx = new ClientContext(url))
            //{
            //    var web = ctx.Web;

            //    // This doesn't work
            //    //List announcements = web.Lists[0];

            //    // This works but isn't initialized
            //    List announcements = web.Lists.GetByTitle("Announcements");
            //    // This works but isn't initialized
            //    ctx.Load(announcements.ContentTypes);

            //    List sharedDocs = web.Lists.GetByTitle("Shared Documents");
            //    ctx.Load(sharedDocs.ContentTypes);

            //    // This brings the data back
            //    ctx.ExecuteQuery();

            //    ContentType announcementCtype = announcements.ContentTypes[0];

            //    Console.WriteLine(announcementCtype.Name);

            //    ContentType sharedDocsCType0 = sharedDocs.ContentTypes[0];
            //    ContentType sharedDocsCType1 = sharedDocs.ContentTypes[1];

            //    Console.WriteLine(sharedDocsCType0.Name);
            //    Console.WriteLine(sharedDocsCType1.Name);

            //    // NB. The web and announcements objects have not been loaded!.  This is efficient if we don't need them.

            //    Console.ReadLine();
            //}

            // Can use Lambda expressions to do filtering on Load()!
            //using (var ctx = new ClientContext(url))
            //{
            //    var web = ctx.Web;

            //    ctx.Load(web, x => x.Title, x => x.Description);

            //    ctx.ExecuteQuery();

            //    Console.WriteLine(web.Title);
            //    Console.WriteLine(web.Description);

            //    Console.ReadLine();
            //}

            // Do not have to read before updating

            //using (var ctx = new ClientContext(url))
            //{
            //    var web = ctx.Web;

            //    web.Description = "New Description";

            //    // Do need to call Update and ExectuteQuery
            //    web.Update();
            //    ctx.ExecuteQuery();

            //    Console.ReadLine();
            //}

            // Can use LINQ to filter queries.

            //using (var ctx = new ClientContext(url))
            //{
            //    var web = ctx.Web;
            //    var lists = web.Lists;

            //    ctx.Load(lists, 
            //        l => l.Include
            //        (list => list.Title).Where
            //        (list => list.BaseType == BaseType.GenericList)
            //        );

            //    ctx.ExecuteQuery();

            //    foreach (var list in lists)
            //    {
            //        Console.WriteLine(list.Title);
            //    }
            //    Console.ReadLine();
            //}

            // LoadQuery returns IEnumerable<T>

            using (var ctx = new ClientContext(url))
            {
                var web = ctx.Web;
                var lists = web.Lists;

                IEnumerable<List> doclibs = ctx.LoadQuery(lists.Where
                    (list => list.BaseType == BaseType.DocumentLibrary)
                    );

                ctx.ExecuteQuery();

                foreach (var list in doclibs)
                {
                    Console.WriteLine(list.Title);
                }
                Console.ReadLine();
            }

        }

    }
}
