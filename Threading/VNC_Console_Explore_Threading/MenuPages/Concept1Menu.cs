using EasyConsole;
using VNC_Console_Explore_Threading.Pages;

namespace VNC_Console_Explore_Threading.MenuPages
{
    class Concept1Menu : MenuPage
    {
        public Concept1Menu(Program program) : base("Page 1", program,
                  new Option("Page 1A", () => program.NavigateTo<Page1A>()),
                  new Option("Page 1B", () => program.NavigateTo<Page1B>()))
        {
        }
    }
}
