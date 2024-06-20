﻿namespace MKTDotNetCore.WindowFormApp
{
    partial class frmBlogList
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
            dgvBlog = new DataGridView();
            colId = new DataGridViewTextBoxColumn();
            colTitle = new DataGridViewTextBoxColumn();
            colAuthor = new DataGridViewTextBoxColumn();
            colContent = new DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)dgvBlog).BeginInit();
            SuspendLayout();
            // 
            // dgvBlog
            // 
            dgvBlog.AllowUserToAddRows = false;
            dgvBlog.AllowUserToDeleteRows = false;
            dgvBlog.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvBlog.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvBlog.Columns.AddRange(new DataGridViewColumn[] { colId, colTitle, colAuthor, colContent });
            dgvBlog.Dock = DockStyle.Fill;
            dgvBlog.Location = new Point(0, 0);
            dgvBlog.Name = "dgvBlog";
            dgvBlog.ReadOnly = true;
            dgvBlog.RowHeadersWidth = 51;
            dgvBlog.Size = new Size(800, 450);
            dgvBlog.TabIndex = 0;
            // 
            // colId
            // 
            colId.DataPropertyName = "BlogId";
            colId.HeaderText = "Id";
            colId.MinimumWidth = 6;
            colId.Name = "colId";
            colId.ReadOnly = true;
            colId.Visible = false;
            // 
            // colTitle
            // 
            colTitle.DataPropertyName = "BlogTitle";
            colTitle.HeaderText = "Title";
            colTitle.MinimumWidth = 6;
            colTitle.Name = "colTitle";
            colTitle.ReadOnly = true;
            // 
            // colAuthor
            // 
            colAuthor.DataPropertyName = "BlogAuthor";
            colAuthor.HeaderText = "Author";
            colAuthor.MinimumWidth = 6;
            colAuthor.Name = "colAuthor";
            colAuthor.ReadOnly = true;
            // 
            // colContent
            // 
            colContent.DataPropertyName = "BlogContent";
            colContent.HeaderText = "Content";
            colContent.MinimumWidth = 6;
            colContent.Name = "colContent";
            colContent.ReadOnly = true;
            // 
            // frmBlogList
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(dgvBlog);
            Name = "frmBlogList";
            Text = "frmBlogList";
            Load += frmBlogList_Load;
            ((System.ComponentModel.ISupportInitialize)dgvBlog).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView dgvBlog;
        private DataGridViewTextBoxColumn colId;
        private DataGridViewTextBoxColumn colTitle;
        private DataGridViewTextBoxColumn colAuthor;
        private DataGridViewTextBoxColumn colContent;
    }
}