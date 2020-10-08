using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class Array2 : Page
    {
        public Array2(Program program) : base("Array2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page Array2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
