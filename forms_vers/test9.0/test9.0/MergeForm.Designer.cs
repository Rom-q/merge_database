namespace database_form_ver
{
    partial class MergeForm
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
            folderBrowserDialog1 = new FolderBrowserDialog();
            newpathbutton = new Button();
            newpath = new TextBox();
            oldpath = new TextBox();
            oldpathbutton = new Button();
            updpath = new TextBox();
            updpathbutton = new Button();
            buttonmerge = new Button();
            progressBar1 = new ProgressBar();
            exit_butt = new Button();
            SuspendLayout();
            // 
            // newpathbutton
            // 
            newpathbutton.BackColor = SystemColors.ActiveCaptionText;
            newpathbutton.Location = new Point(12, 11);
            newpathbutton.Name = "newpathbutton";
            newpathbutton.Size = new Size(28, 23);
            newpathbutton.TabIndex = 0;
            newpathbutton.UseVisualStyleBackColor = false;
            newpathbutton.Click += newpathbutton_Click;
            // 
            // newpath
            // 
            newpath.Location = new Point(46, 11);
            newpath.Name = "newpath";
            newpath.Size = new Size(166, 23);
            newpath.TabIndex = 1;
            newpath.Text = "путь к новой базе";
            // 
            // oldpath
            // 
            oldpath.Location = new Point(46, 40);
            oldpath.Name = "oldpath";
            oldpath.Size = new Size(166, 23);
            oldpath.TabIndex = 3;
            oldpath.Text = "путь к старой базе";
            // 
            // oldpathbutton
            // 
            oldpathbutton.BackColor = SystemColors.ActiveCaptionText;
            oldpathbutton.Location = new Point(12, 40);
            oldpathbutton.Name = "oldpathbutton";
            oldpathbutton.Size = new Size(28, 23);
            oldpathbutton.TabIndex = 2;
            oldpathbutton.UseVisualStyleBackColor = false;
            oldpathbutton.Click += oldpathbutton_Click;
            // 
            // updpath
            // 
            updpath.Location = new Point(46, 69);
            updpath.Name = "updpath";
            updpath.Size = new Size(166, 23);
            updpath.TabIndex = 5;
            updpath.Text = "путь к объединенной базе";
            // 
            // updpathbutton
            // 
            updpathbutton.BackColor = SystemColors.ActiveCaptionText;
            updpathbutton.Location = new Point(12, 69);
            updpathbutton.Name = "updpathbutton";
            updpathbutton.Size = new Size(28, 23);
            updpathbutton.TabIndex = 4;
            updpathbutton.UseVisualStyleBackColor = false;
            updpathbutton.Click += updpathbutton_Click;
            // 
            // buttonmerge
            // 
            buttonmerge.Location = new Point(230, 11);
            buttonmerge.Name = "buttonmerge";
            buttonmerge.Size = new Size(97, 81);
            buttonmerge.TabIndex = 6;
            buttonmerge.Text = "ОБЪЕДЕНИТЬ";
            buttonmerge.UseVisualStyleBackColor = true;
            buttonmerge.Click += buttonmerge_Click;
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(12, 98);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(331, 28);
            progressBar1.TabIndex = 7;
            // 
            // exit_butt
            // 
            exit_butt.Location = new Point(12, 132);
            exit_butt.Name = "exit_butt";
            exit_butt.Size = new Size(331, 28);
            exit_butt.TabIndex = 8;
            exit_butt.Text = "Выход";
            exit_butt.UseVisualStyleBackColor = true;
            exit_butt.Click += exit_butt_Click;
            // 
            // MergeForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(355, 166);
            Controls.Add(exit_butt);
            Controls.Add(progressBar1);
            Controls.Add(buttonmerge);
            Controls.Add(updpath);
            Controls.Add(updpathbutton);
            Controls.Add(oldpath);
            Controls.Add(oldpathbutton);
            Controls.Add(newpath);
            Controls.Add(newpathbutton);
            Margin = new Padding(4, 3, 4, 3);
            Name = "MergeForm";
            Text = "Меню обьединеения";
            ResumeLayout(false);
            PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Button newpathbutton;
        private TextBox newpath;
        private TextBox oldpath;
        private Button oldpathbutton;
        private TextBox updpath;
        private Button updpathbutton;
        private Button buttonmerge;
        public ProgressBar progressBar1;
        private Button exit_butt;
    }
}