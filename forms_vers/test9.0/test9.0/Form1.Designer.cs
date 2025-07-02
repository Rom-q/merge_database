namespace database_form_ver
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            merge_button = new Button();
            gen_button = new Button();
            search_button = new Button();
            exit_button = new Button();
            first_label = new Label();
            folderBrowserDialog1 = new FolderBrowserDialog();
            SuspendLayout();
            // 
            // merge_button
            // 
            merge_button.Location = new Point(88, 41);
            merge_button.Margin = new Padding(1, 2, 1, 2);
            merge_button.Name = "merge_button";
            merge_button.Size = new Size(125, 37);
            merge_button.TabIndex = 0;
            merge_button.Text = "Слить базы";
            merge_button.UseVisualStyleBackColor = true;
            merge_button.Click += merge_button_Click;
            // 
            // gen_button
            // 
            gen_button.Location = new Point(88, 82);
            gen_button.Margin = new Padding(1, 2, 1, 2);
            gen_button.Name = "gen_button";
            gen_button.Size = new Size(125, 37);
            gen_button.TabIndex = 1;
            gen_button.Text = "Сгенерировать тестовые базы";
            gen_button.UseVisualStyleBackColor = true;
            gen_button.Click += gen_button_Click;
            // 
            // search_button
            // 
            search_button.Location = new Point(88, 123);
            search_button.Margin = new Padding(1, 2, 1, 2);
            search_button.Name = "search_button";
            search_button.Size = new Size(125, 37);
            search_button.TabIndex = 2;
            search_button.Text = "Подсчет файлов";
            search_button.UseVisualStyleBackColor = true;
            search_button.Click += search_button_Click;
            // 
            // exit_button
            // 
            exit_button.Location = new Point(88, 164);
            exit_button.Margin = new Padding(1, 2, 1, 2);
            exit_button.Name = "exit_button";
            exit_button.Size = new Size(125, 37);
            exit_button.TabIndex = 3;
            exit_button.Text = "Выход";
            exit_button.UseVisualStyleBackColor = true;
            exit_button.Click += exit_button_Click;
            // 
            // first_label
            // 
            first_label.AutoSize = true;
            first_label.BorderStyle = BorderStyle.Fixed3D;
            first_label.Cursor = Cursors.No;
            first_label.Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 204);
            first_label.Location = new Point(12, 9);
            first_label.Name = "first_label";
            first_label.Size = new Size(277, 30);
            first_label.TabIndex = 4;
            first_label.Text = "Объединитель баз данных";
            // 
            // folderBrowserDialog1
            // 
            folderBrowserDialog1.HelpRequest += folderBrowserDialog1_HelpRequest;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 13F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(301, 229);
            Controls.Add(first_label);
            Controls.Add(exit_button);
            Controls.Add(search_button);
            Controls.Add(gen_button);
            Controls.Add(merge_button);
            Font = new Font("Segoe UI", 8F, FontStyle.Bold, GraphicsUnit.Point, 204);
            Margin = new Padding(1, 2, 1, 2);
            Name = "Form1";
            Text = "Соединитель баз";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button merge_button;
        private System.Windows.Forms.Button gen_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Label first_label;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

