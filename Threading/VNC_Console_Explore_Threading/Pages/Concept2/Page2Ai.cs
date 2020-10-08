using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page2Ai : Page
    {
        public Page2Ai(Program program) : base("Page 2Ai", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 2Ai");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
