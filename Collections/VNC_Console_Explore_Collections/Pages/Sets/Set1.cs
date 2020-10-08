using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Set1 : Page
    {
        public Set1(Program program) : base("Set1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Set1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
