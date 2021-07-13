
namespace SortingByStringPropertyName
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.ReadCustomersButton = new System.Windows.Forms.Button();
            this.PropertyNamesListBox = new System.Windows.Forms.ListBox();
            this.AscendingRadioButton = new System.Windows.Forms.RadioButton();
            this.DescendingRadioButton = new System.Windows.Forms.RadioButton();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CompanyNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ContactTitleColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.FirstNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LastNameColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.StreetColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CityColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CountryColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // ReadCustomersButton
            // 
            this.ReadCustomersButton.Image = ((System.Drawing.Image)(resources.GetObject("ReadCustomersButton.Image")));
            this.ReadCustomersButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ReadCustomersButton.Location = new System.Drawing.Point(261, 84);
            this.ReadCustomersButton.Name = "ReadCustomersButton";
            this.ReadCustomersButton.Size = new System.Drawing.Size(105, 23);
            this.ReadCustomersButton.TabIndex = 0;
            this.ReadCustomersButton.Text = "Read";
            this.ReadCustomersButton.UseVisualStyleBackColor = true;
            this.ReadCustomersButton.Click += new System.EventHandler(this.ReadCustomersButton_Click);
            // 
            // PropertyNamesListBox
            // 
            this.PropertyNamesListBox.FormattingEnabled = true;
            this.PropertyNamesListBox.ItemHeight = 15;
            this.PropertyNamesListBox.Location = new System.Drawing.Point(5, 12);
            this.PropertyNamesListBox.Name = "PropertyNamesListBox";
            this.PropertyNamesListBox.Size = new System.Drawing.Size(237, 184);
            this.PropertyNamesListBox.TabIndex = 1;
            // 
            // AscendingRadioButton
            // 
            this.AscendingRadioButton.AutoSize = true;
            this.AscendingRadioButton.Checked = true;
            this.AscendingRadioButton.Location = new System.Drawing.Point(261, 24);
            this.AscendingRadioButton.Name = "AscendingRadioButton";
            this.AscendingRadioButton.Size = new System.Drawing.Size(81, 19);
            this.AscendingRadioButton.TabIndex = 2;
            this.AscendingRadioButton.TabStop = true;
            this.AscendingRadioButton.Text = "Ascending";
            this.AscendingRadioButton.UseVisualStyleBackColor = true;
            // 
            // DescendingRadioButton
            // 
            this.DescendingRadioButton.AutoSize = true;
            this.DescendingRadioButton.Location = new System.Drawing.Point(261, 49);
            this.DescendingRadioButton.Name = "DescendingRadioButton";
            this.DescendingRadioButton.Size = new System.Drawing.Size(87, 19);
            this.DescendingRadioButton.TabIndex = 3;
            this.DescendingRadioButton.Text = "Descending";
            this.DescendingRadioButton.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CompanyNameColumn,
            this.ContactTitleColumn,
            this.FirstNameColumn,
            this.LastNameColumn,
            this.StreetColumn,
            this.CityColumn,
            this.CountryColumn});
            this.dataGridView1.Location = new System.Drawing.Point(8, 213);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new System.Drawing.Size(856, 211);
            this.dataGridView1.TabIndex = 4;
            // 
            // CompanyNameColumn
            // 
            this.CompanyNameColumn.DataPropertyName = "CompanyName";
            this.CompanyNameColumn.HeaderText = "Company name";
            this.CompanyNameColumn.Name = "CompanyNameColumn";
            this.CompanyNameColumn.Width = 150;
            // 
            // ContactTitleColumn
            // 
            this.ContactTitleColumn.DataPropertyName = "ContactTitle";
            this.ContactTitleColumn.HeaderText = "Title";
            this.ContactTitleColumn.Name = "ContactTitleColumn";
            // 
            // FirstNameColumn
            // 
            this.FirstNameColumn.DataPropertyName = "FirstName";
            this.FirstNameColumn.HeaderText = "First Name";
            this.FirstNameColumn.Name = "FirstNameColumn";
            // 
            // LastNameColumn
            // 
            this.LastNameColumn.DataPropertyName = "LastName";
            this.LastNameColumn.HeaderText = "Last Name";
            this.LastNameColumn.Name = "LastNameColumn";
            // 
            // StreetColumn
            // 
            this.StreetColumn.DataPropertyName = "Street";
            this.StreetColumn.HeaderText = "Street";
            this.StreetColumn.Name = "StreetColumn";
            // 
            // CityColumn
            // 
            this.CityColumn.DataPropertyName = "City";
            this.CityColumn.HeaderText = "City";
            this.CityColumn.Name = "CityColumn";
            // 
            // CountryColumn
            // 
            this.CountryColumn.DataPropertyName = "Country";
            this.CountryColumn.HeaderText = "Country";
            this.CountryColumn.Name = "CountryColumn";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(876, 441);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.DescendingRadioButton);
            this.Controls.Add(this.AscendingRadioButton);
            this.Controls.Add(this.PropertyNamesListBox);
            this.Controls.Add(this.ReadCustomersButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sort by property name";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ReadCustomersButton;
        private System.Windows.Forms.ListBox PropertyNamesListBox;
        private System.Windows.Forms.RadioButton AscendingRadioButton;
        private System.Windows.Forms.RadioButton DescendingRadioButton;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CompanyNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn ContactTitleColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn FirstNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn LastNameColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn StreetColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CityColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn CountryColumn;
    }
}

