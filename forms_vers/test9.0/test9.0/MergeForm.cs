using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace database_form_ver
{
    public partial class MergeForm : Form
    {
        public MergeForm()
        {
            InitializeComponent();
        }

        private void buttonmerge_Click(object sender, EventArgs e)
        {
            string newBase = newpath.Text;
            string oldBase = oldpath.Text;
            string mergedBase = updpath.Text;
            if (Directory.Exists(newBase) && Directory.Exists(oldBase) && Directory.Exists(mergedBase))
            {
                Merge.Run(newBase, oldBase, mergedBase, (progress) =>
                {
                    progressBar1.Invoke(new Action(() =>
                    {
                        progressBar1.Value = progress;
                    }));
                });
            }
            else
            {
                MessageBox.Show("Неверный путь");
            }
        }

        private void newpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                newpath.Text = path;
            }
        }

        private void oldpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                oldpath.Text = path;
            }
        }

        private void updpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = folderBrowserDialog1.SelectedPath;
                updpath.Text = path;
            }
        }

        private void exit_butt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    class Merge
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
        public static void Run(string newBase, string oldBase, string mergedBase, Action<int> reportProgress)
        {

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
                reportProgress?.Invoke((int)((processed / (double)totalFiles) * 100));
            }

            var mergedFileIds = new HashSet<string>(
            Directory.GetFiles(mergedBase, "*.*", SearchOption.AllDirectories)
            .Select(f => ExtractTrackId(f))
            .Where(id => id != null));
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

            MessageBox.Show("Слияние завершенно\nИзменения записанны в логи");
        }

        static string ExtractTrackId(string path)
        {
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (fileName.Length >= 12 && fileName[4] == '_' && fileName[8] == '_')
                return fileName.Substring(0, 12);
            return null;
        }
    }
}