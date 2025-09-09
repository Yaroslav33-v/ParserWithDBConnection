namespace ParserWithDBConnection
{
    partial class Parser
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
            this.buttonParse = new System.Windows.Forms.Button();
            this.checkedListBoxSites = new System.Windows.Forms.CheckedListBox();
            this.buttonCopyToDB = new System.Windows.Forms.Button();
            this.textBoxInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // buttonParse
            // 
            this.buttonParse.Location = new System.Drawing.Point(772, 341);
            this.buttonParse.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonParse.Name = "buttonParse";
            this.buttonParse.Size = new System.Drawing.Size(232, 91);
            this.buttonParse.TabIndex = 0;
            this.buttonParse.Text = "Получить данные";
            this.buttonParse.UseVisualStyleBackColor = true;
            this.buttonParse.Click += new System.EventHandler(this.buttonParse_Click);
            // 
            // checkedListBoxSites
            // 
            this.checkedListBoxSites.FormattingEnabled = true;
            this.checkedListBoxSites.Items.AddRange(new object[] {
            "Основной рынок - акции",
            "Биржевые фонды",
            "Гарантии",
            "Индексы Австрии"});
            this.checkedListBoxSites.Location = new System.Drawing.Point(772, 44);
            this.checkedListBoxSites.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.checkedListBoxSites.Name = "checkedListBoxSites";
            this.checkedListBoxSites.Size = new System.Drawing.Size(232, 92);
            this.checkedListBoxSites.TabIndex = 1;
            this.checkedListBoxSites.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.checkedListBoxSites_ItemCheck);
            // 
            // buttonCopyToDB
            // 
            this.buttonCopyToDB.Location = new System.Drawing.Point(772, 476);
            this.buttonCopyToDB.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.buttonCopyToDB.Name = "buttonCopyToDB";
            this.buttonCopyToDB.Size = new System.Drawing.Size(232, 91);
            this.buttonCopyToDB.TabIndex = 2;
            this.buttonCopyToDB.Text = "Копировать данные в БД";
            this.buttonCopyToDB.UseVisualStyleBackColor = true;
            this.buttonCopyToDB.Click += new System.EventHandler(this.buttonCopyToDB_Click);
            // 
            // textBoxInfo
            // 
            this.textBoxInfo.Location = new System.Drawing.Point(58, 44);
            this.textBoxInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.textBoxInfo.Multiline = true;
            this.textBoxInfo.Name = "textBoxInfo";
            this.textBoxInfo.Size = new System.Drawing.Size(502, 523);
            this.textBoxInfo.TabIndex = 3;
            // 
            // Parser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Info;
            this.ClientSize = new System.Drawing.Size(1100, 652);
            this.Controls.Add(this.textBoxInfo);
            this.Controls.Add(this.buttonCopyToDB);
            this.Controls.Add(this.checkedListBoxSites);
            this.Controls.Add(this.buttonParse);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Parser";
            this.Text = "Парсер";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonParse;
        private System.Windows.Forms.CheckedListBox checkedListBoxSites;
        private System.Windows.Forms.Button buttonCopyToDB;
        private System.Windows.Forms.TextBox textBoxInfo;
    }
}

