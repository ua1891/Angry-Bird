namespace LaeeqFramwork
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.Main = new System.Windows.Forms.Timer(this.components);
            this.Score = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.HighScore = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ScoreHigh = new System.Windows.Forms.Label();
            this.LevelPoints = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Main
            // 
            this.Main.Tick += new System.EventHandler(this.Main_Tick);
            // 
            // Score
            // 
            this.Score.AutoSize = true;
            this.Score.Location = new System.Drawing.Point(873, 28);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(0, 20);
            this.Score.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(900, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 1;
            // 
            // HighScore
            // 
            this.HighScore.AutoSize = true;
            this.HighScore.Location = new System.Drawing.Point(907, 13);
            this.HighScore.Name = "HighScore";
            this.HighScore.Size = new System.Drawing.Size(0, 20);
            this.HighScore.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(789, 30);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(115, 24);
            this.label2.TabIndex = 3;
            this.label2.Text = "HighScore";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(822, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 24);
            this.label3.TabIndex = 4;
            this.label3.Text = "Score";
            // 
            // ScoreHigh
            // 
            this.ScoreHigh.AutoSize = true;
            this.ScoreHigh.BackColor = System.Drawing.Color.Transparent;
            this.ScoreHigh.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreHigh.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ScoreHigh.Location = new System.Drawing.Point(913, 30);
            this.ScoreHigh.Name = "ScoreHigh";
            this.ScoreHigh.Size = new System.Drawing.Size(0, 24);
            this.ScoreHigh.TabIndex = 5;
            // 
            // LevelPoints
            // 
            this.LevelPoints.AutoSize = true;
            this.LevelPoints.BackColor = System.Drawing.Color.Transparent;
            this.LevelPoints.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LevelPoints.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.LevelPoints.Location = new System.Drawing.Point(923, 65);
            this.LevelPoints.Name = "LevelPoints";
            this.LevelPoints.Size = new System.Drawing.Size(0, 24);
            this.LevelPoints.TabIndex = 6;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(0)))));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Modern No. 20", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(12, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(96, 38);
            this.button1.TabIndex = 7;
            this.button1.TabStop = false;
            this.button1.Text = "Exit";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::LaeeqFramwork.Properties.Resources.Bg;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1593, 677);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LevelPoints);
            this.Controls.Add(this.ScoreHigh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.HighScore);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Score);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Timer Main;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label HighScore;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label ScoreHigh;
        private System.Windows.Forms.Label LevelPoints;
        private System.Windows.Forms.Button button1;
    }
}

