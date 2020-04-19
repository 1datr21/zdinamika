namespace zdinamika
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
            this.btnFolder = new System.Windows.Forms.Button();
            this.tbFolder = new System.Windows.Forms.TextBox();
            this.dgvTests = new System.Windows.Forms.DataGridView();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.TObject = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StartTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StopTime = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Scheme = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Interval = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).BeginInit();
            this.SuspendLayout();
            // 
            // btnFolder
            // 
            this.btnFolder.Location = new System.Drawing.Point(359, 10);
            this.btnFolder.Name = "btnFolder";
            this.btnFolder.Size = new System.Drawing.Size(98, 23);
            this.btnFolder.TabIndex = 0;
            this.btnFolder.Text = "Выбрать папку";
            this.btnFolder.UseVisualStyleBackColor = true;
            this.btnFolder.Click += new System.EventHandler(this.button1_Click);
            // 
            // tbFolder
            // 
            this.tbFolder.Location = new System.Drawing.Point(13, 13);
            this.tbFolder.Name = "tbFolder";
            this.tbFolder.Size = new System.Drawing.Size(340, 20);
            this.tbFolder.TabIndex = 1;
            // 
            // dgvTests
            // 
            this.dgvTests.AllowUserToAddRows = false;
            this.dgvTests.AllowUserToDeleteRows = false;
            this.dgvTests.AllowUserToResizeRows = false;
            this.dgvTests.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvTests.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTests.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TObject,
            this.StartTime,
            this.StopTime,
            this.Scheme,
            this.Interval});
            this.dgvTests.Location = new System.Drawing.Point(13, 50);
            this.dgvTests.Name = "dgvTests";
            this.dgvTests.Size = new System.Drawing.Size(751, 257);
            this.dgvTests.TabIndex = 2;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Location = new System.Drawing.Point(0, 316);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(773, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // TObject
            // 
            this.TObject.HeaderText = "Имя объекта";
            this.TObject.Name = "TObject";
            this.TObject.Width = 250;
            // 
            // StartTime
            // 
            this.StartTime.HeaderText = "Время старта";
            this.StartTime.Name = "StartTime";
            // 
            // StopTime
            // 
            this.StopTime.HeaderText = "Время остановки";
            this.StopTime.Name = "StopTime";
            // 
            // Scheme
            // 
            this.Scheme.HeaderText = "Схема проверки";
            this.Scheme.Name = "Scheme";
            // 
            // Interval
            // 
            this.Interval.HeaderText = "Интервал измерения";
            this.Interval.Name = "Interval";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(773, 338);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.dgvTests);
            this.Controls.Add(this.tbFolder);
            this.Controls.Add(this.btnFolder);
            this.Name = "Form1";
            this.Text = "НПП \"Динамика\"";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTests)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFolder;
        private System.Windows.Forms.TextBox tbFolder;
        private System.Windows.Forms.DataGridView dgvTests;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.DataGridViewTextBoxColumn TObject;
        private System.Windows.Forms.DataGridViewTextBoxColumn StartTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn StopTime;
        private System.Windows.Forms.DataGridViewTextBoxColumn Scheme;
        private System.Windows.Forms.DataGridViewTextBoxColumn Interval;
    }
}

