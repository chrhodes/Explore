using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class ConcurrentMenu : MenuPage
    {
        public ConcurrentMenu(Program program) : base("ConcurrentMenu", program,
            new Option("M1 RunProgram OneThread", () => program.NavigateTo<Concurrent_M1_RunProgramOneThread>()),
            new Option("M1 RunProgram MultiThreaded", () => program.NavigateTo<Concurrent_M1_RunProgramMultiThreaded>()),
            new Option("M1 RunProgram Concurrent", () => program.NavigateTo<Concurrent_M1_RunProgramConcurrent>()),
            new Option("M1 RunProgram With Locks", () => program.NavigateTo<Concurrent_M1_RunProgramWithLock>()),

            new Option("M2 Start", () => program.NavigateTo<Concurrent_M2_Start>()),
            new Option("M2 End", () => program.NavigateTo<Concurrent_M2_End>()),

            new Option("M3 BuyAndSell", () => program.NavigateTo<Concurrent_M3_BuyAndSell>()),

            new Option("M4 Queue", () => program.NavigateTo<Concurrent_M4_Queue>()),
            new Option("M4 ConcurrentQueue", () => program.NavigateTo<Concurrent_M4_ConcurrentQueue>()),
            new Option("M4 ConcurrentStack", () => program.NavigateTo<Concurrent_M4_ConcurrentStack>()),
            new Option("M4 ConcurrentBag", () => program.NavigateTo<Concurrent_M4_ConcurrentBag>()),
            new Option("M4 Interface", () => program.NavigateTo<Concurrent_M4_Interface>()),

            new Option("M5_SalesBonuses", () => program.NavigateTo<Concurrent_M5_SalesBonuses>()),

            new Option("M6 EnumerateDictionary", () => program.NavigateTo<Concurrent_M6_EnumerateDictionary>()),
            new Option("M6 EnumerateConcurrentDictionary", () => program.NavigateTo<Concurrent_M6_EnumerateConcurrentDictionary>())
            )
        {
        }
    }
}
