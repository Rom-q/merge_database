namespace database_form_ver
{
    partial class SettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            comboBox1 = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            comboBox2 = new ComboBox();
            label4 = new Label();
            comboBox4 = new ComboBox();
            label5 = new Label();
            comboBox3 = new ComboBox();
            label8 = new Label();
            comboBox6 = new ComboBox();
            label9 = new Label();
            comboBox5 = new ComboBox();
            buttonsave = new Button();
            allowExtraFoldersNew = new CheckBox();
            allowExtraFoldersOld = new CheckBox();
            label6 = new Label();
            EnableIdCheck = new CheckBox();
            EnableDateCheck = new CheckBox();
            EnableDuplicateCheck = new CheckBox();
            EnableExtensionCheck = new CheckBox();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox1.Location = new Point(21, 28);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(243, 23);
            comboBox1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(72, 10);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 1;
            label1.Text = "Иерархия форматов";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 12F);
            label2.Location = new Point(-2, 30);
            label2.Name = "label2";
            label2.Size = new Size(22, 21);
            label2.TabIndex = 2;
            label2.Text = "1.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 12F);
            label3.Location = new Point(-2, 54);
            label3.Name = "label3";
            label3.Size = new Size(22, 21);
            label3.TabIndex = 4;
            label3.Text = "2.";
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox2.Location = new Point(21, 52);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new Size(243, 23);
            comboBox2.TabIndex = 3;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 12F);
            label4.Location = new Point(-2, 102);
            label4.Name = "label4";
            label4.Size = new Size(22, 21);
            label4.TabIndex = 8;
            label4.Text = "4.";
            // 
            // comboBox4
            // 
            comboBox4.FormattingEnabled = true;
            comboBox4.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox4.Location = new Point(21, 100);
            comboBox4.Name = "comboBox4";
            comboBox4.Size = new Size(243, 23);
            comboBox4.TabIndex = 7;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 12F);
            label5.Location = new Point(-2, 78);
            label5.Name = "label5";
            label5.Size = new Size(22, 21);
            label5.TabIndex = 6;
            label5.Text = "3.";
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox3.Location = new Point(21, 76);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new Size(243, 23);
            comboBox3.TabIndex = 5;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 12F);
            label8.Location = new Point(-2, 150);
            label8.Name = "label8";
            label8.Size = new Size(22, 21);
            label8.TabIndex = 12;
            label8.Text = "6.";
            // 
            // comboBox6
            // 
            comboBox6.FormattingEnabled = true;
            comboBox6.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox6.Location = new Point(21, 148);
            comboBox6.Name = "comboBox6";
            comboBox6.Size = new Size(243, 23);
            comboBox6.TabIndex = 11;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 12F);
            label9.Location = new Point(-2, 126);
            label9.Name = "label9";
            label9.Size = new Size(22, 21);
            label9.TabIndex = 10;
            label9.Text = "5.";
            // 
            // comboBox5
            // 
            comboBox5.FormattingEnabled = true;
            comboBox5.Items.AddRange(new object[] { ".wav", ".flac", ".alac", ".aac", ".ogg", ".mp3", ".mkv", ".mp4", ".avi", "Другой формат" });
            comboBox5.Location = new Point(21, 124);
            comboBox5.Name = "comboBox5";
            comboBox5.Size = new Size(243, 23);
            comboBox5.TabIndex = 9;
            // 
            // buttonsave
            // 
            buttonsave.Location = new Point(12, 177);
            buttonsave.Name = "buttonsave";
            buttonsave.Size = new Size(99, 70);
            buttonsave.TabIndex = 13;
            buttonsave.Text = "Сохранить";
            buttonsave.UseVisualStyleBackColor = true;
            buttonsave.Click += buttonsave_Click;
            // 
            // allowExtraFoldersNew
            // 
            allowExtraFoldersNew.AutoSize = true;
            allowExtraFoldersNew.Enabled = false;
            allowExtraFoldersNew.Location = new Point(286, 28);
            allowExtraFoldersNew.Name = "allowExtraFoldersNew";
            allowExtraFoldersNew.Size = new Size(216, 19);
            allowExtraFoldersNew.TabIndex = 14;
            allowExtraFoldersNew.Text = "Доп тестовые папки в первой базе";
            allowExtraFoldersNew.UseVisualStyleBackColor = true;
            allowExtraFoldersNew.Visible = false;
            // 
            // allowExtraFoldersOld
            // 
            allowExtraFoldersOld.AutoSize = true;
            allowExtraFoldersOld.Enabled = false;
            allowExtraFoldersOld.Location = new Point(286, 53);
            allowExtraFoldersOld.Name = "allowExtraFoldersOld";
            allowExtraFoldersOld.Size = new Size(222, 19);
            allowExtraFoldersOld.TabIndex = 15;
            allowExtraFoldersOld.Text = "Доп тестовые папки во второй базе";
            allowExtraFoldersOld.UseVisualStyleBackColor = true;
            allowExtraFoldersOld.Visible = false;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(270, 31);
            label6.Name = "label6";
            label6.Size = new Size(10, 150);
            label6.TabIndex = 17;
            label6.Text = "|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n|\r\n\r\n";
            // 
            // EnableIdCheck
            // 
            EnableIdCheck.AutoSize = true;
            EnableIdCheck.Location = new Point(117, 177);
            EnableIdCheck.Name = "EnableIdCheck";
            EnableIdCheck.Size = new Size(126, 19);
            EnableIdCheck.TabIndex = 18;
            EnableIdCheck.Text = "Проверка по айди";
            EnableIdCheck.UseVisualStyleBackColor = true;
            // 
            // EnableDateCheck
            // 
            EnableDateCheck.AutoSize = true;
            EnableDateCheck.Location = new Point(117, 194);
            EnableDateCheck.Name = "EnableDateCheck";
            EnableDateCheck.Size = new Size(148, 19);
            EnableDateCheck.TabIndex = 19;
            EnableDateCheck.Text = "Проверка по времени";
            EnableDateCheck.UseVisualStyleBackColor = true;
            // 
            // EnableDuplicateCheck
            // 
            EnableDuplicateCheck.AutoSize = true;
            EnableDuplicateCheck.Location = new Point(117, 228);
            EnableDuplicateCheck.Name = "EnableDuplicateCheck";
            EnableDuplicateCheck.Size = new Size(218, 19);
            EnableDuplicateCheck.TabIndex = 21;
            EnableDuplicateCheck.Text = "Проверка повторяющихся файлов";
            EnableDuplicateCheck.UseVisualStyleBackColor = true;
            // 
            // EnableExtensionCheck
            // 
            EnableExtensionCheck.AutoSize = true;
            EnableExtensionCheck.Location = new Point(117, 211);
            EnableExtensionCheck.Name = "EnableExtensionCheck";
            EnableExtensionCheck.Size = new Size(158, 19);
            EnableExtensionCheck.TabIndex = 20;
            EnableExtensionCheck.Text = "Проверка по форматам";
            EnableExtensionCheck.UseVisualStyleBackColor = true;
            // 
            // SettingsForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(348, 252);
            Controls.Add(EnableDuplicateCheck);
            Controls.Add(EnableExtensionCheck);
            Controls.Add(EnableDateCheck);
            Controls.Add(EnableIdCheck);
            Controls.Add(label6);
            Controls.Add(allowExtraFoldersOld);
            Controls.Add(allowExtraFoldersNew);
            Controls.Add(buttonsave);
            Controls.Add(label8);
            Controls.Add(comboBox6);
            Controls.Add(label9);
            Controls.Add(comboBox5);
            Controls.Add(label4);
            Controls.Add(comboBox4);
            Controls.Add(label5);
            Controls.Add(comboBox3);
            Controls.Add(label3);
            Controls.Add(comboBox2);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(comboBox1);
            FormBorderStyle = FormBorderStyle.Fixed3D;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "SettingsForm";
            Text = "SettingsForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox comboBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private ComboBox comboBox2;
        private Label label4;
        private ComboBox comboBox4;
        private Label label5;
        private ComboBox comboBox3;
        private Label label8;
        private ComboBox comboBox6;
        private Label label9;
        private ComboBox comboBox5;
        private Button buttonsave;
        private CheckBox allowExtraFoldersNew;
        private CheckBox allowExtraFoldersOld;
        private Label label6;
        private CheckBox EnableIdCheck;
        private CheckBox EnableDateCheck;
        private CheckBox EnableDuplicateCheck;
        private CheckBox EnableExtensionCheck;
    }
}