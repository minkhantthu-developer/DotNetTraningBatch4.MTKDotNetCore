using MKTDotNetCore.Shared;
using MKTDotNetCore.WindowFormApp.Models;
using MKTDotNetCore.WindowFormApp.Queries;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MKTDotNetCore.WindowFormApp
{
    public partial class frmBlogList : Form
    {
        private readonly DapperService _dapperService;
        public frmBlogList()
        {
            InitializeComponent();
            dgvBlog.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void frmBlogList_Load(object sender, EventArgs e)
        {
            try
            {
                BlogList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void dgvBlog_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1) return;
            int blogId = Convert.ToInt32(dgvBlog.Rows[e.RowIndex].Cells["colId"].Value);
            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType = (EnumFormControlType)index;
            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    FrmBlog frmBlog = new FrmBlog(blogId);
                    frmBlog.ShowDialog();
                    BlogList();
                    break;
                case EnumFormControlType.Delete:
                    var result = MessageBox.Show("Are you sure want to Delete", "Delete Blog", MessageBoxButtons.YesNo);
                    if (result != DialogResult.Yes) return;
                    DeleteBlog(blogId);
                    BlogList();
                    break;
                default: MessageBox.Show("Invalid Case");
                    break;
            }
        }

        private void BlogList()
        {
            var lst = _dapperService.Query<BlogModel>(BlogQuery.BlogReadQuery);
            dgvBlog.DataSource = lst;
        }

        private void DeleteBlog(int id)
        {
            int result = _dapperService.Execute(BlogQuery.BlogDeleteQuery, new { BlogId = id });
            string message = result > 0 ? "Successfully Delete" : "Deleting Fail";
            MessageBox.Show(message);
        }
    }
}
