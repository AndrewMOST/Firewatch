namespace Kursach
{
    partial class Firewatch
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
            this.components = new System.ComponentModel.Container();
            this.SelectImages = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.ConnectTimer = new System.Windows.Forms.Timer(this.components);
            this.PleaseWait = new System.Windows.Forms.Label();
            this.IfMulticlass = new System.Windows.Forms.CheckBox();
            this.IfSingle = new System.Windows.Forms.CheckBox();
            this.WaitForPred = new System.Windows.Forms.Label();
            this.Frame1 = new System.Windows.Forms.Button();
            this.UploadWeights = new System.Windows.Forms.Button();
            this.Frame2 = new System.Windows.Forms.Button();
            this.SettingsLabel = new System.Windows.Forms.Label();
            this.BackBut = new System.Windows.Forms.Button();
            this.weightUpload = new System.Windows.Forms.OpenFileDialog();
            this.SaveLogs = new System.Windows.Forms.CheckBox();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.Pics = new System.Windows.Forms.PictureBox();
            this.ViewImages = new System.Windows.Forms.Button();
            this.Prev = new System.Windows.Forms.Button();
            this.Next = new System.Windows.Forms.Button();
            this.LoadImage = new System.Windows.Forms.Button();
            this.PredClass = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Pics)).BeginInit();
            this.SuspendLayout();
            // 
            // SelectImages
            // 
            this.SelectImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SelectImages.Location = new System.Drawing.Point(9, 10);
            this.SelectImages.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.SelectImages.Name = "SelectImages";
            this.SelectImages.Size = new System.Drawing.Size(308, 83);
            this.SelectImages.TabIndex = 0;
            this.SelectImages.Text = "Classify Images";
            this.SelectImages.UseVisualStyleBackColor = true;
            this.SelectImages.Visible = false;
            this.SelectImages.Click += new System.EventHandler(this.SelectImages_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.InitialDirectory = "C:\\Users\\amost\\DesktopKursach";
            this.openFileDialog1.Multiselect = true;
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select Folder For Analysis";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // ConnectTimer
            // 
            this.ConnectTimer.Interval = 500;
            this.ConnectTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // PleaseWait
            // 
            this.PleaseWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PleaseWait.Location = new System.Drawing.Point(94, 0);
            this.PleaseWait.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.PleaseWait.Name = "PleaseWait";
            this.PleaseWait.Size = new System.Drawing.Size(474, 340);
            this.PleaseWait.TabIndex = 5;
            this.PleaseWait.Text = "Please wait for connection to the server";
            this.PleaseWait.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // IfMulticlass
            // 
            this.IfMulticlass.AutoSize = true;
            this.IfMulticlass.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IfMulticlass.Location = new System.Drawing.Point(18, 228);
            this.IfMulticlass.Margin = new System.Windows.Forms.Padding(2);
            this.IfMulticlass.Name = "IfMulticlass";
            this.IfMulticlass.Size = new System.Drawing.Size(361, 24);
            this.IfMulticlass.TabIndex = 1;
            this.IfMulticlass.Text = "Use Multiclass Classification (Binary by Default)";
            this.IfMulticlass.UseVisualStyleBackColor = true;
            this.IfMulticlass.Visible = false;
            this.IfMulticlass.CheckedChanged += new System.EventHandler(this.IfMulticlass_CheckedChanged);
            // 
            // IfSingle
            // 
            this.IfSingle.AutoSize = true;
            this.IfSingle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.IfSingle.Location = new System.Drawing.Point(18, 200);
            this.IfSingle.Margin = new System.Windows.Forms.Padding(2);
            this.IfSingle.Name = "IfSingle";
            this.IfSingle.Size = new System.Drawing.Size(321, 24);
            this.IfSingle.TabIndex = 7;
            this.IfSingle.Text = "Select Single Images (Folders by Default)";
            this.IfSingle.UseVisualStyleBackColor = true;
            this.IfSingle.Visible = false;
            this.IfSingle.CheckedChanged += new System.EventHandler(this.IfSingle_CheckedChanged);
            // 
            // WaitForPred
            // 
            this.WaitForPred.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.WaitForPred.Location = new System.Drawing.Point(94, 0);
            this.WaitForPred.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.WaitForPred.Name = "WaitForPred";
            this.WaitForPred.Size = new System.Drawing.Size(474, 340);
            this.WaitForPred.TabIndex = 9;
            this.WaitForPred.Text = "Please wait: the images are being processed";
            this.WaitForPred.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.WaitForPred.Visible = false;
            // 
            // Frame1
            // 
            this.Frame1.Enabled = false;
            this.Frame1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Frame1.Location = new System.Drawing.Point(9, 105);
            this.Frame1.Margin = new System.Windows.Forms.Padding(2);
            this.Frame1.Name = "Frame1";
            this.Frame1.Size = new System.Drawing.Size(644, 235);
            this.Frame1.TabIndex = 10;
            this.Frame1.UseVisualStyleBackColor = true;
            this.Frame1.Visible = false;
            // 
            // UploadWeights
            // 
            this.UploadWeights.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.UploadWeights.Location = new System.Drawing.Point(334, 10);
            this.UploadWeights.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.UploadWeights.Name = "UploadWeights";
            this.UploadWeights.Size = new System.Drawing.Size(318, 83);
            this.UploadWeights.TabIndex = 11;
            this.UploadWeights.Text = "Upload Weights";
            this.UploadWeights.UseVisualStyleBackColor = true;
            this.UploadWeights.Visible = false;
            this.UploadWeights.Click += new System.EventHandler(this.UploadWeights_Click);
            // 
            // Frame2
            // 
            this.Frame2.Enabled = false;
            this.Frame2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Frame2.Font = new System.Drawing.Font("Microsoft Sans Serif", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Frame2.Location = new System.Drawing.Point(9, 105);
            this.Frame2.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Frame2.Name = "Frame2";
            this.Frame2.Size = new System.Drawing.Size(644, 83);
            this.Frame2.TabIndex = 12;
            this.Frame2.UseVisualStyleBackColor = true;
            this.Frame2.Visible = false;
            // 
            // SettingsLabel
            // 
            this.SettingsLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SettingsLabel.Location = new System.Drawing.Point(13, 109);
            this.SettingsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.SettingsLabel.Name = "SettingsLabel";
            this.SettingsLabel.Size = new System.Drawing.Size(636, 75);
            this.SettingsLabel.TabIndex = 13;
            this.SettingsLabel.Text = "Settings";
            this.SettingsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.SettingsLabel.Visible = false;
            // 
            // BackBut
            // 
            this.BackBut.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.BackBut.Location = new System.Drawing.Point(518, 257);
            this.BackBut.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.BackBut.Name = "BackBut";
            this.BackBut.Size = new System.Drawing.Size(135, 83);
            this.BackBut.TabIndex = 14;
            this.BackBut.Text = "Back";
            this.BackBut.UseVisualStyleBackColor = true;
            this.BackBut.Visible = false;
            this.BackBut.Click += new System.EventHandler(this.BackBut_Click);
            // 
            // weightUpload
            // 
            this.weightUpload.FileName = "weights";
            // 
            // SaveLogs
            // 
            this.SaveLogs.AutoSize = true;
            this.SaveLogs.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveLogs.Location = new System.Drawing.Point(18, 257);
            this.SaveLogs.Margin = new System.Windows.Forms.Padding(2);
            this.SaveLogs.Name = "SaveLogs";
            this.SaveLogs.Size = new System.Drawing.Size(200, 24);
            this.SaveLogs.TabIndex = 15;
            this.SaveLogs.Text = "Save Classification Logs";
            this.SaveLogs.UseVisualStyleBackColor = true;
            this.SaveLogs.Visible = false;
            this.SaveLogs.CheckedChanged += new System.EventHandler(this.SaveLogs_CheckedChanged);
            // 
            // SaveSettings
            // 
            this.SaveSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.SaveSettings.ForeColor = System.Drawing.SystemColors.ControlText;
            this.SaveSettings.Location = new System.Drawing.Point(429, 257);
            this.SaveSettings.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(224, 83);
            this.SaveSettings.TabIndex = 16;
            this.SaveSettings.Text = "Save Settings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Visible = false;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // Pics
            // 
            this.Pics.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Pics.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Pics.Location = new System.Drawing.Point(9, 56);
            this.Pics.Name = "Pics";
            this.Pics.Size = new System.Drawing.Size(497, 284);
            this.Pics.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Pics.TabIndex = 17;
            this.Pics.TabStop = false;
            this.Pics.Visible = false;
            this.Pics.WaitOnLoad = true;
            // 
            // ViewImages
            // 
            this.ViewImages.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.ViewImages.Location = new System.Drawing.Point(9, 257);
            this.ViewImages.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.ViewImages.Name = "ViewImages";
            this.ViewImages.Size = new System.Drawing.Size(318, 83);
            this.ViewImages.TabIndex = 18;
            this.ViewImages.Text = "View Images";
            this.ViewImages.UseVisualStyleBackColor = true;
            this.ViewImages.Visible = false;
            this.ViewImages.Click += new System.EventHandler(this.ViewImages_Click);
            // 
            // Prev
            // 
            this.Prev.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Prev.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Prev.Location = new System.Drawing.Point(518, 10);
            this.Prev.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Prev.Name = "Prev";
            this.Prev.Size = new System.Drawing.Size(61, 83);
            this.Prev.TabIndex = 21;
            this.Prev.UseVisualStyleBackColor = true;
            this.Prev.Visible = false;
            this.Prev.Click += new System.EventHandler(this.Prev_Click);
            // 
            // Next
            // 
            this.Next.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.Next.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Next.Location = new System.Drawing.Point(592, 10);
            this.Next.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.Next.Name = "Next";
            this.Next.Size = new System.Drawing.Size(61, 83);
            this.Next.TabIndex = 22;
            this.Next.UseVisualStyleBackColor = true;
            this.Next.Visible = false;
            this.Next.Click += new System.EventHandler(this.Next_Click);
            // 
            // LoadImage
            // 
            this.LoadImage.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.LoadImage.Location = new System.Drawing.Point(518, 105);
            this.LoadImage.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.LoadImage.Name = "LoadImage";
            this.LoadImage.Size = new System.Drawing.Size(135, 83);
            this.LoadImage.TabIndex = 23;
            this.LoadImage.Text = "Load";
            this.LoadImage.UseVisualStyleBackColor = true;
            this.LoadImage.Visible = false;
            this.LoadImage.Click += new System.EventHandler(this.LoadImage_Click);
            // 
            // PredClass
            // 
            this.PredClass.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.PredClass.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.PredClass.Location = new System.Drawing.Point(9, 10);
            this.PredClass.Name = "PredClass";
            this.PredClass.Size = new System.Drawing.Size(497, 48);
            this.PredClass.TabIndex = 24;
            this.PredClass.Text = "Error";
            this.PredClass.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.PredClass.Visible = false;
            // 
            // Firewatch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(662, 349);
            this.Controls.Add(this.PredClass);
            this.Controls.Add(this.SettingsLabel);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.LoadImage);
            this.Controls.Add(this.Next);
            this.Controls.Add(this.Prev);
            this.Controls.Add(this.BackBut);
            this.Controls.Add(this.SaveLogs);
            this.Controls.Add(this.UploadWeights);
            this.Controls.Add(this.IfSingle);
            this.Controls.Add(this.IfMulticlass);
            this.Controls.Add(this.SelectImages);
            this.Controls.Add(this.PleaseWait);
            this.Controls.Add(this.ViewImages);
            this.Controls.Add(this.Frame2);
            this.Controls.Add(this.Pics);
            this.Controls.Add(this.Frame1);
            this.Controls.Add(this.WaitForPred);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximizeBox = false;
            this.Name = "Firewatch";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Firewatch";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.Pics)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SelectImages;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Timer ConnectTimer;
        private System.Windows.Forms.Label PleaseWait;
        private System.Windows.Forms.CheckBox IfMulticlass;
        private System.Windows.Forms.CheckBox IfSingle;
        private System.Windows.Forms.Label WaitForPred;
        private System.Windows.Forms.Button Frame1;
        private System.Windows.Forms.Button UploadWeights;
        private System.Windows.Forms.Button Frame2;
        private System.Windows.Forms.Label SettingsLabel;
        private System.Windows.Forms.Button BackBut;
        private System.Windows.Forms.OpenFileDialog weightUpload;
        private System.Windows.Forms.CheckBox SaveLogs;
        private System.Windows.Forms.Button SaveSettings;
        private System.Windows.Forms.PictureBox Pics;
        private System.Windows.Forms.Button ViewImages;
        private System.Windows.Forms.Button Prev;
        private System.Windows.Forms.Button Next;
        private System.Windows.Forms.Button LoadImage;
        private System.Windows.Forms.Label PredClass;
    }
}

