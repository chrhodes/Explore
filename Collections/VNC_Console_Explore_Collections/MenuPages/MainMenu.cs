using EasyConsole;
using VNC_Console_Explore_Collections.MenuPages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class MainMenu : MenuPage
    {
        public MainMenu(Program program) : base("Main Menu", program,
                  new Option("Arrays", () => program.NavigateTo<ArraysMenu>()),
                  new Option("Lists Menu", () => program.NavigateTo<ListsMenu>()),
                  new Option("Dictionaries Menu", () => program.NavigateTo<DictionariesMenu>()),
                  new Option("HashTables Menu", () => program.NavigateTo<HashTablesMenu>()),
                  new Option("Sets", () => program.NavigateTo<SetsMenu>()),
                  new Option("Input", () => program.NavigateTo<InputPage>()),
                  new Option("Concurrent", () => program.NavigateTo<ConcurrentMenu>()))
        {
        }
    }
}
