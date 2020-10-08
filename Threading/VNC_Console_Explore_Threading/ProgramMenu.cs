namespace VNC_Console_Explore_Threading
{
    class ProgramMenu : EasyConsole.Program
    {
        public ProgramMenu() : base("EasyConsole Menu", breadcrumbHeader: true)
        {
            // Register all Menu pages

            AddPage(new MenuPages.MainMenu(this));

            AddPage(new MenuPages.Concept1Menu(this));
            AddPage(new Pages.Page1A(this));
            AddPage(new Pages.Page1Ai(this));
            AddPage(new Pages.Page1B(this));
            AddPage(new Pages.Page1Bi(this));
            AddPage(new Pages.Page1BiA(this));
            AddPage(new Pages.Page1Bii(this));
            AddPage(new Pages.Page1Biii(this));

            AddPage(new MenuPages.Concept2Menu(this));
            AddPage(new Pages.Page2A(this));
            AddPage(new Pages.Page2Ai(this));
            AddPage(new Pages.Page2B(this));

            AddPage(new Pages.InputPage(this));

            // Set initial page

            SetPage<MenuPages.MainMenu>();
        }
    }
}
