using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWatcher
{
    class Program
    {
        static void Main(string[] args)
        {
            string modulePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ModulesDynamic");
            FileSystemWatcher fileWatcher = new FileSystemWatcher(modulePath, "*.txt");

            fileWatcher.Created += FileWatcher_Created;
            fileWatcher.Changed += FileWatcher_Changed;
            fileWatcher.Deleted += FileWatcher_Deleted;
            fileWatcher.Renamed += FileWatcher_Renamed;

            fileWatcher.EnableRaisingEvents = true;

            Console.ReadLine();
        }

        private static void FileWatcher_Renamed(object sender, RenamedEventArgs e)
        {
            Console.WriteLine("Renamed");
        }

        private static void  FileWatcher_Deleted(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Deleted");
        }

        private static void  FileWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Changed");
        }

        /// <summary>
        /// Raised when a new file is added to the ModulePath directory
        /// </summary>
        static void FileWatcher_Created(object sender, FileSystemEventArgs e)
        {
            Console.WriteLine("Created");
        }
    }
}
