using EasyConsole;

namespace VNC_Console_Explore_Threading.Pages
{
    class Page2A : MenuPage
    {
        public Page2A(Program program) : base("Page 2A", program,
                  new Option("Page 2Ai", () => program.NavigateTo<Page2Ai>()))
        {
        }
    }
}
