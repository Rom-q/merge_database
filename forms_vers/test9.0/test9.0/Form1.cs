using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;

namespace database_form_ver
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void gen_button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string basePath = folderBrowserDialog1.SelectedPath;
                MusicTestBaseGenerator.Run(basePath);
            }
        }

        private void merge_button_Click(object sender, EventArgs e)
        {
            var mergeForm = new MergeForm();
            mergeForm.Show();
        }

        private void search_button_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                Value.Run(path);
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }

        private void Settings_button_Click(object sender, EventArgs e)
        {
            var settingsform = new SettingsForm();
            settingsform.Show();
        }
    }
}
class MusicTestBaseGenerator
{
    static string[] SupportedFormats = { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3" };
    static string[] Genres = {
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

    public static void Run(string basePath)
    {
        string newBase = Path.Combine(basePath, "new_base");
        string oldBase = Path.Combine(basePath, "old_base");

        Directory.CreateDirectory(newBase);
        Directory.CreateDirectory(oldBase);

        var ids = Enumerable.Range(0, 1000).Select(i => $"MUSC_{i / 10:000}_{i % 10:000}").ToList();

        GenerateBase(newBase, ids, 20, 40, allowExtraFolders: false);
        GenerateBase(oldBase, ids, 20, 40, allowExtraFolders: true);
        MessageBox.Show("Базы созданы");
        Process.Start("explorer.exe", basePath);
    }

    static void GenerateBase(string baseDir, List<string> ids, int minFiles, int maxFiles, bool allowExtraFolders)
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
                    string ext = SupportedFormats[rand.Next(SupportedFormats.Length)];
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
    public static void Run(string path)
    {
        try
        {
            string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
            int total = files.Length;
            for (int i = 0; i < total; i++)
            {
                string file = files[i];
            }
            MessageBox.Show($"Всего файлов в папке:" + Environment.NewLine + total);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка: " + ex.Message);
        }
    }
}
