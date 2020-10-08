using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page2B : Page
    {
        public Page2B(Program program) : base("Page 2B", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 2B");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
