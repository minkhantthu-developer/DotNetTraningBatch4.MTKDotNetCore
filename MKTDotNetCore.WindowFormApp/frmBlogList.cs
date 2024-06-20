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
                var lst = _dapperService.Query<BlogModel>(BlogQuery.BlogReadQuery);
                dgvBlog.DataSource = lst;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
    }
}
