using EasyConsole;
using VNC_Console_Explore_Threading.Pages;

namespace VNC_Console_Explore_Threading.MenuPages
{
    class Concept2Menu : MenuPage
    {
        public Concept2Menu(Program program) : base("Concept2Menu", program,
                  new Option("Page 2A", () => program.NavigateTo<Page2A>()),
                  new Option("Page 2B", () => program.NavigateTo<Page2B>()))
        {
        }
    }
}
