using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class StacksMenu : MenuPage
    {
        public StacksMenu(Program program) : base("StacksMenu", program,
            new Option("Stack1", () => program.NavigateTo<Stack1>()),
            new Option("Stack1", () => program.NavigateTo<Stack1>()))
        {
        }
    }
}
