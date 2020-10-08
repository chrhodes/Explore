using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class List2 : Page
    {
        public List2(Program program) : base("List2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page List2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
