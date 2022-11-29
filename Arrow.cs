using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conductor;
internal class Arrow
    {

    public ConsoleKey Key()
    {
        Menu.key = Console.ReadKey(true);
        switch (Menu.key.Key)
        {
            case ConsoleKey.UpArrow:
                if (Menu.YCursorPos > 0)
                    Menu.YCursorPos--;
                break;
            case ConsoleKey.DownArrow:
                if (Menu.YCursorPos < Menu.FileList.Length - 1)
                    Menu.YCursorPos++;
                break;
        }
        return Menu.key.Key;
    }
    public void MenuArrow()
    {
        if (Menu.YCursorPos != 0)
        {
            Console.SetCursorPosition(0, Menu.YCursorPos - 1);
            Console.WriteLine("  ");
        }
        Console.SetCursorPosition(0, Menu.YCursorPos);
        Console.WriteLine("  ");
        Console.SetCursorPosition(0, Menu.YCursorPos + 1);
        Console.WriteLine("  ");
        Console.SetCursorPosition(0, Menu.YCursorPos);
        Console.WriteLine("=>");
        Console.SetCursorPosition(0, Menu.YCursorPos);
    }
}

