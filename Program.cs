using System;
using System.ComponentModel.Design;
using System.IO;
class Program
{
    static string help()
    {
        string text = new string('-', 70) + '\n';
        text += "Help for this program\n";
        text += "After launching program write the paths to the directories, where you want to see the file lists.\n";
        text += "This program also supports the input of several parametrs, just split each one by ','.";
        text += "For example: C:\\Users\\Professional\\Desktop,C:\\Users\\Professional\\Downloads\n";
        text += "Then write the file formats, of which files you want the program shows you. Remember to add '*'to the format type\n";
        text += "For example: *pdf,*txt\n";
        text += new string('-', 70);
        return text;
    }
    static void Main()
    {
        Console.WriteLine("Програма для виведення файлів у заданому каталозі за шаблоном");
        Console.WriteLine("Для виведення підказки введіть help\n");
        Console.Write("Введіть шляхи до каталогів через кому (,): ");
        string path = Console.ReadLine();
        if (path == "help") 
        {
            Console.WriteLine(help());
            return;
        }
        string[] pathArr = path.Split(',');
        Console.Write("Введіть шаблон файлів (наприклад, *.exe), через кому (,): ");
        string searchPattern = Console.ReadLine();
        if (searchPattern == "help")
        {
            Console.WriteLine(help());
            return;
        }
        string[] searchPatternArr = searchPattern.Split(',');
        bool anyPathExists = false;
        try
        {
            foreach (var p in pathArr)
            {
                if (Directory.Exists(p))
                {
                    if (!anyPathExists)
                    {
                        Console.WriteLine("{0,-30} {1,-20} {2,-15}", "Ім'я файлу", "Розмір (байти)", "Атрибути");
                        Console.WriteLine(new string('-', 70));
                        anyPathExists = true;
                    }
                    foreach (var sP in searchPatternArr)
                    {
                        Console.WriteLine($"Files in {p} with {sP}:");
                        foreach (string file in Directory.GetFiles(p, sP))
                        {
                            FileInfo fileInfo = new FileInfo(file);
                            FileAttributes attributes = fileInfo.Attributes;

                            Console.WriteLine("{0,-30} {1,-20} {2,-15}",
                            Path.GetFileName(file), fileInfo.Length, attributes.ToString());
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Помилка: Вказаний шлях не існує або не є каталогом.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Помилка: " + ex.Message);
        }
    }
}