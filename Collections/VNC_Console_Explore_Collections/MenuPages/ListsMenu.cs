using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class ListsMenu : MenuPage
    {
        public ListsMenu(Program program) : base("ListsMenu", program,
            new Option("List1", () => program.NavigateTo<List1>()),
            new Option("List2", () => program.NavigateTo<List2>()))
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Lists Menu");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
