

using System;
using System.IO;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Student(string name, string group, DateTime dateofbirth)
        {
            Name = name;
            Group = group;
            DateOfBirth = dateofbirth;
        }
    }

    class Program
    {
        public static void Main()
        {
            var MyProgram = new Program();
            string binFile;
            string deskpath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\Students";
            Console.WriteLine("Задайте имя файла для загрузки");
            binFile = Console.ReadLine();
            Console.WriteLine("1");

            if (File.Exists(binFile))
            {
                Console.WriteLine("2");
                Directory.CreateDirectory(deskpath);
                Console.WriteLine("3");

                using (var fileStreamRead = new FileStream(binFile, FileMode.Open))
                {
                    Console.WriteLine("4");
                    var bFormatter = new BinaryFormatter();
                    Console.WriteLine("5");
                    while (fileStreamRead.Position != fileStreamRead.Length)
                    {
                        Console.WriteLine("6");
                        Student[] newStudent = (Student[])bFormatter.Deserialize(fileStreamRead);
                        Console.WriteLine("7");
                        foreach (var student1 in newStudent)
                        {


                            using (BinaryWriter writer = new BinaryWriter(File.Open(deskpath + "\\" + student1.Group, FileMode.OpenOrCreate)))
                            {
                                Console.WriteLine("8");
                                writer.Write($"{student1.Name}, {student1.DateOfBirth}");
                            }
                        }

                    }
                }
            }
            else
            {
                Console.WriteLine("26");
                Console.WriteLine($"Такого файла '{binFile}' не существует");
                return;
            }
        }

    }
}
