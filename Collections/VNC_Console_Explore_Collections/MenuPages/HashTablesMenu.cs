using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class HashTablesMenu : MenuPage
    {
        public HashTablesMenu(Program program) : base("HashTablesMenu", program,
          new Option("HashTable 1", () => program.NavigateTo<HashTable1>()),
          new Option("HashTable 2", () => program.NavigateTo<HashTable2>()))
        {
        }
    }
}
