using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Set2 : Page
    {
        public Set2(Program program) : base("Set2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Set2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
