using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page1BiA : MenuPage
    {
        public Page1BiA(Program program) : base("Page 1BiA", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page 1BiA");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
