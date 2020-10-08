using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page1Bi : MenuPage
    {
        public Page1Bi(Program program) : base("Page 1Bi", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1Bi");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
