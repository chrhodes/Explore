using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page1Biii : Page
    {
        public Page1Biii(Program program) : base("Page1Biii", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1Biii");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
