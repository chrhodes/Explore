using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Array1 : Page
    {
        public Array1(Program program) : base("Array1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Array1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
