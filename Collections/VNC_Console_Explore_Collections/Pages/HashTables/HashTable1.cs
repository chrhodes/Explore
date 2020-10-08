using EasyConsole;

namespace VNC_Console_Explore_Collections.Pages
{
    class HashTable1 : Page
    {
        public HashTable1(Program program) : base("HashTable1", program)
        {
        }

        public override void Display()
        {
            base.Display();

            Output.WriteLine("Hello from Page HashTable1");

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}
