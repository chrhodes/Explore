using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class DictionariesMenu : MenuPage
    {
        public DictionariesMenu(Program program) : base("DictionariesMenu", program,
          new Option("Dictionary1", () => program.NavigateTo<Dictionary1>()),
          new Option("Dictionary1", () => program.NavigateTo<Dictionary2>()))
        {
        }
    }
}
