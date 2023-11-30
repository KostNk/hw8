using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program1
{
    public long SizeDir(string dir1)
    {
        long fs = 0;
        Console.WriteLine($"Подсчитываем размер директории {dir1}");

        string[] files1 = Directory.GetFiles(dir1);
        foreach (string f1 in files1)
        {
            FileInfo fi = new FileInfo(f1);

            try
            {
                Console.WriteLine($"'{f1}' size = {fi.Length}");
                fs = fs + fi.Length;

            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine($"не могу удалить {f1} - нет прав");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error ocured while delete file = '{e}'");
            }
        }

        string[] subdirs1 = Directory.GetDirectories(dir1);
        foreach (string d1 in subdirs1)
        {
            fs = fs + SizeDir(d1);
        }
        return fs;
    }
    public static void Main()
    {
        var MyProgram = new Program1();
        string dirpath;
        Console.WriteLine("Задайте папку для подсчёта размера занимаемого места на диске");
        dirpath = Console.ReadLine();

        if (Directory.Exists(dirpath))
        {
            Console.WriteLine($"'{dirpath}' size = {MyProgram.SizeDir(dirpath)}");
        }
        else
        {
            Console.WriteLine($"Такой директории '{dirpath}' не существует");
            return;
        }
    }

}
//526 674 557