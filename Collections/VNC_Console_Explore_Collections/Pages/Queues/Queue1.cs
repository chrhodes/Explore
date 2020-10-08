using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Queue1 : Page
    {
        public Queue1(Program program) : base("Queue1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Queue1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
