using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Dictionary1 : Page
    {
        public Dictionary1(Program program) : base("Dictionary1", program)
        {
        }
        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Dictionary1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
