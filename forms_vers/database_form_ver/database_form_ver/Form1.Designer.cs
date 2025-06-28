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
            this.merge_button = new System.Windows.Forms.Button();
            this.gen_button = new System.Windows.Forms.Button();
            this.search_button = new System.Windows.Forms.Button();
            this.exit_button = new System.Windows.Forms.Button();
            this.first_label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // merge_button
            // 
            this.merge_button.Location = new System.Drawing.Point(88, 41);
            this.merge_button.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.merge_button.Name = "merge_button";
            this.merge_button.Size = new System.Drawing.Size(125, 37);
            this.merge_button.TabIndex = 0;
            this.merge_button.Text = "Слить базы";
            this.merge_button.UseVisualStyleBackColor = true;
            // 
            // gen_button
            // 
            this.gen_button.Location = new System.Drawing.Point(88, 82);
            this.gen_button.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.gen_button.Name = "gen_button";
            this.gen_button.Size = new System.Drawing.Size(125, 37);
            this.gen_button.TabIndex = 1;
            this.gen_button.Text = "Сгенерировать тестовые базы";
            this.gen_button.UseVisualStyleBackColor = true;
            // 
            // search_button
            // 
            this.search_button.Location = new System.Drawing.Point(88, 123);
            this.search_button.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.search_button.Name = "search_button";
            this.search_button.Size = new System.Drawing.Size(125, 37);
            this.search_button.TabIndex = 2;
            this.search_button.Text = "Подсчет файлов";
            this.search_button.UseVisualStyleBackColor = true;
            // 
            // exit_button
            // 
            this.exit_button.Location = new System.Drawing.Point(88, 164);
            this.exit_button.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.exit_button.Name = "exit_button";
            this.exit_button.Size = new System.Drawing.Size(125, 37);
            this.exit_button.TabIndex = 3;
            this.exit_button.Text = "Выход";
            this.exit_button.UseVisualStyleBackColor = true;
            this.exit_button.Click += new System.EventHandler(this.exit_button_Click);
            // 
            // first_label
            // 
            this.first_label.AutoSize = true;
            this.first_label.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.first_label.Cursor = System.Windows.Forms.Cursors.No;
            this.first_label.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.first_label.Location = new System.Drawing.Point(12, 9);
            this.first_label.Name = "first_label";
            this.first_label.Size = new System.Drawing.Size(275, 30);
            this.first_label.TabIndex = 4;
            this.first_label.Text = "Обьединитель баз данных";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(301, 229);
            this.Controls.Add(this.first_label);
            this.Controls.Add(this.exit_button);
            this.Controls.Add(this.search_button);
            this.Controls.Add(this.gen_button);
            this.Controls.Add(this.merge_button);
            this.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(1, 2, 1, 2);
            this.Name = "Form1";
            this.Text = "Соединитель баз";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button merge_button;
        private System.Windows.Forms.Button gen_button;
        private System.Windows.Forms.Button search_button;
        private System.Windows.Forms.Button exit_button;
        private System.Windows.Forms.Label first_label;
    }
}

