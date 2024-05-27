using System;
using System.IO;

class Lab4
{
    static void Main(string[] args)
    {
        if (args.Length == 0 || args.Length > 2 || (args.Length == 1 && args[0] == "--help"))
        {
            ShowHelp();
            Environment.Exit(1);
        }

        string directory = args[0];
        string filePattern = args.Length > 1 ? args[1] : "*";

        if (!Directory.Exists(directory))
        {
            Console.WriteLine("Указанный путь не существует или не является каталогом.");
            Environment.Exit(2);
        }

        try
        {
            ProcessDirectory(directory, filePattern);
            Environment.Exit(0);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Environment.Exit(3);
        }
    }

    static void ShowHelp()
    {
        Console.WriteLine("Использование: Program <каталог> [шаблон_файлов]");
        Console.WriteLine("Пример: Program C:\\Users\\User\\Documents *.exe");
        Console.WriteLine("Параметры:");
        Console.WriteLine("  <каталог>         Путь к каталогу, где будет выполняться поиск.");
        Console.WriteLine("  [шаблон_файлов]   Шаблон для поиска файлов (например, *.exe). Опционально, по умолчанию '*'.");
        Console.WriteLine("Программа поддерживает следующие коды завершения:");
        Console.WriteLine("  0 - Успешное выполнение.");
        Console.WriteLine("  1 - Неправильное использование программы.");
        Console.WriteLine("  2 - Указанный каталог не существует.");
        Console.WriteLine("  3 - Ошибка выполнения программы.");
    }

    static void ProcessDirectory(string directory, string filePattern)
    {
        foreach (string file in Directory.EnumerateFiles(directory, filePattern, SearchOption.AllDirectories))
        {
            FileInfo fileInfo = new FileInfo(file);
            string attributes = GetFileAttributes(fileInfo);
            Console.WriteLine($"{fileInfo.FullName} - {attributes}");
        }
    }

    static string GetFileAttributes(FileInfo fileInfo)
    {
        string attributes = string.Empty;
        if (fileInfo.Attributes.HasFlag(FileAttributes.Hidden))
        {
            attributes += "Скрытый ";
        }
        if (fileInfo.Attributes.HasFlag(FileAttributes.ReadOnly))
        {
            attributes += "Только чтение ";
        }
        if (fileInfo.Attributes.HasFlag(FileAttributes.Archive))
        {
            attributes += "Архивный ";
        }
        return string.IsNullOrEmpty(attributes) ? "Обычный" : attributes.Trim();
    }
}

