using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Простой файловый менеджер");
            Console.WriteLine("1. Просмотр содержимого директории");
            Console.WriteLine("2. Создание файла/директории");
            Console.WriteLine("3. Удаление файла/директории");
            Console.WriteLine("4. Копирование файла/директории");
            Console.WriteLine("5. Перемещение файла/директории");
            Console.WriteLine("6. Чтение файла");
            Console.WriteLine("7. Запись в файл");
            Console.WriteLine("0. Выход");

            Console.Write("Введите номер операции: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ListDirectoryContents();
                    break;
                case "2":
                    CreateFileOrDirectory();
                    break;
                case "3":
                    DeleteFileOrDirectory();
                    break;
                case "4":
                    CopyFileOrDirectory();
                    break;
                case "5":
                    MoveFileOrDirectory();
                    break;
                case "6":
                    ReadFile();
                    break;
                case "7":
                    WriteToFile();
                    break;
                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Некорректный выбор. Пожалуйста, выберите существующую операцию.");
                    break;
            }
        }
    }

    static void ListDirectoryContents()
    {
        Console.Write("Введите путь к директории: ");
        string path = Console.ReadLine();

        try
        {
            string[] files = Directory.GetFiles(path);
            string[] directories = Directory.GetDirectories(path);

            Console.WriteLine("Файлы:");
            foreach (var file in files)
            {
                Console.WriteLine(Path.GetFileName(file));
            }

            Console.WriteLine("Директории:");
            foreach (var directory in directories)
            {
                Console.WriteLine(Path.GetFileName(directory));
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CreateFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории: ");
        string path = Console.ReadLine();

        try
        {
            Console.Write("Выберите тип (Файл - F, Директория - D): ");
            string typeChoice = Console.ReadLine().ToUpper();

            if (typeChoice == "F")
            {
                File.Create(path).Close();
                Console.WriteLine("Файл создан успешно.");
            }
            else if (typeChoice == "D")
            {
                Directory.CreateDirectory(path);
                Console.WriteLine("Директория создана успешно.");
            }
            else
            {
                Console.WriteLine("Некорректный выбор типа.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void DeleteFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории для удаления: ");
        string path = Console.ReadLine();

        try
        {
            if (File.Exists(path))
            {
                File.Delete(path);
                Console.WriteLine("Файл удален успешно.");
            }
            else if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                Console.WriteLine("Директория удалена успешно.");
            }
            else
            {
                Console.WriteLine("Файл/директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CopyFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории для копирования: ");
        string sourcePath = Console.ReadLine();

        Console.Write("Введите полный путь к месту копирования: ");
        string destinationPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                Console.WriteLine("Файл скопирован успешно.");
            }
            else if (Directory.Exists(sourcePath))
            {
                CopyDirectory(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                Console.WriteLine("Директория скопирована успешно.");
            }
            else
            {
                Console.WriteLine("Файл/директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void CopyDirectory(string source, string destination)
    {
        if (!Directory.Exists(destination))
        {
            Directory.CreateDirectory(destination);
        }

        string[] files = Directory.GetFiles(source);
        foreach (var file in files)
        {
            string fileName = Path.GetFileName(file);
            string destFile = Path.Combine(destination, fileName);
            File.Copy(file, destFile, true);
        }

        string[] dirs = Directory.GetDirectories(source);
        foreach (var dir in dirs)
        {
            string dirName = Path.GetFileName(dir);
            string destDir = Path.Combine(destination, dirName);
            CopyDirectory(dir, destDir);
        }
    }

    static void MoveFileOrDirectory()
    {
        Console.Write("Введите полный путь к файлу/директории для перемещения: ");
        string sourcePath = Console.ReadLine();

        Console.Write("Введите полный путь к месту перемещения: ");
        string destinationPath = Console.ReadLine();

        try
        {
            if (File.Exists(sourcePath))
            {
                File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                Console.WriteLine("Файл перемещен успешно.");
            }
            else if (Directory.Exists(sourcePath))
            {
                Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                Console.WriteLine("Директория перемещена успешно.");
            }
            else
            {
                Console.WriteLine("Файл/директория не существует.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }

    static void ReadFile()
    {
        Console.Write("Введите полный путь к файлу для чтения: ");
        string filePath = Console.ReadLine();

        try
        {
            if (File.Exists(filePath))
            {
                string content = File.ReadAllText(filePath);
                Console.WriteLine("Содержимое файла:");
                Console.WriteLine(content);
            }
            else
            {
                Console.WriteLine("Файл ");
                static void WriteToFile()
                {
                    Console.Write("Введите полный путь к файлу для записи: ");
                    string filePath = Console.ReadLine();

                    Console.WriteLine("Введите текст для записи в файл (Ctrl + Z, Enter для завершения ввода):");

                    try
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            while (true)
                            {
                                string line = Console.ReadLine();
                                if (line == null)
                                    break;

                                writer.WriteLine(line);
                            }
                        }

                        Console.WriteLine("Текст успешно записан в файл.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Ошибка: {ex.Message}");
                    }
                }

            }
