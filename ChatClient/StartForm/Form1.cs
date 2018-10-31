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
            LoginForm.lgForm lg = new LoginForm.lgForm();
            lg.Show();
        }

        private void btn_register_Click(object sender, EventArgs e)
        {

        }
    }
}
