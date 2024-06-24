using MKTDotNetCore.Shared;

namespace MKTDotNetCore.WinFormsAppSqlInjection
{
    public partial class Form1 : Form
    {
        private readonly DapperService _dapperService;

        public Form1()
        {
            InitializeComponent();
            _dapperService = new DapperService(ConnectionStrings.SqlConnectionStringBuilder.ConnectionString);
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string query = $"select * from Tbl_User where Email='{txtEmail.Text.Trim()}' and Password='{txtPassword.Text.Trim()}'";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query);
            if (model is null)
            {
                MessageBox.Show("User does not extist");
                return;
            }
            MessageBox.Show("Admin email " + model.Email);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = "select * from Tbl_User where Email=@Email and Password=@Password";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email = txtEmail.Text.Trim(),
                Password = txtPassword.Text.Trim()
            });
            if (model is null)
            {
                MessageBox.Show("User does not extist");
                return;
            }
            MessageBox.Show("Admin email " + model.Email);
        }
    }

    public class UserModel
    {
        public string? Email { get; set; }
        public bool IsAdmin { get; set; }
    }
}
