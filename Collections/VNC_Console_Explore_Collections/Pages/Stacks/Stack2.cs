using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Stack2 : Page
    {
        public Stack2(Program program) : base("Stack2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Stack2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
