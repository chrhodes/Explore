using EasyConsole;
using VNC_Console_Explore_Threading.Pages;

namespace VNC_Console_Explore_Threading.MenuPages
{
    class MainMenu : MenuPage
    {
        public MainMenu(Program program) : base("Main Menu", program,
                  new Option("Page 1", () => program.NavigateTo<Concept1Menu>()),
                  new Option("Page 2", () => program.NavigateTo<Concept2Menu>()),
                  new Option("Input", () => program.NavigateTo<InputPage>()))
        {
        }
    }
}
