using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page1Bii : MenuPage
    {
        public Page1Bii(Program program) : base("Page 1Bii", program,
            new Option("Page 1BiA", () => program.NavigateTo<Page1BiA>()))
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1Bi");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
