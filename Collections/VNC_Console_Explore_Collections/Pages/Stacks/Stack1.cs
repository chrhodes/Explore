using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Stack1 : Page
    {
        public Stack1(Program program) : base("Stack1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Stack1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
