using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace database_form_ver
{
    public class AppSettings
    {
        public static Dictionary<string, int> FormatPriority = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            [".wav"] = 6,
            [".flac"] = 5,
            [".alac"] = 4,
            [".aac"] = 3,
            [".ogg"] = 2,
            [".mp3"] = 1,
        };
        public static string[] Genres = 
        {
            "Эпическая музыка",
            "Медитативная",
            "Напряжённая",
            "Спокойная",
            "Мрачная",
            "Космическая",
            "Военная",
            "Эмоциональная"
        };
        static public bool allowExtraFoldersNew = false;
        static public bool allowExtraFoldersOld = true;
        public static String[] SupportedFormats = { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3" };
        public static String[] SupportedFormats1 = { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", ".ts", "none", "Введите свой"};
        public static bool EnableIdCheck = true;
        public static bool EnableExtensionCheck = true;
        public static bool EnableDuplicateCheck = true;
        public static bool EnableDateCheck = true;
        public static string[] IgnoredFormats = { "none", "" };
    }
}