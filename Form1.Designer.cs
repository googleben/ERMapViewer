namespace ERMapViewer
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
            if (disposing && (components != null)) {
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
            this.msbDgv = new System.Windows.Forms.DataGridView();
            this.name = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.status = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.msbDgv)).BeginInit();
            this.SuspendLayout();
            // 
            // msbDgv
            // 
            this.msbDgv.AllowUserToResizeRows = false;
            this.msbDgv.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.msbDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.msbDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.name});
            this.msbDgv.Location = new System.Drawing.Point(0, 0);
            this.msbDgv.Name = "msbDgv";
            this.msbDgv.RowHeadersVisible = false;
            this.msbDgv.RowTemplate.Height = 25;
            this.msbDgv.Size = new System.Drawing.Size(378, 421);
            this.msbDgv.TabIndex = 0;
            this.msbDgv.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.msbDgv_CellDoubleClick);
            // 
            // name
            // 
            this.name.HeaderText = "Name";
            this.name.Name = "name";
            // 
            // status
            // 
            this.status.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.status.Location = new System.Drawing.Point(0, 427);
            this.status.Name = "status";
            this.status.Size = new System.Drawing.Size(378, 23);
            this.status.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 450);
            this.Controls.Add(this.status);
            this.Controls.Add(this.msbDgv);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.msbDgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView msbDgv;
        private DataGridViewTextBoxColumn name;
        private TextBox status;
    }
}