using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

using EaseEFDAL.EF;
using EaseEFDAL.Models;
using System.Data.Entity;

namespace ConsoleExploreEaseEFDal
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteLine("Using EaseEFDAL");

            //Explore_RoutingEntities();

            Explore_MCREntities();

            WriteLine("Press Enter to Exit ...");
            ReadLine();
        }
        static void Explore_MCREntities()
        {
            using (var mcrContext = new MCREntities())
            {
                WriteLine("PCNAreaTemplate");

                foreach (PCNAREATEMPLATE item in mcrContext.PCNAREATEMPLATEs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNAUTHGROUP");

                foreach (PCNAUTHGROUP item in mcrContext.PCNAUTHGROUPS)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNCHANGETEXT");

                foreach (PCNCHANGETEXT item in mcrContext.PCNCHANGETEXTs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNHEADERS");

                foreach (PCNHEADER item in mcrContext.PCNHEADERs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNNotifyQuestions");


                WriteLine("PCNMMs");

                foreach (PCNMM item in mcrContext.PCNMMs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNNOTES");

                foreach (PCNNOTE item in mcrContext.PCNNOTES)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                foreach (PCNNotifyQuestion item in mcrContext.PCNNotifyQuestions)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNPARTs");

                foreach (PCNPART item in mcrContext.PCNPARTS)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNRFCCATs");

                foreach (PCNRFCCAT item in mcrContext.PCNRFCCATs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNRFCCATEGORies");

                foreach (PCNRFCCATEGORY item in mcrContext.PCNRFCCATEGORies)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNRFCCOMMENTs");

                foreach (PCNRFCCOMMENT item in mcrContext.PCNRFCCOMMENTs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNRFCGROUPs");

                foreach (PCNRFCGROUP item in mcrContext.PCNRFCGROUPs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNSDs");

                foreach (PCNSD item in mcrContext.PCNSDs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNSDAuths");

                foreach (PCNSDAuth item in mcrContext.PCNSDAuths)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNSDAUTHCNTs");

                foreach (PCNSDAUTHCNT item in mcrContext.PCNSDAUTHCNTs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNTASKS");

                foreach (PCNTASK item in mcrContext.PCNTASKS)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNTASKDueDateChanges");

                foreach (PCNTASKDueDateChanx item in mcrContext.PCNTASKDueDateChanges)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNTASKEscalates");

                foreach (PCNTASKEscalate item in mcrContext.PCNTASKEscalates)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNTASKLISTs");

                foreach (PCNTASKLIST item in mcrContext.PCNTASKLISTs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNWITEXTs");

                foreach (PCNWITEXT item in mcrContext.PCNWITEXTs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("PCNWITHDRAWs");

                foreach (PCNWITHDRAW item in mcrContext.PCNWITHDRAWs)
                {
                    WriteLine(item);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();
            }

        }

        //private static void DisplayDbSet<T>(DbContext context, DbSet<T> dbset)
        //{
        //    WriteLine("{dbset}");

        //    foreach (T item in dbset)
        //    {
                
        //    }

        //}

        private static void Explore_RoutingEntities()
        {
            using (var routingContext = new RoutingEntities())
            {
                WriteLine("PARTXREF");

                foreach (PARTXREF part in routingContext.PARTXREFs)
                {
                    WriteLine(part);
                    //WriteLine(part.ID);
                    //WriteLine(part.PARTNO);
                    //WriteLine(part.PlantID);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("ROUTEHDR");

                foreach (ROUTEHDR route in routingContext.ROUTEHDRs)
                {
                    WriteLine(route);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();

                WriteLine("OPHDR");

                foreach (OPHDR operation in routingContext.OPHDRs)
                {
                    WriteLine(operation);
                }

                WriteLine("Press Enter to Continue ...");
                ReadLine();
                WriteLine("SUBHDR");

                foreach (SUBHDR step in routingContext.SUBHDRs)
                {
                    WriteLine(step);
                }
            }
        }
    }
}
