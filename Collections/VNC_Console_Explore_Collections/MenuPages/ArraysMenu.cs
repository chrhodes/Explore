using EasyConsole;
using VNC_Console_Explore_Collections.Pages;

namespace VNC_Console_Explore_Collections.MenuPages
{
    class ArraysMenu : MenuPage
    {
        public ArraysMenu(Program program) : base("ArraysMenu", program,
            new Option("Array1", () => program.NavigateTo<Array1>()),
            new Option("Array Two", () => program.NavigateTo<Array2>()),
            new Option("Days of Week", () => program.NavigateTo<Array_DaysOfWeek>()))
        {
        }
    }
}
