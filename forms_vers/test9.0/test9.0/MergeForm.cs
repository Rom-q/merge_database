using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using database_form_ver;

namespace database_form_ver
{
    public partial class MergeForm : Form
    {
        private CancellationTokenSource? cts; //!

        public MergeForm()
        {
            InitializeComponent();
        }

        private async void buttonmerge_Click(object sender, EventArgs e)
        {
            string newBase = newpath.Text;
            string oldBase = oldpath.Text;
            string mergedBase = updpath.Text;

            if (!Directory.Exists(newBase) || !Directory.Exists(oldBase) || !Directory.Exists(mergedBase))
            {
                MessageBox.Show("Неверный путь");
                return;
            }

            progressBar1.Value = 0;
            progressBar1.Maximum = 100;
            cts = new CancellationTokenSource();

            buttonmerge.Enabled = false;
            buttonmerge.Visible = false;
            buttoncancel.Enabled = true;
            buttoncancel.Visible = true;

            try
            {
                await Task.Run(() =>
                {
                    Merge.Run(newBase, oldBase, mergedBase, (progress) =>
                    {
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Value = progress;
                        }));
                    }, cts.Token);
                });

                if (!cts.Token.IsCancellationRequested)
                {
                    MessageBox.Show("Слияние завершено успешно!");
                    Process.Start("explorer.exe", mergedBase);
                }
                else
                {
                    MessageBox.Show("Слияние отменено пользователем.");
                }
            }
            catch (OperationCanceledException)
            {
                MessageBox.Show("Слияние было отменено.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка:" + Environment.NewLine + ex.Message);
            }
            finally
            {
                buttonmerge.Enabled = true;
                buttonmerge.Visible = true;
                buttoncancel.Enabled = false;
                buttoncancel.Visible = false;
                cts.Dispose();
                cts = null;
            }
        }

        private void newpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                newpath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void oldpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                oldpath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void updpathbutton_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                updpath.Text = folderBrowserDialog1.SelectedPath;
        }

        private void exit_butt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttoncancel_Click_1(object sender, EventArgs e)
        {
            cts?.Cancel();
        }
    }
}
class Merge
{
    static string[] SupportedFormats = AppSettings.SupportedFormats;
    static string[] IgnoredFormats = AppSettings.IgnoredFormats;

    static Dictionary<string, int> FormatPriority = AppSettings.FormatPriority;

    public static void Run(
        string newBase,
        string oldBase,
        string mergedBase,
        Action<int> reportProgress,
        CancellationToken token)
    {
        Directory.CreateDirectory(mergedBase);
        string logDir = Path.Combine(mergedBase, "logs");
        Directory.CreateDirectory(logDir);

        string add = Path.Combine(logDir, "add.txt");
        string change = Path.Combine(logDir, "change.txt");
        string delete = Path.Combine(logDir, "delete.txt");

        File.WriteAllText(add, "=== Добавлено ===" + Environment.NewLine + Environment.NewLine);
        File.WriteAllText(change, "=== Изменено ===" + Environment.NewLine + Environment.NewLine);
        File.WriteAllText(delete, "=== Пропущено ===" + Environment.NewLine + Environment.NewLine);

        var oldFiles = Directory.GetFiles(oldBase, "*.*", SearchOption.AllDirectories)
            .Where(f => IsSupported(f)).ToList();

        var newFiles = Directory.GetFiles(newBase, "*.*", SearchOption.AllDirectories)
            .Where(f => IsSupported(f)).ToList();

        var newFileMap = new Dictionary<string, (string path, string format, DateTime date)>();
        foreach (var nf in newFiles)
        {
            token.ThrowIfCancellationRequested();

            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(nf) : Path.GetFileName(nf); //!
            if (key == null) continue;

            newFileMap[key] = (nf, Path.GetExtension(nf), File.GetLastWriteTime(nf));
        }

        int totalFiles = oldFiles.Count;
        int processed = 0;

        foreach (var oldFile in oldFiles)
        {
            token.ThrowIfCancellationRequested();

            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(oldFile) : Path.GetFileName(oldFile); //!
            if (key == null) continue;

            string ext = Path.GetExtension(oldFile);
            if (!IsSupported(ext)) continue;

            DateTime oldDate = File.GetLastWriteTime(oldFile);

            string relativePath = Path.GetRelativePath(oldBase, oldFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath) ?? mergedBase); //!

            if (newFileMap.TryGetValue(key, out var existing))
            {
                bool shouldReplace = true;

                if (AppSettings.EnableExtensionCheck)
                {
                    int oldPriority = FormatPriority.GetValueOrDefault(ext, 0);
                    int newPriority = FormatPriority.GetValueOrDefault(existing.format, 0);
                    if (oldPriority < newPriority)
                        shouldReplace = false;
                }

                if (AppSettings.EnableDateCheck && oldDate <= existing.date)
                    shouldReplace = false;

                if (shouldReplace)
                {
                    File.Copy(oldFile, mergedPath, true);
                    File.AppendAllText(change, $"[~] Обновлён: {relativePath} (по формату/дате)" + Environment.NewLine);
                }
                else
                {
                    File.AppendAllText(delete, $"[-] Пропущен: {relativePath} (условия не выполнены)" + Environment.NewLine);
                }
            }
            else
            {
                File.Copy(oldFile, mergedPath, true);
                File.AppendAllText(add, $"[+] Добавлен: {relativePath} (из старой базы)" + Environment.NewLine);
            }

            processed++;
            reportProgress?.Invoke((int)((processed / (double)totalFiles) * 100));
        }

        var mergedFileKeys = new HashSet<string?>(
            Directory.GetFiles(mergedBase, "*.*", SearchOption.AllDirectories)
            .Select(f => AppSettings.EnableIdCheck ? ExtractTrackId(f) : Path.GetFileName(f))  //!
            .Where(key => key != null));

        foreach (var newFile in newFiles)
        {
            token.ThrowIfCancellationRequested();

            string ext = Path.GetExtension(newFile);
            if (!IsSupported(ext)) continue;

            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(newFile) : Path.GetFileName(newFile); //!
            if (key == null) continue;

            if (!AppSettings.EnableDuplicateCheck && mergedFileKeys.Contains(key)) continue;

            string relativePath = Path.GetRelativePath(newBase, newFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath) ?? mergedBase); //!

            File.Copy(newFile, mergedPath, true);
            File.AppendAllText(add, $"[+] Добавлен: {relativePath} (из новой базы)" + Environment.NewLine);
        }
    }

    static string? ExtractTrackId(string path) //!
    {
        string fileName = Path.GetFileNameWithoutExtension(path);
        if (fileName.Length >= 12 && fileName[4] == '_' && fileName[8] == '_')
            return fileName.Substring(0, 12);
        return null;
    }

    static bool IsSupported(string pathOrExt)
    {
        string ext = pathOrExt.StartsWith(".") ? pathOrExt : Path.GetExtension(pathOrExt);
        return SupportedFormats.Contains(ext, StringComparer.OrdinalIgnoreCase)
            && !IgnoredFormats.Contains(ext, StringComparer.OrdinalIgnoreCase);
    }
}
