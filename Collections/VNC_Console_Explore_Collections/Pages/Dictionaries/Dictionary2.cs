using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Dictionary2 : Page
    {
        public Dictionary2(Program program) : base("Dictionary2", program)
        {
        }
        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Dictionary2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
