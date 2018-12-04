using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RegisterForm
{
    public partial class regForm : Form
    {
        public regForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(inputID.Text.Equals("") || inputPwd.Text.Equals("") || inputName.Text.Equals(""))
            {
                MessageBox.Show("빈칸이 있습니다.");
            }
            else
            {
                string data = "300/" + inputID.Text + "/" + inputPwd.Text + "/" + inputName.Text + "/";
                MessageBox.Show(data);
            }
        }
    }
}
