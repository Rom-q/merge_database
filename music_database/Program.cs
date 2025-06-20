using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class MusicMerger
{
    static readonly string[] SupportedFormats = { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3" };
    static readonly Dictionary<string, int> FormatPriority = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
    {
        [".wav"] = 6,
        [".flac"] = 5,
        [".alac"] = 4,
        [".aac"] = 3,
        [".ogg"] = 2,
        [".mp3"] = 1,
    };

    static void Main()
    {

        Console.OutputEncoding = Encoding.UTF8;
        PrintHeader();
        Console.WriteLine("[1] Слияние музыкальных баз");
        Console.WriteLine("[2] Генерация тестовых баз");
        Console.WriteLine("[3] Подсчет файлов");
        Console.Write("→ Введите номер опции: ");
        string input = Console.ReadLine();
        if (input == "1")
        {

        }
        else if(input == "2")
        {
            MusicTestBaseGenerator.Run();
            return;
        }
        else if (input == "3")
        {
            Value.Run();
            return;
        }
        else
        {
            Console.WriteLine("Не правильное значение");
            return;
        }
            Console.Write("Введите путь к новой музыкальной базе: ");
        string newBase = Console.ReadLine().Trim('"');

        Console.Write("Введите путь к старой музыкальной базе: ");
        string oldBase = Console.ReadLine().Trim('"');

        Console.Write("Введите путь, куда сохранить объединённую базу: ");
        string mergedBase = Console.ReadLine().Trim('"');

        Directory.CreateDirectory(mergedBase);
        string logDir = Path.Combine(mergedBase, "logs");
        Directory.CreateDirectory(logDir);

        string add = Path.Combine(logDir, "add.txt");
        string change = Path.Combine(logDir, "change.txt");
        string delete = Path.Combine(logDir, "delete.txt");

        File.WriteAllText(add, "=== Добавлено ===\n\n");
        File.WriteAllText(change, "=== Изменено ===\n\n");
        File.WriteAllText(delete, "=== Пропущено ===\n\n");

        var oldFiles = Directory.GetFiles(oldBase, "*.*", SearchOption.AllDirectories)
            .Where(f => SupportedFormats.Contains(Path.GetExtension(f))).ToList();
        var newFiles = Directory.GetFiles(newBase, "*.*", SearchOption.AllDirectories)
            .Where(f => SupportedFormats.Contains(Path.GetExtension(f))).ToList();

        var newFileMap = new Dictionary<string, (string path, string format, DateTime date)>();
        foreach (var nf in newFiles)
        {
            string id = ExtractTrackId(nf);
            if (id == null) continue;
            newFileMap[id] = (nf, Path.GetExtension(nf), File.GetLastWriteTime(nf));
        }

        int totalFiles = oldFiles.Count;
        int processed = 0;

        foreach (var oldFile in oldFiles)
        {
            string id = ExtractTrackId(oldFile);
            if (id == null) continue;

            string ext = Path.GetExtension(oldFile);
            DateTime oldDate = File.GetLastWriteTime(oldFile);

            string relativePath = Path.GetRelativePath(oldBase, oldFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath));

            if (newFileMap.TryGetValue(id, out var existing))
            {
                int oldPriority = FormatPriority.ContainsKey(ext) ? FormatPriority[ext] : 0;
                int newPriority = FormatPriority.ContainsKey(existing.format) ? FormatPriority[existing.format] : 0;

                if (oldPriority > newPriority || (oldPriority == newPriority && oldDate > existing.date))
                {
                    File.Copy(oldFile, mergedPath, true);
                    File.AppendAllText(change, $"[~] Обновлён: {relativePath} (по формату/дате)\n");
                }
                else
                {
                    File.AppendAllText(delete, $"[-] Пропущен: {relativePath} (приоритет ниже)\n");
                }
            }
            else
            {
                File.Copy(oldFile, mergedPath, true);
                File.AppendAllText(add, $"[+] Добавлен: {relativePath} (из старой базы)\n");
            }

            processed++;
            DrawProgressBar(processed, totalFiles, prefix: "Обработка файлов");
        }

        var mergedFileIds = new HashSet<string>(
    Directory.GetFiles(mergedBase, "*.*", SearchOption.AllDirectories)
    .Select(f => ExtractTrackId(f))
    .Where(id => id != null)
);
        foreach (var newFile in newFiles)
        {
            string id = ExtractTrackId(newFile);
            if (id == null || mergedFileIds.Contains(id)) continue;

            string relativePath = Path.GetRelativePath(newBase, newFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath));

            File.Copy(newFile, mergedPath, true);
            File.AppendAllText(add, $"[+] Добавлен: {relativePath} (из новой базы)\n");
        }

        Console.WriteLine("\n\nСлияние завершено. Логи сохранены в: " + logDir);
        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadLine();
    }

    static string ExtractTrackId(string path)
    {
        string fileName = Path.GetFileNameWithoutExtension(path);
        if (fileName.Length >= 12 && fileName[4] == '_' && fileName[8] == '_')
            return fileName.Substring(0, 12);
        return null;
    }

    static void DrawProgressBar(int progress, int total, int barLength = 40, string prefix = "")
    {
        double percent = (double)progress / total;
        int filled = (int)(barLength * percent);
        string bar = new string('█', filled) + new string('░', barLength - filled);
        Console.CursorLeft = 0;
        Console.Write($"{prefix} [{bar}] {progress}/{total}");
    }

    static void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("============================================");
        Console.WriteLine("        Автоматическое сравнение баз        ");
        Console.WriteLine("============================================\n");
        Console.ResetColor();
    }
}
class MusicTestBaseGenerator
{
    static readonly string[] Formats = { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3" };
    static readonly string[] Genres = {
        "Эпическая музыка",
        "Медитативная",
        "Напряжённая",
        "Спокойная",
        "Мрачная",
        "Космическая",
        "Военная",
        "Эмоциональная"
    };

    static Random rand = new Random();

    public static void Run()
    {
        Console.Write("Введите путь, куда создать тестовые базы: ");
        string basePath = Console.ReadLine().Trim('"');

        string newBase = Path.Combine(basePath, "new_base");
        string oldBase = Path.Combine(basePath, "old_base");

        Directory.CreateDirectory(newBase);
        Directory.CreateDirectory(oldBase);

        var ids = Enumerable.Range(0, 1000).Select(i => $"MUSC_{i / 10:000}_{i % 10:000}").ToList();

        GenerateBase(newBase, ids, 20, 40, allowExtraFolders: false);
        GenerateBase(oldBase, ids, 20, 40, allowExtraFolders: true);

        Console.WriteLine("\nТестовые базы сгенерированы.");
        Console.WriteLine($"Новая база: {newBase}");
        Console.WriteLine($"Старая база: {oldBase}");
        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadLine();
    }

    static void GenerateBase(string baseDir, List<string> ids, int minFiles, int maxFiles, bool allowExtraFolders = false)
    {
        var allGenres = new List<string>(Genres);

        if (allowExtraFolders)
        {
            allGenres.AddRange(new[] { "Рандом", "Без категории", "Необработаное", "Демо", "Неподходящее" });
        }

        foreach (var genre in allGenres.OrderBy(_ => rand.Next()))
        {
            string genrePath = Path.Combine(baseDir, genre);
            Directory.CreateDirectory(genrePath);

            bool hasSubfolders = rand.NextDouble() < 0.5;
            int subfolderCount = hasSubfolders ? rand.Next(1, 4) : 0;

            List<string> targetDirs = new() { genrePath };
            for (int s = 0; s < subfolderCount; s++)
            {
                string sub = Path.Combine(genrePath, "Подпапка_" + (s + 1));
                Directory.CreateDirectory(sub);
                targetDirs.Add(sub);
            }

            foreach (var dir in targetDirs)
            {
                int fileCount = rand.Next(minFiles / 2, maxFiles / 2);
                for (int i = 0; i < fileCount; i++)
                {
                    string id = ids[rand.Next(ids.Count)];
                    string ext = Formats[rand.Next(Formats.Length)];
                    string filePath = Path.Combine(dir, id + ext);

                    File.WriteAllBytes(filePath, new byte[] { 0x52, 0x49, 0x46, 0x46 }); 

                    var daysAgo = rand.Next(0, 1000);
                    var date = DateTime.Now.AddDays(-daysAgo);
                    File.SetLastWriteTime(filePath, date);
                }
            }
        }
    }
}
class Value
{
    public static void Run()
    {
        Console.WriteLine("Введите путь для подсчета");
        string path = Console.ReadLine().Trim('"');
        try
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            int total = files.Length;


            for (int i = 0; i < total; i++)
            {
                string file = files[i];
                Console.WriteLine(file);
            }
            Console.WriteLine("\nСписок получен");
            Console.WriteLine($"Найдено файлов: {total}\n");
            Console.WriteLine("Нажмите Enter для выхода...");
            Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
            Console.ReadLine();
        }
    }
}

