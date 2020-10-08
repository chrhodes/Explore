using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page1B : MenuPage
    {
        public Page1B(Program program) : base("Page1B", program,
                  new Option("Page 1Bi", () => program.NavigateTo<Page1Bi>()),
                  new Option("Page 1Bii", () => program.NavigateTo<Page1Bii>())
            )
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1B");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
