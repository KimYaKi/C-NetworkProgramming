using System;
using System.Net.Sockets;
using System.Windows.Forms;

namespace ListForm
{
    public partial class lsForm : Form
    {
        public lsForm(string id, Socket mainSocket)
        {
            InitializeComponent();
            string list_code = "400";
            Console.WriteLine(list_code);
        }
    }
}
