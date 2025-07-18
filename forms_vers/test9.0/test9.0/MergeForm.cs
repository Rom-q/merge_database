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
                ShowTopmostMessage("Неверный путь", "Ошибка");
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
                    progressBar1.Value = 100;
                    ShowTopmostMessage("Слияние завершено успешно!" + Environment.NewLine + "Логи записанны.", "Готово");
                    Process.Start("explorer.exe", mergedBase);
                }
                else
                {
                    ShowTopmostMessage("Слияние отменено пользователем.", "Отмена");
                }
            }
            catch (OperationCanceledException)
            {
                ShowTopmostMessage("Слияние было отменено.", "Отмена");
            }
            catch (Exception ex)
            {
                ShowTopmostMessage("Произошла ошибка:" + Environment.NewLine + ex.Message, "Ошибка");
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
        private void ShowTopmostMessage(string message, string caption)
        {
            using (Form topmostForm = new Form())
            {
                topmostForm.StartPosition = FormStartPosition.Manual;
                topmostForm.Location = new Point(-10000, -10000); // за экран
                topmostForm.ShowInTaskbar = false;
                topmostForm.TopMost = true;
                topmostForm.Show();
                topmostForm.Focus();
                MessageBox.Show(topmostForm, message, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        string logPath = Path.Combine(logDir, "log.txt");
        string addPath = Path.Combine(logDir, "add.txt");
        string changePath = Path.Combine(logDir, "change.txt");
        string deletePath = Path.Combine(logDir, "delete.txt");

        string NL = Environment.NewLine;

        var fullLog = new List<string>();
        var addLog = new List<string> { "=== ДОБАВЛЕННЫЕ ФАЙЛЫ ===", "" };
        var changeLog = new List<string> { "=== ОБНОВЛЁННЫЕ ФАЙЛЫ ===", "" };
        var deleteLog = new List<string> { "=== ПРОПУЩЕННЫЕ ФАЙЛЫ ===", "" };

        var addedFromNewLines = new List<string>();
        var addedFromOldLines = new List<string>();
        var updatedLines = new List<string>();
        var skippedLines = new List<string>();

        int addedFromNew = 0, addedFromOld = 0, updated = 0, skipped = 0;

        var oldFiles = Directory.GetFiles(oldBase, "*.*", SearchOption.AllDirectories)
            .Where(IsSupported).ToList();

        var newFiles = Directory.GetFiles(newBase, "*.*", SearchOption.AllDirectories)
            .Where(IsSupported).ToList();

        var newFileMap = new Dictionary<string, (string path, string format, DateTime date)>();
        foreach (var nf in newFiles)
        {
            token.ThrowIfCancellationRequested();
            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(nf) : Path.GetFileName(nf);
            if (key == null) continue;
            newFileMap[key] = (nf, Path.GetExtension(nf), File.GetLastWriteTime(nf));
        }

        int totalFiles = oldFiles.Count;
        int processed = 0;

        fullLog.Add("=== ОТЧЁТ О СЛИЯНИИ АУДИОБАЗ ===");
        fullLog.Add("Дата: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
        fullLog.Add("");
        fullLog.Add("Настройки:");
        fullLog.Add($" - Проверка ID: {(AppSettings.EnableIdCheck ? "Включена" : "Отключена")}");
        fullLog.Add($" - Проверка по дате: {(AppSettings.EnableDateCheck ? "Включена" : "Отключена")}");
        fullLog.Add($" - Проверка по формату: {(AppSettings.EnableExtensionCheck ? "Включена" : "Отключена")}");
        fullLog.Add($" - Проверка на дубликаты: {(AppSettings.EnableDuplicateCheck ? "Включена" : "Отключена")}");
        fullLog.Add("");
        fullLog.Add("Базы:");
        fullLog.Add($" - Новая база: {newBase}");
        fullLog.Add($" - Старая база: {oldBase}");
        fullLog.Add($" - Целевая база: {mergedBase}");
        fullLog.Add("");
        fullLog.Add($"Файлов в новой базе: {newFiles.Count}");
        fullLog.Add($"Файлов в старой базе: {oldFiles.Count}");
        fullLog.Add("");
        fullLog.Add("------------------------------------------------------------");

        foreach (var oldFile in oldFiles)
        {
            token.ThrowIfCancellationRequested();

            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(oldFile) : Path.GetFileName(oldFile);
            if (key == null) continue;

            string ext = Path.GetExtension(oldFile);
            if (!IsSupported(ext)) continue;

            DateTime oldDate = File.GetLastWriteTime(oldFile);
            string relativePath = Path.GetRelativePath(oldBase, oldFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath) ?? mergedBase);

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
                    string msg = $" - {relativePath} → заменён (формат: {ext} < {existing.format}, дата: {oldDate:yyyy-MM-dd} < {existing.date:yyyy-MM-dd})";
                    updatedLines.Add(msg);
                    changeLog.Add(msg);
                    updated++;
                }
                else
                {
                    string msg = $" - {relativePath} → пропущен (условия не выполнены)";
                    skippedLines.Add(msg);
                    deleteLog.Add(msg);
                    skipped++;
                }
            }
            else
            {
                File.Copy(oldFile, mergedPath, true);
                string msg = $" - {relativePath} (из второй базы)";
                addedFromOldLines.Add(msg);
                addLog.Add(msg);
                addedFromOld++;
            }

            processed++;
            reportProgress?.Invoke((int)((processed / (double)totalFiles) * 100));
        }

        var mergedFileKeys = new HashSet<string?>(
            Directory.GetFiles(mergedBase, "*.*", SearchOption.AllDirectories)
            .Select(f => AppSettings.EnableIdCheck ? ExtractTrackId(f) : Path.GetFileName(f))
            .Where(key => key != null));

        foreach (var newFile in newFiles)
        {
            token.ThrowIfCancellationRequested();

            string ext = Path.GetExtension(newFile);
            if (!IsSupported(ext)) continue;

            string? key = AppSettings.EnableIdCheck ? ExtractTrackId(newFile) : Path.GetFileName(newFile);
            if (key == null) continue;

            if (!AppSettings.EnableDuplicateCheck && mergedFileKeys.Contains(key)) continue;

            string relativePath = Path.GetRelativePath(newBase, newFile);
            string mergedPath = Path.Combine(mergedBase, relativePath);
            Directory.CreateDirectory(Path.GetDirectoryName(mergedPath) ?? mergedBase);

            File.Copy(newFile, mergedPath, true);
            string msg = $" - {relativePath} (из главной базы)";
            addedFromNewLines.Add(msg);
            addLog.Add(msg);
            addedFromNew++;
        }

        fullLog.Add("");
        fullLog.Add($"[+] ДОБАВЛЕНО ИЗ ВТОРОЙ БАЗЫ ({addedFromOld}):");
        fullLog.AddRange(addedFromOldLines);
        fullLog.Add("");

        fullLog.Add($"[+] ДОБАВЛЕНО ИЗ ГЛАВНОЙ БАЗЫ ({addedFromNew}):");
        fullLog.AddRange(addedFromNewLines);
        fullLog.Add("");

        fullLog.Add($"[~] ОБНОВЛЕНО ({updated}):");
        fullLog.AddRange(updatedLines);
        fullLog.Add("");

        fullLog.Add($"[-] ПРОПУЩЕНО ({skipped}):");
        fullLog.AddRange(skippedLines);
        fullLog.Add("");

        fullLog.Add("------------------------------------------------------------");
        fullLog.Add("");
        fullLog.Add("ИТОГИ:");
        fullLog.Add($" - Добавлено из главной базы: {addedFromNew}");
        fullLog.Add($" - Добавлено из второй базы: {addedFromOld}");
        fullLog.Add($" - Обновлено: {updated}");
        fullLog.Add($" - Пропущено: {skipped}");
        fullLog.Add("");
        fullLog.Add("Слияние завершено успешно.");

        File.WriteAllText(logPath, string.Join(NL, fullLog));
        File.WriteAllText(addPath, string.Join(NL, addLog));
        File.WriteAllText(changePath, string.Join(NL, changeLog));
        File.WriteAllText(deletePath, string.Join(NL, deleteLog));
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
