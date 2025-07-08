using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using test9._0;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace database_form_ver
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
            foreach (var cb in new[] { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6 })
            {
                cb.DropDownStyle = ComboBoxStyle.DropDown;
                cb.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                cb.AutoCompleteSource = AutoCompleteSource.ListItems;
                cb.Items.AddRange(AppSettings.SupportedFormats1);
            }
            var formats = AppSettings.SupportedFormats;
            var cbList = new[] { comboBox1, comboBox2, comboBox3, comboBox4, comboBox5, comboBox6 };

            for (int i = 0; i < cbList.Length; i++)
            {
                cbList[i].Text = (i < formats.Length) ? formats[i] : "";
            }
            allowExtraFoldersNew.Checked = AppSettings.allowExtraFoldersNew;
            allowExtraFoldersOld.Checked = AppSettings.allowExtraFoldersOld;
            EnableIdCheck.Checked = AppSettings.EnableIdCheck;
            EnableExtensionCheck.Checked = AppSettings.EnableExtensionCheck;
            EnableDuplicateCheck.Checked = AppSettings.EnableDuplicateCheck;
            EnableDateCheck.Checked = AppSettings.EnableDateCheck;
        }


        private void buttonsave_Click(object sender, EventArgs e)
        {
            string[] comboBoxes = {
                comboBox1.Text.Trim().ToLower(),
                comboBox2.Text.Trim().ToLower(),
                comboBox3.Text.Trim().ToLower(),
                comboBox4.Text.Trim().ToLower(),
                comboBox5.Text.Trim().ToLower(),
                comboBox6.Text.Trim().ToLower()
            };
            var cleanedFormats = comboBoxes
                .Where(f => !AppSettings.IgnoredFormats.Contains(f))
                .Distinct()
                .ToArray();
            bool allValid = comboBoxes.All(format =>
                string.IsNullOrWhiteSpace(format) ||
                format == "none" ||
                format.StartsWith(".")
            );
            if (!allValid)
            {
                MessageBox.Show("Проверьте правильность написания форматов (допускаются none и пустое поле)" + Environment.NewLine + "Ошибка ввода");
            }
            else
            {
                AppSettings.SupportedFormats = cleanedFormats;

                AppSettings.FormatPriority = AppSettings.SupportedFormats
                    .Select((ext, i) => new { ext, priority = AppSettings.SupportedFormats.Length - i })
                    .ToDictionary(x => x.ext, x => x.priority, StringComparer.OrdinalIgnoreCase);
                //AppSettings.SupportedFormats = comboBoxes.Distinct().ToArray();
                AppSettings.allowExtraFoldersOld = allowExtraFoldersOld.Checked;
                AppSettings.allowExtraFoldersNew = allowExtraFoldersNew.Checked;
                AppSettings.EnableIdCheck = EnableIdCheck.Checked;
                AppSettings.EnableExtensionCheck = EnableExtensionCheck.Checked;
                AppSettings.EnableDuplicateCheck = EnableDuplicateCheck.Checked;
                AppSettings.EnableDateCheck = EnableDateCheck.Checked;
                MessageBox.Show("Изменения успешно сохранены" + Environment.NewLine + "Успешно");
                this.Close();
            }
        }
    }
}
