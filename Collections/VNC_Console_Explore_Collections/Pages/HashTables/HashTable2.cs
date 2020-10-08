using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class HashTable2 : Page
    {
        public HashTable2(Program program) : base("HashTable2", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page HashTable2");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
