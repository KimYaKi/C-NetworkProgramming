using System;
using System.Windows.Forms;

namespace StartForm
{
    public partial class StartForm : Form
    {
        public StartForm()
        {
            InitializeComponent();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            this.Hide();
            LoginForm.lgForm lg = new LoginForm.lgForm();
            lg.ShowDialog();
            this.Show();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {
            this.Hide();
            RegisterForm.regForm rg = new RegisterForm.regForm();
            rg.ShowDialog();
            this.Show();
        }
    }
}
