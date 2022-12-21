using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;//ленивые твари
//работает плохо Path.GetExtension(File1)
namespace Conductor;
internal static class Menu
{
    private static string[] ConsoleDiscription= new string[] {"|Стрелками двигаться по директориям","|Escape - вернуться в меню выбора диска","|Enter - открыть файл" };
    private static string FilePath="";
    private static List<DriveInfo> DiskList = DriveInfo.GetDrives().ToList();
    private static int[] DiskSizeList = new int[] {};
    private static int[] DiskFreeSizeList = new int[] {};
    public static string[] FileList = new string[] { };//количество возможных дисков
    public static ConsoleKeyInfo key;
    public static int YCursorPos;

    static Menu()
    {
        Console.CursorVisible = false;
    }
    public static void ConvertData()
    {
        Array.Clear(FileList, 0, FileList.Length);
        Array.Resize(ref FileList, DiskList.Count);//как увеличить массив на 15 см
        Array.Resize(ref DiskSizeList, DiskList.Count);
        Array.Resize(ref DiskFreeSizeList, DiskList.Count);
        int i = 0;
        foreach (var oneDisk in DiskList)
        {
            FileList[i] =oneDisk.Name;
            DiskSizeList[i] = (int)(oneDisk.TotalSize/((long)Math.Pow(2,30)));
            DiskFreeSizeList[i] = (int)(oneDisk.TotalFreeSpace / ((long)Math.Pow(2, 30)));
            i++;
        }
    }
    public static void MenuFolderList(ConsoleKey key)
    {
        MenuFolderListGo(key);
        MenuFolderListSave(key);
        MenuFolderListOpen(key);
        YCursorPos = 0;
    }
    private static void MenuFolderListGo(ConsoleKey key)
    {
        if (key == ConsoleKey.RightArrow && Directory.Exists(FileList[YCursorPos]))
            FilePath = FileList[YCursorPos];
        else if (key == ConsoleKey.LeftArrow)
        {
            FilePath = FilePath.Substring(0, FilePath.LastIndexOf('\\'));
            if (FilePath.Length <= 2)
                FilePath = FilePath + "\\";//заходим в выбранный диск
        }
    }
    private static void MenuFolderListSave(ConsoleKey key)
    {
        if (key == ConsoleKey.Escape)
            ConvertData();
        else
            FileList = Directory.GetFileSystemEntries(FilePath);
    }
    private static void MenuFolderListOpen(ConsoleKey key)
    {
        if (key == ConsoleKey.Enter)
            Process.Start(new ProcessStartInfo { FileName= FileList[YCursorPos], UseShellExecute=true });
    }
    public static void MenuArray()
    {
        FileName();
        DiscriptionFileTipe();
        DiscriptionDiskList();
        Discription();
        Console.SetCursorPosition(0, YCursorPos);
    }
    private static void FileName()
    {
        int i = 0;
        foreach (var File1 in FileList)
        {

            Console.SetCursorPosition(2, i);
            if (File1.Length <= 3)
                Console.WriteLine(File1[..^1]);
            else
            {
                string file2 = File1.Substring(File1.LastIndexOf('\\') + 1);
                if (file2.Length <= 37)
                {
                    Console.WriteLine(file2 + String.Concat(Enumerable.Repeat(" ", 37 - file2.Length)));
                }
                else
                    Console.WriteLine(File1.Substring(File1.LastIndexOf('\\') + 1).Substring(0, 34) + "...");
                //вставляем троиточие если имя файла или дерриктори слишком большое
            }
            i++;
        }
    }
    private static void Discription()
    {
        int i = 0;
        foreach (var Discription in ConsoleDiscription)
        {
            Console.SetCursorPosition(80, i);
            Console.WriteLine(Discription);
            i++;
        }

    }
    private static void DiscriptionDiskList()
    {
        int i = 0;
        foreach (var File1 in FileList)
        {
            Console.SetCursorPosition(40, i);
            if (File1.Length <= 3)
                Console.WriteLine("Занято " + DiskSizeList[i] + " Гб. Свободно " + DiskFreeSizeList[i] + " Гб.");
            else
                    Console.WriteLine(File.GetCreationTime(File1));
            i++;
        }
    }
    private static void DiscriptionFileTipe()
    {
        int i = 0;
        foreach (var File1 in FileList)
        {
            Console.SetCursorPosition(60, i);
            if (File.Exists(File1))
                Console.WriteLine("<"+Path.GetExtension(File1).Replace(".","")+">");
            else if (Directory.Exists(File1))
                Console.WriteLine("<DIR>");
            i++;
        }
    }
}