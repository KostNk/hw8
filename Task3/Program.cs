using System;
using System.IO;
using static System.Runtime.InteropServices.JavaScript.JSType;

class Program
{
    public void DeleteDir(string dir1)
    {
        //Console.WriteLine($"Зачищаем директорию {dir1}");

        string[] files1 = Directory.GetFiles(dir1);
        foreach (string f1 in files1)
        {
            //Console.WriteLine(File.GetLastAccessTime(f1));

            try
            {
                TimeSpan ts = DateTime.Now - File.GetLastAccessTime(f1);

                if (ts.TotalMinutes > 30)
                {
                    File.Delete(f1);
                }
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
            DeleteDir(d1);
        }
    }
    public long SizeDir(string dir1)
    {
        long fs = 0;
        //Console.WriteLine($"Подсчитываем размер директории {dir1}");

        string[] files1 = Directory.GetFiles(dir1);
        foreach (string f1 in files1)
        {
            FileInfo fi = new FileInfo(f1);

            try
            {
                //Console.WriteLine($"'{f1}' size = {fi.Length}");
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
        var MyProgram = new Program();
        string dirpath;
        Console.WriteLine("Задайте папку для зачистки файлов которые не использовались более 30 минут");
        dirpath = Console.ReadLine();

        if (Directory.Exists(dirpath))
        {
            Console.WriteLine($"'{dirpath}' размер до зачистки = {MyProgram.SizeDir(dirpath)}");
            MyProgram.DeleteDir(dirpath);
            Console.WriteLine($"'{dirpath}' размер после зачистки = {MyProgram.SizeDir(dirpath)}");
        }
        else
        {
            Console.WriteLine($"Такой директории '{dirpath}' не существует");
            return;
        }
    }

}