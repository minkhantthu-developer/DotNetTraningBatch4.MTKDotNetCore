using MKTDotNetCore.Shared;
using MKTDotNetCore.WindowFormApp.Models;
using MKTDotNetCore.WindowFormApp.Queries;

namespace MKTDotNetCore.WindowFormApp
{
    public partial class FrmBlog : Form
    {
        private readonly DapperService _dapperService;
        public FrmBlog()
        {
            InitializeComponent();
            _dapperService= new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BlogModel blog = new BlogModel
            {
                BlogTitle = txtTitle.Text,
                BlogAuthor = txtAuthor.Text,
                BlogContent = txtContent.Text
            };
            int result=_dapperService.Execute(BlogQuery.BlogCreteQuery, blog);
            string message = result > 0 ? "Successfully Save" : "Saving Fail";
            var messageBoxIcon = result > 0 ? MessageBoxIcon.Information : MessageBoxIcon.Error;
            MessageBox.Show(message,"BlogCreate",MessageBoxButtons.OK, messageBoxIcon);
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
    }
}
