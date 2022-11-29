using System.Diagnostics.Metrics;
using System.Reflection;

namespace Conductor;

internal static class Program
{

    public static void Main()
    {
        Menu.ConvertData(); 
        while (true)
        {
            Menu.MenuArray();
            ConsoleKey Check;
            do
            {
                Arrow arrow = new Arrow();
                arrow.MenuArrow();
                Check = arrow.Key();
            }
            while (Check != ConsoleKey.RightArrow && Check != ConsoleKey.Escape
                 && Check != ConsoleKey.LeftArrow && Check != ConsoleKey.Enter);
            Menu.MenuFolderList(Check);
            Console.Clear();
        }
    }
}