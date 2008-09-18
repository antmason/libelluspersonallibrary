namespace Libellus.UI.Forms
{
    partial class ViewBooksForm
    {
        #region Private Data
        private Libellus.UI.Controls.BookGrid bookGrid1;
        private Libellus.UI.Controls.BookSearch bookSearch1;
        #endregion 

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
            this.bookGrid1 = new Libellus.UI.Controls.BookGrid();
            this.bookSearch1 = new Libellus.UI.Controls.BookSearch();
            this.SuspendLayout();
            // 
            // bookGrid1
            // 
            this.bookGrid1.AutoSize = true;
            this.bookGrid1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.bookGrid1.BookId = null;
            this.bookGrid1.LibraryMode = Libellus.Utilities.Constants.LibraryMode.LIBRARY;
            this.bookGrid1.Location = new System.Drawing.Point(9, 80);
            this.bookGrid1.Margin = new System.Windows.Forms.Padding(0);
            this.bookGrid1.Name = "bookGrid1";
            this.bookGrid1.Size = new System.Drawing.Size(726, 216);
            this.bookGrid1.TabIndex = 0;
            // 
            // bookSearch1
            // 
            this.bookSearch1.BookGrid = null;
            this.bookSearch1.Location = new System.Drawing.Point(12, 12);
            this.bookSearch1.Name = "bookSearch1";
            this.bookSearch1.Size = new System.Drawing.Size(679, 65);
            this.bookSearch1.TabIndex = 1;
            // 
            // ViewBooksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.ClientSize = new System.Drawing.Size(747, 306);
            this.Controls.Add(this.bookSearch1);
            this.Controls.Add(this.bookGrid1);
            this.Name = "ViewBooksForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion





    }
}
