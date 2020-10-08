using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class List1 : Page
    {
        public List1(Program program) : base("List1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page List1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
