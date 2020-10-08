using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Queue2 : Page
    {
        public Queue2(Program program) : base("Queue2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Queue2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
