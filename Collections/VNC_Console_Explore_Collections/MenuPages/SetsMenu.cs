using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class SetsMenu : MenuPage
    {
        public SetsMenu(Program program) : base("SetsMenu", program,
          new Option("Page 1A", () => program.NavigateTo<Set1>()),
          new Option("Page 1B", () => program.NavigateTo<Set2>()))
        {
        }
    }
}
