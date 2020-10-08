
namespace VNC_Console_Explore_Collections
{
    class ProgramMenu : EasyConsole.Program
    {
        public ProgramMenu() : base ("EasyConsole Menu", breadcrumbHeader: true)
        {
            // Register all Menu pages

            AddPage(new MenuPages.MainMenu(this));

            AddPage(new MenuPages.ArraysMenu(this));
            AddPage(new Pages.Array1(this));
            AddPage(new Pages.Array2(this));
            AddPage(new Pages.Array_DaysOfWeek(this));

            AddPage(new MenuPages.ConcurrentMenu(this));

            AddPage(new MenuPages.DictionariesMenu(this));
            AddPage(new Pages.Dictionary1(this));
            AddPage(new Pages.Dictionary2(this));

            AddPage(new MenuPages.HashTablesMenu(this));
            AddPage(new Pages.HashTable1(this));
            AddPage(new Pages.HashTable2(this));

            AddPage(new MenuPages.InputPage(this));

            AddPage(new MenuPages.ListsMenu(this));
            AddPage(new Pages.List1(this));
            AddPage(new Pages.List2(this));

            AddPage(new MenuPages.SetsMenu(this));
            AddPage(new Pages.Set1(this));
            AddPage(new Pages.Set1(this));

            AddPage(new MenuPages.SetsMenu(this));
            AddPage(new Pages.Set1(this));
            AddPage(new Pages.Set1(this));

            AddPage(new MenuPages.StacksMenu(this));
            AddPage(new Pages.Stack1(this));
            AddPage(new Pages.Stack2(this));

            AddPage(new MenuPages.ConcurrentMenu(this));
            AddPage(new Pages.Concurrent_M1_RunProgramOneThread(this));
            AddPage(new Pages.Concurrent_M1_RunProgramMultiThreaded(this));
            AddPage(new Pages.Concurrent_M1_RunProgramConcurrent(this));
            AddPage(new Pages.Concurrent_M1_RunProgramWithLock(this));

            AddPage(new Pages.Concurrent_M2_Start(this));
            AddPage(new Pages.Concurrent_M2_End(this));

            AddPage(new Pages.Concurrent_M3_BuyAndSell(this));

            AddPage(new Pages.Concurrent_M4_ConcurrentBag(this));
            AddPage(new Pages.Concurrent_M4_ConcurrentQueue(this));
            AddPage(new Pages.Concurrent_M4_ConcurrentStack(this));
            AddPage(new Pages.Concurrent_M4_Interface(this));
            AddPage(new Pages.Concurrent_M4_Queue(this));

            // Set initial page

            SetPage<MenuPages.MainMenu>();
        }
    }
}
