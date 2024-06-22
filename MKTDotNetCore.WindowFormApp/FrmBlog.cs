using MKTDotNetCore.Shared;
using MKTDotNetCore.WindowFormApp.Models;
using MKTDotNetCore.WindowFormApp.Queries;

namespace MKTDotNetCore.WindowFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        private readonly int _blogId;

        public FrmBlog()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        public FrmBlog(int blogId)
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
            _blogId = blogId;
            var model = _dapperService.QueryFirstOrDefault<BlogModel>(BlogQuery.BlogFirstOrDefaultQuery, new {BlogId=_blogId});
            if (model is null) return;
            txtTitle.Text = model.BlogTitle;
            txtAuthor.Text = model.BlogAuthor;
            txtContent.Text=model.BlogContent;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = txtTitle.Text,
                BlogAuthor = txtAuthor.Text,
                BlogContent = txtContent.Text
            };
            int result = _dapperService.Execute(BlogQuery.BlogCreteQuery, blog);
            string message = result > 0 ? "Successfully Save" : "Saving Fail";
            var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message, "BlogCreate", MessageBoxButtons.OK, messageBoxIcon);
            Clear();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtTitle.Clear();
            txtAuthor.Clear();
            txtContent.Clear();
            txtTitle.Focus();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                BlogModel blog = new BlogModel
                {
                    BlogId = _blogId,
                    BlogTitle = txtTitle.Text.Trim(),
                    BlogAuthor = txtAuthor.Text.Trim(),
                    BlogContent = txtContent.Text.Trim()
                };
                int result=_dapperService.Execute(BlogQuery.BlogUpdateQuery, blog);
                string message = result > 0 ? "Successfully Update" : "Update Fail";
                MessageBox.Show(message);
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
